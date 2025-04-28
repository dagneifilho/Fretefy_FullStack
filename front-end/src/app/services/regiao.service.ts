import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegiaoListed } from '../models/regiao-listed.model';
import { environment } from 'src/environments/environment';
import { Regiao } from '../models/regiao.model';
import { RegiaoUpdate } from '../models/regiao-update.model';
import { RegiaoInsert } from '../models/regiao-insert.model';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  private apiBaseUrl = `${environment.apiBaseUrl}/api/regiao`;

  constructor(private http: HttpClient) { }

  getRegioes() : Observable<RegiaoListed[]>{
    return this.http.get<RegiaoListed[]>(this.apiBaseUrl);
  }

  getRegiao(id: string) : Observable<Regiao>{
    return this.http.get<Regiao>(`${this.apiBaseUrl}/${id}`);
  }

  mudarStatus(id: string) :Observable<Regiao>{
    return this.http.post<Regiao>(`${this.apiBaseUrl}/${id}/alterar-status`, null);
  }

  update(regiao: RegiaoUpdate) : Observable<Regiao> {
    return this.http.put<Regiao>(this.apiBaseUrl, regiao);
  }

  insert(regiao: RegiaoInsert):Observable<Regiao>{
    return this.http.post<Regiao>(this.apiBaseUrl, regiao);
  }
}
