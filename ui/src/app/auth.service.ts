import { Injectable, OnDestroy } from '@angular/core';
import { Auth, User, UserCredential, createUserWithEmailAndPassword, signInWithEmailAndPassword, signOut, user } from '@angular/fire/auth';
import { TokenCacheService } from './token-cache.service';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  private _user$: Observable<User | null> = user(this._auth);
  private _userSubscription: Subscription;
  // private isLoggedIn = new BehaviorSubject(false);

  constructor(
    private _auth: Auth,
    private _tokenCacheService: TokenCacheService
  )
  {
    this._userSubscription = this._user$.subscribe((currentUser: User | null) => {
      //handle user state changes here. Note, that user will be null if there is no currently logged in user.
      console.log("CurrentUser: ");
      console.log(currentUser);
      // this.isLoggedIn.next(!!currentUser);
    });
  }

  isSignedIn (): boolean {
    console.warn("ToDo: verify the token (f.g. if it has expired).");
    return this._tokenCacheService.tryGet() != null;
  }

  // On successful creation of the user account, this user will also be signed in to your application.
  async signup(email: string, password: string): Promise<void> {
    const userCredential: UserCredential = await createUserWithEmailAndPassword(this._auth, email, password);
    this.setToken(userCredential);
  }

  private async getTokenId(userCredential: UserCredential): Promise<string> {
    return await userCredential.user.getIdToken();
  }

  private async setToken(userCredential: UserCredential) {
    const tokenId: string = await this.getTokenId(userCredential);
    this._tokenCacheService.set(tokenId);
  }

  async login(email: string, password: string): Promise<void> {
    const userCredential: UserCredential = await signInWithEmailAndPassword(this._auth, email, password);
    this.setToken(userCredential);
  }

  logout(): Promise<void> {
    this._tokenCacheService.clear();
    return signOut(this._auth);
  }

  ngOnDestroy(): void {
    this._userSubscription.unsubscribe();
  }
}
