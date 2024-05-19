import { Injectable } from '@angular/core';
import { User } from '../../models/auth/user.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JwtClaims } from '../../utils/jwt-token-claims';

const TOKEN_KEY = 'auth-token';
const REFRESHTOKEN_KEY = 'auth-refreshtoken';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  constructor() {}

  signOut(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);

    const user = this.getUserFromToken(token);

    if (user.id) {
      this.saveUser(user);
    }
  }

  private getUserFromToken(token: string): User {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken<any>(token);

    const user: User = {
      id: decodedToken[JwtClaims.Sub],
      email: decodedToken[JwtClaims.Email],
      name: decodedToken[JwtClaims.Name],
      registerDate: new Date(decodedToken[JwtClaims.RegisterDate]),
      roles: decodedToken[JwtClaims.Roles]?.split(','),
    };

    return user;
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public saveRefreshToken(token: string): void {
    window.sessionStorage.removeItem(REFRESHTOKEN_KEY);
    window.sessionStorage.setItem(REFRESHTOKEN_KEY, token);
  }

  public getRefreshToken(): string | null {
    return window.sessionStorage.getItem(REFRESHTOKEN_KEY);
  }

  public saveUser(user: User): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): User | undefined {
    const user = window.sessionStorage.getItem(USER_KEY);

    return user ? JSON.parse(user) : undefined;
  }
}
