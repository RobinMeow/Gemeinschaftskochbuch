import { Inject, Injectable, OnDestroy } from '@angular/core';
import { Auth, User, UserCredential, createUserWithEmailAndPassword, signInWithEmailAndPassword, signOut, user } from '@angular/fire/auth';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { API_BASE_URI } from './app.tokens';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  private _user$: Observable<User | null> = user(this._auth);
  private _onAuthStateChangd: Subscription;
  private _isAuthenticated = new BehaviorSubject(false);

  get isAuthenticated$(): Observable<boolean> {
    return this._isAuthenticated.asObservable();
  }

  constructor(
    private _auth: Auth, /* _auth is stateful and carries all data, for example, whether or not, a user is currently logged in. */
    @Inject(API_BASE_URI) private _apiBaseUri: string,
    private _httpClient: HttpClient,
  )
  {
    this._onAuthStateChangd = this._user$.subscribe((currentUser: User | null) => {
      this._isAuthenticated.next(currentUser != null);
    });
  }

  // On successful creation of the user account, this user will also be signed in to your application.
  async signup(email: string, password: string): Promise<void> {
    const userCredential: UserCredential = await createUserWithEmailAndPassword(this._auth, email, password);
  }

  setChefname(chefname: string): void {
    const httpOptions: { headers: HttpHeaders; } = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    // email and uid are read from claim
    const url = this._apiBaseUri + '/Auth/SignUp?chefname=' + chefname;

    const post = this._httpClient.post<any>(url, httpOptions);
    post.subscribe();
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

  ngOnDestroy(): void {
    this._onAuthStateChangd.unsubscribe();
  }
}
