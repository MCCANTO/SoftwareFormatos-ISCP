import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import {
  Freidora,
  OrdenFR,
} from 'src/app/core/models/fritura/data-orden.interface';
import { Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrdenService {
  private URL_BASE = environment.api_base + '/fritura';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE = baseUrl + `${environment.api_base}/fritura`;
  }

  getAllFreidora() {
    return this.http.get<Response<Freidora[]>>(`${this.URL_BASE}/linea`);
  }

  getByIdOrden(linea: number, orden: string) {
    return this.http.get<Response<OrdenFR>>(
      `${this.URL_BASE}/orden?lineaId=${linea}&orden=${orden}`
    );
  }

  getAllOrden(linea: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/ordenes?lineaId=${linea}&orden=${orden}`
    );
  }

  getAllOrdenConsumo(orden: string, clasificacion: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/orden/consumo?orden=${orden}&clasificacion=${clasificacion}`
    );
  }

  getAllControlAceite(fecha: Date[], lineaId: number, ordenId: string) {
    const desde = fecha[0];
    const hasta = fecha[1];
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/control-aceite?desde=${desde}&hasta=${hasta}&lineaId=${lineaId}&ordenId=${ordenId}`
    );
  }

  saveControlAceite(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/control-aceite`,
      data
    );
  }

  getAllDefectoCaracterizacion(articulo: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/defecto?articulo=${articulo}`
    );
  }

  getAllRegistroCaracterizacion(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/caracterizacion?ordenId=${ordenId}`
    );
  }

  saveRegistroCaracterizacion(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/caracterizacion`,
      data
    );
  }

  printControlParametrosCalidadAceite(fecha: Date[], lineaId: number, ordenId: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    const desde = fecha[0];
    const hasta = fecha[1];
    let filtro = `?desde=${desde}&hasta=${hasta}&lineaId=${lineaId}&ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/control-aceite/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }


  printCaracterizacionProductoTerminado(ordenId: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/caracterizacion/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
