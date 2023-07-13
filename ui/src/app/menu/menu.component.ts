import { Component, Input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDrawer } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnDestroy {
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
