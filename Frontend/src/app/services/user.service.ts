import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { CreateUser, ResponseCreateUser, UpdateUser, User } from '../types/User';
import { map, Observable } from 'rxjs';
import { Role } from '../types/Role';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: string = environment.apiUrl ?? 'https://localhost:7125/api'
  url: string = this.baseUrl + '/UserGeneral';

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) { }

  create(data: CreateUser): Observable<ResponseCreateUser> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.post<ResponseCreateUser>(this.url, data, { observe: 'response' }).pipe(
      map((response: HttpResponse<ResponseCreateUser>) => {
        if (!response.body) {
          throw new Error("A resposta do servidor foi nula ou inválida!")
        }
        return response.body;
      })
    )
  }

  getAllRoles(): Observable<HttpResponse<Role[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/roles`;
    return this.http.get<Role[]>(urlForRequest, { headers: headers, observe: 'response' });
  }

  getAll(): Observable<HttpResponse<User[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.get<User[]>(this.url, { headers: headers, observe: 'response' });
  }

  search(param: string): Observable<HttpResponse<User[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/search/${param}`;
    return this.http.get<User[]>(urlForRequest, { headers: headers, observe: 'response' });
  }

  delete(id: number): Observable<any> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.delete(urlForRequest, { headers: headers, observe: 'response' });
  }

  getById(id: string): Observable<HttpResponse<User>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.get<User>(urlForRequest, { headers: headers, observe: 'response' });
  }

  update(user: UpdateUser): Observable<HttpResponse<any>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url;
    return this.http.put(urlForRequest, user, { headers: headers, observe: 'response' });
  }

  lastActiveUsers(): Observable<HttpResponse<User[]>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const userCompanieId = this.authService.getCompanieId();
    const urlForRequest = this.url + `/last-active-users/${userCompanieId}`;
    console.log(urlForRequest)
    return this.http.get<User[]>(urlForRequest, { headers: headers, observe: 'response' });
  }
}
