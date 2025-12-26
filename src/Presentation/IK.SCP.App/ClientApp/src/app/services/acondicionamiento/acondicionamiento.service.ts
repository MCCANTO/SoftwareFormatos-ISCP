import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class AcondicionamientoService {
  private URL_BASE = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE = baseUrl + environment.api_base + '/acondicionamiento';
  }

  getAllMateriaPrima() {
    return this.http.get<Response<any>>(`${this.URL_BASE}/materia-prima`);
  }

  getAllProcesoMateriaPrima(materiaPrimaId: number) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/proceso-materia-prima?materiaPrimaId=${materiaPrimaId}`);
  }

  getAllOrdenes(
    ordenId: string,
    fechaInicio: string,
    fechaFin: string,
    materiaPrimaId: number
  ) {
    let filtro = `?ordenId=${ordenId}&materiaPrimaId=${materiaPrimaId}`;
    if (fechaInicio) filtro += `&fechaInicio=${fechaInicio}`;
    if (fechaFin) filtro += `&fechaFin=${fechaFin}`;
    return this.http.get<Response<any>>(`${this.URL_BASE}/orden${filtro}`);
  }

  insertOrden(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/orden`, data);
  }
}
