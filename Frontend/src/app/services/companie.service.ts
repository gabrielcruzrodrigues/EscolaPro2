import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { Companie, CreateCompanie, UpdateCompanie } from '../types/Companie';

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

  create(data: CreateCompanie): Observable<HttpResponse<Companie>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.post<Companie>(this.url, data, { headers: headers, observe: 'response' });
  }

  delete(id: number): Observable<any> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.delete(urlForRequest, { headers: headers, observe: 'response' });
  }

  search(param: string): Observable<HttpResponse<Companie[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/search/${param}`;
    return this.http.get<Companie[]>(urlForRequest, { headers: headers, observe: 'response' });
  }

  getById(id: string): Observable<HttpResponse<Companie>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.get<Companie>(urlForRequest, { headers: headers, observe: 'response' });
  }

  update(companie: UpdateCompanie): Observable<HttpResponse<any>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url;
    return this.http.put(urlForRequest, companie, { headers: headers, observe: 'response' });
  }
}
