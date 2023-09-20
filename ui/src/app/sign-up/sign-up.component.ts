import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { PasswordComponent } from '../password/password.component';
import { Router } from '@angular/router';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { Observable, catchError, first, of } from 'rxjs';
import { ApiNotification } from '../common/ApiNotification';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    PasswordComponent,
    MatStepperModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  accountForm: FormGroup;
  chefnameForm: FormGroup;

  @ViewChild('stepper') private _stepper!: MatStepper;

  constructor(
    private _authService: AuthService,
    formBuilder: FormBuilder,
    private _router: Router,
  ) {
    _authService.isAuthenticated$.pipe(first()).subscribe((isAuthed) => {
      if (isAuthed) {
        _router.navigateByUrl('');
        return;
      }
    });

    this.accountForm = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
    });
    this.chefnameForm = formBuilder.group({
      chefname: [ '', [Validators.required, Validators.minLength(3), Validators.maxLength(20)] ],
    });
  }

  async signup() {
    try {
      if (this.accountForm.invalid) return;

      const { email, password } = this.accountForm.value;

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

  submitChefname(){
    if (this.chefnameForm.invalid) return;

    const { chefname } = this.chefnameForm.value;
    const unkownError = undefined;

    this._authService.chooseChefname(chefname)
    .pipe(
      catchError((err: HttpErrorResponse, caught: Observable<any>) => {
        console.error(err.message, err);
        return of(unkownError);
      })
    ).subscribe((response: HttpResponse<any> | undefined)=> {
      console.log(response);

      if (response === unkownError)
        return;

      if (response.ok) {
        this._stepper.next();
      }
      else {
        console.log(response.body?.notifications ?? 'Keine Fehler.');
      }
    });
  }
}
