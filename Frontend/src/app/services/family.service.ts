import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../environments/environment';
import { CreateFamily, Family } from '../types/Family';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FamilyService {
  baseUrl: string = environment.apiUrl ?? 'https://localhost:7125/api'
  url: string = this.baseUrl + '/Family';

  constructor(
    private authService: AuthService,
    private http: HttpClient
  ) { }

  create(data: FormData): Observable<HttpResponse<Family>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.post<Family>(this.url, data, { headers: headers, observe: 'response' });
  }

  delete(id: number): Observable<any> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.delete(urlForRequest, { headers: headers, observe: 'response' });
  }

  getAll(): Observable<HttpResponse<Family[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.get<Family[]>(this.url, { headers: headers, observe: 'response' });
  }

  search(param: string): Observable<HttpResponse<Family[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/search/${param}`;
    return this.http.get<Family[]>(urlForRequest, { headers: headers, observe: 'response' });
  }

  getById(id: string): Observable<HttpResponse<Family>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.get<Family>(urlForRequest, { headers: headers, observe: 'response' });
  }

  update(family: FormData): Observable<HttpResponse<any>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url;
    return this.http.put(urlForRequest, family, { headers: headers, observe: 'response' });
  }
}
