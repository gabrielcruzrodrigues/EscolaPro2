import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { LoginRequest } from '../types/Auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = environment.apiUrl ?? 'https://localhost:7125/api'
  url: string = this.baseUrl + '/Auth';
  accessToken: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private cookieService: CookieService
  ) { }

  login(data: LoginRequest): Observable<any> {
    const urlForRequest = this.url + '/login';
    return this.http.post(urlForRequest, data, { observe: 'response' });
  }

  getRole(): number {
    return Number(this.cookieService.get('role'));
  }

  async getName(): Promise<string> {
    return this.cookieService.get('name');
  }

  async getId(): Promise<string> {
    return this.cookieService.get('userId');
  }

  saveCookiesLogin(body: any): void {
    this.cookieService.set('token', body.token, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });

    this.cookieService.set('refreshToken', body.refreshToken, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });

    this.cookieService.set('expiration', body.expiration, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });

    this.cookieService.set('role', body.role, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });

    this.cookieService.set('name', body.name, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });

    this.cookieService.set('userId', body.userId, {
      path: '/', 
      secure: false, // Trocar para true em prod
      sameSite: 'Lax', 
      expires: 1 // Alterar em prod
    });
  }

  getAccessToken(): string {
    const cookieValue = this.cookieService.get('token');
    return cookieValue; 
  }

  logout(): void {
    this.cookieService.deleteAll();
    this.router.navigate(['/login']);
  }
}
