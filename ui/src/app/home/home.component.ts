import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  protected hasAccount: boolean | null = null; // null = unknown

  constructor(
    private _authService: AuthService
  ) {
    this._authService.isSignedIn();
  }

  redirectToSignup() {

  }

  redirectToLogin() {

  }
}
