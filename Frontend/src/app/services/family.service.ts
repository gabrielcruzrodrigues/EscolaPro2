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
}
