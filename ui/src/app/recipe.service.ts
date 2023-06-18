import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Recipe } from './add-recipe/Recipe';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { NewRecipe } from './add-recipe/NewRecipe';
import { API_BASE_URI } from './app.tokens';


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

  private _api: string;

  constructor(
    @Inject(API_BASE_URI) apiBaseUri: string,
    private _httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandler
    ) {
    this._api = apiBaseUri + 'Recipe/';
    this._handleError = httpErrorHandler.createHandleError('RecipeService');
  }

  add(recipe: NewRecipe): Observable<Recipe | null> {
    const functionName: string = 'Add';
    return this._httpClient.post<Recipe>(this._api + functionName, recipe, httpOptions)
      .pipe(
        catchError(this._handleError(functionName, null))
      );
  }

  getAll(): Observable<Recipe[]> {
    const functionName: string = 'GetAll';
    return this._httpClient.get<Recipe[]>(this._api + functionName)
      .pipe(
        catchError(this._handleError(functionName, []))
      );
  }
}
