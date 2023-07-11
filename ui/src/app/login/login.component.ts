import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Auth, User, UserCredential, signInWithEmailAndPassword, signOut, user } from '@angular/fire/auth';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  protected loginForm: FormGroup;

  private _auth: Auth = inject(Auth);
  user$ = user(this._auth);
  userSubscription: Subscription;
  protected loggedIn: boolean = false;

  constructor(
    private router: Router,
    private http: HttpClient,
    formBuilder: FormBuilder
  ) {
    this.userSubscription = this.user$.subscribe((aUser: User | null) => {
      //handle user state changes here. Note, that user will be null if there is no currently logged in user.
      this.loggedIn = !!aUser;
    });
    this.loginForm = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  login() {
    if (this.loginForm.invalid) return;

    const { email, password } = this.loginForm.value;

    signInWithEmailAndPassword(this._auth, email, password)
      .then((credentials: UserCredential) => {
        return credentials.user.getIdToken();
      })
      .then((idToken: string) => {
        // return this.http.post('/api/auth/login', { token: idToken }).toPromise();
      })
      .then(() => {
        // this.router.navigate(['/']);
      })
      .catch((error: any) => {
        console.error('Error logging in:', error);
      });
  }

  logout() {
    signOut(this._auth).then(() => {
      // this.router.navigate(['/login']);
    });
  }
}
