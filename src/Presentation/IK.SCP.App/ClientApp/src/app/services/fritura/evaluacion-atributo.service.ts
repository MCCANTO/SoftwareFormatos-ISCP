import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EvaluacionAtributoService {

  private URL_BASE = environment.api_base + '/fritura/evaluacionatributo'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + `${environment.api_base}/fritura/evaluacionatributo`;
  }

  getAll(linea: number, ordenId: string) {
    return this.http.get(`${this.URL_BASE}?linea=${linea}&ordenId=${ordenId}`);
  }

  getById(id: number) {
    return this.http.get(`${this.URL_BASE}/${id}`);
  }

  create( data: any ) {
    const body = { ...data };
    return this.http.post(this.URL_BASE, body);
  }

  generatePDFEvaluacionAtributos(linea:number, ordenId:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    return this.http.get(`${this.URL_BASE}/PDF?linea=${linea}&orden=${ordenId}`, { headers: headers, responseType: 'blob' });
  }
}
