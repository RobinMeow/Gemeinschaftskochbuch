import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { PasswordComponent } from '../password/password.component';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    PasswordComponent
  ],
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  form: FormGroup;

  constructor(
    private _authService: AuthService,
    formBuilder: FormBuilder,
    private _router: Router,
    private _userService: UserService
  ) {
    this.form = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      chefname: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(3)]],
    });
  }

  async signup() {
    try {
      if (this.form.invalid) return;

      const { email, password, chefname } = this.form.value;

      await this._authService.signup(email, password)
      .then(() => {
        this._router.navigateByUrl('');
        this._userService.setUsername(email, chefname);
      })
      .catch(err => {
        const errMsg: string = JSON.stringify(err);
        const isEmailAlreadyInUse: boolean = errMsg.indexOf('auth/email-already-in-use') != -1;

        if (isEmailAlreadyInUse) {
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
}
