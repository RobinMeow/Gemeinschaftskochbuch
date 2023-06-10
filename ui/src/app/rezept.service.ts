import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Rezept } from './add-rezept/Rezept';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';

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

  addRezept(rezept: Rezept): Observable<Rezept> {
    return this._httpClient.post<Rezept>('http://localhost:5263' + '/Rezept/Add', rezept, httpOptions)
      .pipe(
        catchError(this._handleError('addRezept', rezept))
      );
  }
}
