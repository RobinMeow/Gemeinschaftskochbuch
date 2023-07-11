import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgSwitch, NgSwitchDefault, NgSwitchCase } from '@angular/common';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';

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
        SignupComponent,
        LoginComponent
    ]
})
export class AppComponent {
  title = 'Gemeinschaftskochbuch';
}
