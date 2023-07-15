import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { API_BASE_URI } from './app.tokens';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _httpOptions: { headers: HttpHeaders; };
  private _api: string;

  constructor(
    @Inject(API_BASE_URI) apiBaseUri: string,
    private _httpClient: HttpClient,
  ) {
    this._api = apiBaseUri + '/User/';

    this._httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
  }

  setUsername(email: string, chefname: string) {
    return this._httpClient.post<any>(this._api + 'setChefname', { email, chefname }, this._httpOptions);
  }

}
