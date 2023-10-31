import {Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { tap } from 'rxjs';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatStepperModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  private readonly _authService = inject(AuthService);
  private readonly _router = inject(Router);
  private readonly _nnfb = inject(NonNullableFormBuilder);

  protected readonly signUpForm = this._nnfb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    chefname: [ '', [Validators.required, Validators.minLength(3), Validators.maxLength(20)] ],
  });

  @ViewChild('stepper') private _stepper!: MatStepper;

  ngOnInit(): void {
    if (this._authService.isAuthenticated()) {
      this._router.navigateByUrl('');
      return;
    }
  }

  async signup() {
    try {
      if (this.signUpForm.controls.email.invalid || this.signUpForm.controls.password.invalid)
        return;

      const email: string = this.signUpForm.controls.email.value;
      const password: string = this.signUpForm.controls.password.value;

      await this._authService.signup(email, password)
      .then(() => {
        this._stepper.next();
      })
      .catch(err => {
        const errMsg: string = JSON.stringify(err);
        const isEmailAlreadyInUse: boolean = errMsg.indexOf('auth/email-already-in-use') != -1; // ToDo: Use Async Validator instead

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

  protected submitChefname(){
    if (this.signUpForm.controls.chefname.invalid) return;

    const chefname = this.signUpForm.controls.chefname.value;

    this._authService.chooseChefname(chefname)
    .pipe(
      tap({
        error: (err) => {
          console.error(err);
        }
      }),
    ).subscribe(() => {
      this._router.navigateByUrl('');
    });
  }
}
