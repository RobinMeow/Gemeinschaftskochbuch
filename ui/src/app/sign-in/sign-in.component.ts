import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  private readonly _authService = inject(AuthService);
  private readonly _router = inject(Router);
  private readonly _nnfb = inject(NonNullableFormBuilder);

  protected readonly signInForm = this._nnfb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  ngOnInit(): void {
    if (this._authService.isAuthenticated()) {
      this._router.navigateByUrl('');
      return;
    }
  }

  async login() {
    try {
      if (this.signInForm.invalid) return;

      const email: string = this.signInForm.controls.email.value;
      const password: string = this.signInForm.controls.password.value;

      await this._authService.signin(email, password)
      .then(() => {
        this._router.navigateByUrl('');
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
