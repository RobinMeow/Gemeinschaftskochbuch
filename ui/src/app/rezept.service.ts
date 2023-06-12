import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Rezept } from './add-rezept/Rezept';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { NewRezept } from './add-rezept/NewRezept';

const API: string = 'http://localhost:5263/Rezept/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    // Authorization: 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root',
})
export class RezeptService {
  private readonly _handleError: HandleError; // https://angular.io/guide/http#sending-data-to-a-server

  constructor(
    private _httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandler
    ) {
    this._handleError = httpErrorHandler.createHandleError('RezeptService');
  }

  add(rezept: NewRezept): Observable<Rezept | null> {
    const functionName: string = 'Add';
    return this._httpClient.post<Rezept>(API + functionName, rezept, httpOptions)
      .pipe(
        catchError(this._handleError(functionName, null))
      );
  }

  getAll(): Observable<Rezept[]> {
    const functionName: string = 'GetAll';
    return this._httpClient.get<Rezept[]>(API + functionName)
      .pipe(
        catchError(this._handleError(functionName, []))
      );
  }
}
