import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { API_BASE_URI } from './app.tokens';
import { Auth } from '@angular/fire/auth';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _httpOptions: { headers: HttpHeaders; };
  private _api: string;

  constructor(
    @Inject(API_BASE_URI) apiBaseUri: string,
    private _httpClient: HttpClient,
    private _auth: Auth
  ) {
    this._api = apiBaseUri + '/User/';

    this._httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
  }

  setUsername(email: string, chefname: string) {
    const user = this._auth.currentUser;

    if (!user)
      throw new Error('User not authenticated.');

    return this._httpClient.post<any>(this._api + 'setChefname', { userId: user.uid, email, chefname }, this._httpOptions);
  }

}
