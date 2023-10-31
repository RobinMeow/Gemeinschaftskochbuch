import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { Observable, catchError, first, of } from 'rxjs';
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
    this._authService.isAuthenticated$.pipe(first()).subscribe((isAuthed) => {
      if (isAuthed) {
        this._router.navigateByUrl('');
        return;
      }
    });
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

  submitChefname(){
    if (this.signUpForm.controls.chefname.invalid) return;

    const chefname = this.signUpForm.controls.chefname.value;
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
