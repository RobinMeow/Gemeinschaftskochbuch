import { Inject, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaders } from '@angular/common/http';
import { Observable, from, mergeMap } from 'rxjs';
import { AuthService } from './auth.service';
import { FRONTEND_ORIGINS } from './app.tokens';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private _auth: AuthService,
    @Inject(FRONTEND_ORIGINS) private _frontendOrigins: string[],
    ) {
    }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // ToDo: If api
    // ToDo: if protected

    // we cant use await here, without changing the function signature.
    // so we transform the Promise into an Observable,
    // and merge it with the returning Observable
    const tryGetTokenAsync: Promise<string | undefined> = this._auth.tryGetToken();
    const workaroundAwait: Observable<string | undefined> = from(tryGetTokenAsync);

    return workaroundAwait.pipe(
      mergeMap(token => {
        if (token != undefined) {
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`,
              'Access-Control-Allow-Origin': this._frontendOrigins.join(',')
            }
          });
        }
        else{
          // assume API call which doesnt require authentication.
        }

        return next.handle(request);
      })
    );
  }
}
