import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CondicionPreviaService {
  
  private URL_BASE = environment.api_base + '/fritura/condicionprevia'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this.URL_BASE = baseUrl + `${environment.api_base}/fritura/condicionprevia`;
  }

  getAll(tipoId: number, linea: number) {
    return this.http.get(this.URL_BASE + `?tipoId=${tipoId}&linea=${linea}`);
  }

}
