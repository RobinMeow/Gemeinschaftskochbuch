import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  protected hasAccount: boolean | null = null; // null = unknown

  constructor(
    private _authService: AuthService,
    private _router: Router
  ) {
    this._authService.isSignedIn();
  }

  redirectToSignup() {
    this._router.navigateByUrl('/signup');
  }

  redirectToLogin() {
    this._router.navigateByUrl('/signin');
  }
}
