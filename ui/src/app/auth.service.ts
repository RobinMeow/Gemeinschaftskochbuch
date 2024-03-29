import { Injectable, OnDestroy, inject } from '@angular/core';
import { Auth, User, UserCredential, browserLocalPersistence, createUserWithEmailAndPassword, signInWithEmailAndPassword, signOut, user } from '@angular/fire/auth';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { API_BASE_URI } from './app.tokens';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private readonly _auth = inject(Auth); /* _auth is stateful and carries all data, for example, whether or not, a user is currently logged in. */
  private readonly _apiBaseUri: string = inject(API_BASE_URI);
  private readonly _httpClient = inject(HttpClient);

  private _user$: Observable<User | null> = user(this._auth);
  private readonly _onAuthStateChanged: Subscription;

  private _isAuthenticated = new BehaviorSubject(this.isAuthenticated());

  get isAuthenticated$(): Observable<boolean> {
    return this._isAuthenticated.asObservable();
  }

  constructor() {
    this._auth.setPersistence(browserLocalPersistence);

    this._auth.onIdTokenChanged(async (user) => {
      const token =  await user?.getIdTokenResult();
      localStorage.setItem("TokenExpirationDate", JSON.stringify(token?.expirationTime ?? 0));
    });

    this._onAuthStateChanged = this._user$.subscribe((currentUser: User | null) => {
      this._isAuthenticated.next(currentUser != null);
    });
  }

  // On successful creation of the user account, this user will also be signed in to your application.
  async signup(email: string, password: string): Promise<void> {
    const userCredential: UserCredential = await createUserWithEmailAndPassword(this._auth, email, password);
  }

  chooseChefname(chefname: string): Observable<any> {
    const httpOptions: { headers: HttpHeaders; } = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    // email and uid are read from claim
    const url = this._apiBaseUri + '/Auth/ChooseChefname?chefname=' + chefname;

    return this._httpClient.post<any>(url, httpOptions);
  }

  async signin(email: string, password: string): Promise<void> {
    const userCredential: UserCredential = await signInWithEmailAndPassword(this._auth, email, password);
  }

  async logout(): Promise<void> {
    await signOut(this._auth);
  }

  async tryGetToken(): Promise<string | undefined> {
    return await this._auth.currentUser?.getIdToken();
  }

  isAuthenticated(): any {
    const obj: string | null = localStorage.getItem("TokenExpirationDate");
    const date = obj !== null ? new Date(obj) : new Date(0);
    return date.getTime() > Date.now();
  }

  ngOnDestroy(): void {
    this._onAuthStateChanged.unsubscribe();
  }
}
