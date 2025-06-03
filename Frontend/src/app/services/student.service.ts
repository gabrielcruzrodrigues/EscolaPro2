import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../types/Student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl: string = environment.apiUrl ?? 'https://localhost:7125/api'
  url: string = this.baseUrl + '/Student';

  constructor(
    private authService: AuthService,
    private http: HttpClient
  ) { }

  create(data: FormData): Observable<HttpResponse<Student>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    return this.http.post<Student>(this.url, data, { headers: headers, observe: 'response' });
  }

  getById(id: string): Observable<HttpResponse<Student>> {
    const accessToken = this.authService.getAccessToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
    const urlForRequest = this.url + `/${id}`;
    return this.http.get<Student>(urlForRequest, { headers: headers, observe: 'response' });
  }
}
