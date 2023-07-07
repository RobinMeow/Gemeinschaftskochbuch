import { Injectable } from '@angular/core';

@Injectable()
export class TokenCacheService {
  private readonly tokenKey = 'token';

  public tryGet(): string | null {
    const token = localStorage.getItem(this.tokenKey);
    return token;
  }

  public set(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  public clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
