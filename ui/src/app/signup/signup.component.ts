import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth, createUserWithEmailAndPassword } from '@angular/fire/auth';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

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

  constructor(private formBuilder: FormBuilder) {
    this.signupForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  signup() {
    if (this.signupForm.invalid) return;

    const { email, password } = this.signupForm.value;

    createUserWithEmailAndPassword(this._auth, email, password)
      .then(userCredential => {
        // Signup successful
        console.log(userCredential);
      })
      .catch(error => {
        // Signup failed
        console.error(error);
      });
  }
}
