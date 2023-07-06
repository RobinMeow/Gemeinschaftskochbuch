import { Inject, Injectable } from '@angular/core';
import { API_BASE_URI, FRONTEND_ORIGINS } from './app.tokens';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class ApiAuthService {
  private readonly _handleError: HandleError; // https://angular.io/guide/http#sending-data-to-a-server

  private _httpOptions: { headers?: HttpHeaders };

  private _api: string;

  constructor(
    @Inject(API_BASE_URI) apiBaseUri: string,
    @Inject(FRONTEND_ORIGINS) frontendOrigins: string[],
    private _httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandler
    ) {
    this._api = apiBaseUri + '/Auth/';

    this._httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': frontendOrigins.join(','),
      })
    };
    this._handleError = httpErrorHandler.createHandleError('AuthService');
  }
}
