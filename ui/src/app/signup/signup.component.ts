import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth, createUserWithEmailAndPassword } from '@angular/fire/auth';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TokenCacheService } from '../token-cache.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './signup.component.html',
  standalone: true,
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {

  protected signupForm: FormGroup;

  private _auth: Auth = inject(Auth);

  constructor(
    formBuilder: FormBuilder,
    private _tokenCacheService: TokenCacheService,
    private _httpClient: HttpClient
    ) {
    this.signupForm = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  signup() {
    if (this.signupForm.invalid) return;

    const { email, password } = this.signupForm.value;

    createUserWithEmailAndPassword(this._auth, email, password).then(userCredential => {
      console.log(userCredential);

      userCredential.user.getIdToken().then(tokenId => {

        this._tokenCacheService.set(tokenId);
        console.log("SignedUp and Token recieved. Token: ");
        console.log(tokenId);

        this._httpClient.get<any>('http://localhost:5263/Recipe/Test')
        .subscribe(okResult => {
          console.log(okResult);
        }); // req auth

      }).catch(error => {
        console.error(error);
      });
    }).catch(error => {
        console.error(error);
    });
  }
}
