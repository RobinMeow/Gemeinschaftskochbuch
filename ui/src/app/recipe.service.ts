import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Recipe } from './add-recipe/Recipe';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { NewRecipe } from './add-recipe/NewRecipe';

const API: string = 'http://localhost:5263/Recipe/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    // Authorization: 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  private readonly _handleError: HandleError; // https://angular.io/guide/http#sending-data-to-a-server

  constructor(
    private _httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandler
    ) {
    this._handleError = httpErrorHandler.createHandleError('RecipeService');
  }

  add(recipe: NewRecipe): Observable<Recipe | null> {
    const functionName: string = 'Add';
    return this._httpClient.post<Recipe>(API + functionName, recipe, httpOptions)
      .pipe(
        catchError(this._handleError(functionName, null))
      );
  }

  getAll(): Observable<Recipe[]> {
    const functionName: string = 'GetAll';
    return this._httpClient.get<Recipe[]>(API + functionName)
      .pipe(
        catchError(this._handleError(functionName, []))
      );
  }
}
