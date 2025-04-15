import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { Companie } from '../types/Companie';

@Injectable({
  providedIn: 'root'
})
export class CompanieService {
  baseUrl: string = environment.apiUrl ?? 'https://localhost:7125/api'
  url: string = this.baseUrl + '/Companie';

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) { }

  getAll(): Observable<HttpResponse<Companie[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.get<Companie[]>(this.url, { headers: headers, observe: 'response' });
  }
}
