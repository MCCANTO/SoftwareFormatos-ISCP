import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CondicionPreviaService {
  
  private URL_BASE = environment.api_base + '/envasado/condicionprevia'

  constructor(
    private http: HttpClient
  ) { }

  getAll(tipoId: number) {
    return this.http.get(this.URL_BASE + '?tipoId=' + tipoId);
  }

}
