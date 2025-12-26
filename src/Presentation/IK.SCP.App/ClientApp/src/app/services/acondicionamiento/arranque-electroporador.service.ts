import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ArranqueElectroporadorService {
  private URL_BASE = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl + environment.api_base + '/acondicionamiento/checklist-pef';
  }

  getAll(ordenId: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}?ordenId=${ordenId}`);
  }

  getById(id: number) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/${id}`);
  }

  getOpen(ordenId: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/activo/${ordenId}`);
  }

  insert(data: any) {
    return this.http.post<Response<any>>(this.URL_BASE, data);
  }

  saveCondiciones(data: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/condicion-previa`,
      data
    );
  }

  getAllVerificacionDetalle(id: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/verificacion-equipo/${id}`
    );
  }

  saveVerificacionDetalle(data: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/verificacion-equipo`,
      data
    );
  }

  saveVariablesBasicas(data: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/variable-basica`,
      data
    );
  }

  close(data: any) {
    return this.http.put<Response<any>>(`${this.URL_BASE}/cierre`, data);
  }

  printCheckListArranqueElectroporador(ArranqueElectroporadorId) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ArranqueElectroporadorId=${ArranqueElectroporadorId}`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
