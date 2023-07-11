import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { PasswordComponent } from '../password/password.component';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    PasswordComponent
  ],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  form: FormGroup;

  constructor(
    private _authService: AuthService,
    formBuilder: FormBuilder
  ) {
    this.form = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  async login() {
    try {
      if (this.form.invalid) return;

      const { email, password } = this.form.value;

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
      console.error(error);
    }
  }
}
