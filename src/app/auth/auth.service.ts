import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { tap } from 'rxjs/operators';
import { appRoutes } from '../app.routes';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7231/api/Auth';

  constructor(private http: HttpClient) {}
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  login(credentials: LoginRequest) {
    return this.http.post<{ token: string, usuario: string }>(`${this.baseUrl}/login`, credentials)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('usuario', response.usuario);
        })
      );
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);

  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUsuario(): string | null {
    return localStorage.getItem('usuario');
  }
}