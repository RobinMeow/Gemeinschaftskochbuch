import { Component, Input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Subscription } from 'rxjs';
import { MatDrawer } from '@angular/material/sidenav';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-corner',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './auth-corner.component.html',
  styleUrls: ['./auth-corner.component.scss']
})
export class AuthCornerComponent implements OnDestroy {
  private _sub: Subscription;

  protected isAuthenticated: boolean = false;
  @Input() drawer!: MatDrawer;

  constructor(
    protected _authService: AuthService,
    private _router: Router
  ) {
    this._sub = this._authService.isAuthenticated$.subscribe((isAuthed) => {
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
