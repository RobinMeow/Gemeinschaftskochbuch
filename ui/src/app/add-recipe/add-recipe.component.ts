import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Recipe } from './Recipe';
import { RecipeService } from '../recipe.service';
import { NewRecipe } from './NewRecipe';

@Component({
  selector: 'app-add-recipe',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    HttpClientModule
  ],
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss']
})
export class AddRecipeComponent {
  // not marked as static, because I want to access them conviently from the template
  protected readonly NAME_MIN_LENGTH: number = 3;
  protected readonly NAME_MAX_LENGTH: number = 100;

  protected recipeForm: FormGroup;
  protected recipes: Recipe[] = [];

  get name(): AbstractControl<any, any> { return this.recipeForm.get('name')!; }

  constructor(
    fromBuilder: FormBuilder,
    private _recipeService: RecipeService
  ) {
    this.recipeForm = fromBuilder.group({
      name: ['', [ // default value, than array of validators (you can pass in a second array for async validators. I should check it out)
        Validators.required,
        Validators.minLength(this.NAME_MIN_LENGTH),
        Validators.maxLength(this.NAME_MAX_LENGTH)
      ]]
    });

    // ToDo: auto completion and stuff

    // This shows how to make a dynamic form for the Zutaten, which can be any number, so the html should be generated based on it..
    // https://youtu.be/JeeUY6WaXiA?t=355

    _recipeService.getAll().subscribe((recipes: Recipe[]) => {
      this.recipes = recipes;
    });
  }

  protected onAdd(): void {
    if (!this.recipeForm.valid) return;

    const newRecipe: NewRecipe = {
      name: this.name.value,
    };

    const addRecipe$ = this._recipeService.add(newRecipe);
    addRecipe$.subscribe((recipe: Recipe | null) => {
      if (recipe == null) return; // ToDo: Display nice error snackbar or something similar.

      this.recipes.push(recipe);
    });
  }

}
