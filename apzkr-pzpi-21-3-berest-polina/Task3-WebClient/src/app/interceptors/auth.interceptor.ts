import {
  HTTP_INTERCEPTORS,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { TokenStorageService } from '../services/auth/token-storage.service';
import { AuthService } from '../services/auth/auth.service';

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );

  constructor(
    private tokenService: TokenStorageService,
    private authService: AuthService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<Object>> {
    let authReq = req;
    const token = this.tokenService.getToken();
    if (token != null) {
      authReq = this.addTokenHeader(req, token);
    }

    return next.handle(authReq).pipe(
      catchError((error) => {
        if (
          error instanceof HttpErrorResponse &&
          !(
            authReq.url.toLowerCase().includes('auth/login') ||
            authReq.url.toLowerCase().includes('auth/refresh-token')
          ) &&
          error.status === 401
        ) {
          return this.handle401Error(authReq, next);
        }

        return throwError(error);
      })
    ) as any;
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      const token = this.tokenService.getToken();
      const refreshToken = this.tokenService.getRefreshToken();

      if (token && refreshToken)
        return this.authService.refreshToken(token, refreshToken).pipe(
          switchMap(({ token, refreshToken }) => {
            this.isRefreshing = false;

            this.tokenService.saveToken(token);
            this.tokenService.saveRefreshToken(refreshToken);
            this.refreshTokenSubject.next(token);

            return next.handle(this.addTokenHeader(request, token));
          }),
          catchError((err) => {
            this.isRefreshing = false;

            if (err instanceof HttpErrorResponse && err.status === 401) {
              this.authService.logout();
              this.tokenService.signOut();
            }

            return throwError(err);
          })
        );
    }

    return this.refreshTokenSubject.pipe(
      filter((token) => token !== null),
      take(1),
      switchMap((token) => next.handle(this.addTokenHeader(request, token)))
    );
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({
      headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token),
    });
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
];
