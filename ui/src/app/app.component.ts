import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgSwitch, NgSwitchDefault, NgSwitchCase } from '@angular/common';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';

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
        AddRecipeComponent
    ]
})
export class AppComponent {
  title = 'Gemeinschaftskochbuch';
}
