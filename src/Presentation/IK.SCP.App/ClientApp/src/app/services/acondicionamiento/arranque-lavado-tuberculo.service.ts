import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class ArranqueLavadoTuberculoService {
  private URL_BASE = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl +
      environment.api_base +
      '/acondicionamiento/arranque-lavado-tuberculo';
  }

  getAllArranqueLavadoTuberculo(ordenId: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}?ordenId=${ordenId}`);
  }

  getByIdArranqueLavadoTuberculo(arranqueLavadoTuberculoId: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/detalle/${arranqueLavadoTuberculoId}`
    );
  }

  getArranqueLavadoTuberculoActivo(ordenId: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/${ordenId}`);
  }

  insertArranqueLavadoTuberculoActivo(data: any) {
    return this.http.post<Response<any>>(this.URL_BASE, data);
  }

  closeArranqueLavadoTuberculoActivo(data: any) {
    return this.http.put<Response<any>>(`${this.URL_BASE}/cierre`, data);
  }

  getAllVerificacionEquipoDetalle(id: number) {
    return this.http.get<Response<number>>(
      `${this.URL_BASE}/verificacion-equipo/${id}`
    );
  }

  saveVerificacionEquipoDetalle(data: any) {
    return this.http.put<Response<number>>(
      `${this.URL_BASE}/verificacion-equipo`,
      data
    );
  }

  saveCondicionPrevia(data: any) {
    return this.http.put<Response<number>>(
      `${this.URL_BASE}/condicion-previa`,
      data
    );
  }

  printCheckListArranqueLavadoTuberculos(ArranqueLavadoTuberculoId:number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ArranqueLavadoTuberculoId=${ArranqueLavadoTuberculoId}`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
