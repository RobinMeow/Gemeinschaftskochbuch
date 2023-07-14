import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthCornerComponent } from './auth-corner/auth-corner.component';
import { MatDrawer } from '@angular/material/sidenav';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    AuthCornerComponent
  ],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {

  @Input() drawer!: MatDrawer;

}
