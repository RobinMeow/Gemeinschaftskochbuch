import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenCacheService } from './token-cache.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private _tokenCacheService: TokenCacheService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // ToDo: If api
    // ToDo: if protected

    const bearerToken: string | null = this._tokenCacheService.tryGet();

    if (!!bearerToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${bearerToken}`
        }
      });
    }
    else{
      // assume non protected API call.
    }

    return next.handle(request);
  }
}
