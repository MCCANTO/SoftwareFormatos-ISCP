import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class ControlMaizService {
  private URL_BASE = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl + environment.api_base + '/acondicionamiento/control-maiz';
  }

  getAllIngresoMateriaPrima(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/materia-prima?ordenId=${ordenId}`
    );
  }

  insertIngresoMateriaPrima(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/materia-prima`,
      data
    );
  }

  getAllIngresoInsumo(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/insumo?ordenId=${ordenId}`
    );
  }

  insertIngresoInsumo(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/insumo`, data);
  }

  getAllObservacion(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/observacion?ordenId=${ordenId}`
    );
  }

  insertObservacion(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/observacion`, data);
  }

  getAllPelado(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/pelado?ordenId=${ordenId}`
    );
  }

  savePelado(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/pelado`, data);
  }

  getAllRemojo(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/remojo?ordenId=${ordenId}`
    );
  }

  insertRemojo(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/remojo`, data);
  }

  getAllSancochado(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/sancochado?ordenId=${ordenId}`
    );
  }

  insertSancochado(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/sancochado`, data);
  }

  getAllReposo(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/reposo?ordenId=${ordenId}`
    );
  }

  getSancochadoReposo(ordenId: string, numeroBatch: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/reposo/sancochado?ordenId=${ordenId}&numeroBatch=${numeroBatch}`
    );
  }

  insertReposo(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/reposo`, data);
  }

  printControlReposoMaiz(ordenId:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/reposo/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }

  printControlMaiz(ordenId:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
