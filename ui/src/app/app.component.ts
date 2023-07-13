import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NgSwitch, NgSwitchDefault, NgSwitchCase } from '@angular/common';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { MenuComponent } from './menu/menu.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [
    NgSwitch,
    NgSwitchDefault,
    NgSwitchCase,
    RouterOutlet,
    AddRecipeComponent,
    HomeComponent,
    HeaderComponent,
    MenuComponent,
    MatSidenavModule
  ]
})
export class AppComponent {
  title = 'Gemeinschaftskochbuch';
}
