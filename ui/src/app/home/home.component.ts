import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { NoahsKitchen } from '../NoahsKitchen';

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
export class HomeComponent implements OnDestroy {

  protected NoahsKitchen = NoahsKitchen;

  protected isAuthenticated: boolean = false;
  private _sub: Subscription;

  constructor(
    protected _authService: AuthService,
    private _router: Router
  ) {
    // redirect to login, when not authenticated.
    this._sub = this._authService.isAuthenticated$.subscribe((isAuthed) => {
      console.log('is authed: '+ isAuthed);
      this.isAuthenticated = isAuthed;
    });
  }

  redirectToSignup() {
    this._router.navigateByUrl('/signup');
  }

  redirectToLogin() {
    this._router.navigateByUrl('/signin');
  }

  async logout() {
    await this._authService.logout();
  }

  ngOnDestroy(): void {
    this._sub.unsubscribe();
  }
}
