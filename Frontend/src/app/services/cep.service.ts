import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cep } from '../types/Cep';

@Injectable({
  providedIn: 'root'
})
export class CepService {
  url: string = "https://brasilapi.com.br/api/cep/v1/";

  constructor(
    private http: HttpClient
  ) { }

  getCep(cep: string): Observable<HttpResponse<Cep>> {
    const urlForRequest = this.url + cep;
    return this.http.get<Cep>(urlForRequest, { observe: 'response' });
  }
}