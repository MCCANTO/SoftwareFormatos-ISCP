import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root',
})
export class ControlPefService {
  private URL_BASE = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl + environment.api_base + '/acondicionamiento/control-pef';
  }

  get(OrdenId: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}?ordenId=${OrdenId}`);
  }

  create(data: any) {
    return this.http.post<Response<any>>(this.URL_BASE, data);
  }

  update(data: any) {
    return this.http.put<Response<any>>(this.URL_BASE, data);
  }

  getAllCondicionPreviaDetalle(id: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/condicion-previa/${id}`
    );
  }

  saveCondicionPrevia(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/condicion-previa`,
      data
    );
  }

  saveFuerzaCorte(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/fuerza-corte`, data);
  }

  getAllFuerzaCorteDetalle(id: number) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/fuerza-corte/${id}`);
  }

  saveTiempo(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/tiempo`, data);
  }

  printControlPEF(ordenId:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
