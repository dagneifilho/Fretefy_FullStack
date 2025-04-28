import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Cidade } from '../models/cidade.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/cidade`;

  constructor(private http: HttpClient) { }

  getCidadesDisponiveis() : Observable<Cidade[]> {
    return this.http.get<Cidade[]>(`${this.apiBaseUrl}/disponiveis`);
  }
}
