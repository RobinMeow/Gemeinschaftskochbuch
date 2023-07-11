import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../auth.service';
import { FirebaseError } from '@angular/fire/app';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  protected loginForm: FormGroup;
  hasAccount: boolean | null = null;

  constructor(
    private _router: Router,
    private _authService: AuthService,
    formBuilder: FormBuilder
  ) {
    if (false) // prevent loop, cuz this component is in home rn
      if (this._authService.isSignedIn()) {
        _router.navigateByUrl('/');
        console.log('Already signed in. Redirect to home.');
      }

    this.loginForm = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  showLogin(){
    this.hasAccount = true;
  }

  showSignup(){
    this.hasAccount = false;
  }

  async signup() {
    try {
      if (this.loginForm.invalid) return;

      const { email, password } = this.loginForm.value;

      await this._authService.signup(email, password)
      .then(() => {
        this.hasAccount = false;
      })
      .catch(err => {
        const errMsg: string = JSON.stringify(err);
        const emailAlreadyInUse: boolean = errMsg.indexOf('auth/email-already-in-use') != -1;

        if (emailAlreadyInUse) {
          console.warn('Email existiert bereits.');
        }
        else {
          console.error(err);
        }
      });
    }
    catch (error) {
      console.error(error);
    }
  }

  async login() {
    try {
      if (this.loginForm.invalid) return;

      const { email, password } = this.loginForm.value;

      await this._authService.login(email, password)
      .then(() => {
      })
      .catch((error) => {
        const errorMessage: string = JSON.stringify(error);;
        const userNotFound: boolean = errorMessage.indexOf('auth/user-not-found') != -1;
        const wrongPassword: boolean = errorMessage.indexOf('auth/wrong-password') != -1;

        if (userNotFound)
          console.log("Benutzer existiert nicht.");
        else if (wrongPassword)
          console.log("Falsches Passwort.");
        else
          console.log(error);
          // ToDo: Feedback, Falscher Benutzername oder Password, oder nicht registriert
      });
    }
    catch (error) {
      if (typeof(error) == typeof(FirebaseError)) {
        console.error("Wrong Password.");
      }
      else
        console.error(error);
    }
  }

  logout() {
    this._authService.logout();
  }
}
