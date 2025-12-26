import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import {
  StatusResponse,
  Response,
} from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrdenService {
  private url_base = environment.api_base + '/envasado';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url_base = baseUrl + `${environment.api_base}/envasado`;
  }

  getAllEnvasadora() {
    return this.http.get<StatusResponse>(`${this.url_base}/envasadora`);
  }

  getByIdOrden(orden: string) {
    return this.http.get<Response<OrdenEnvasado>>(
      `${this.url_base}/orden/${orden}`
    );
  }

  getAllRegistroPedaceria(envasadoraId: number, ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.url_base}/registro-pedaceria?envasadoraId=${envasadoraId}&ordenId=${ordenId}`
    );
  }

  saveRegistroPedaceria(data: any) {
    return this.http.post<Response<any>>(
      `${this.url_base}/registro-pedaceria`,
      data
    );
  }

  printRegistrosPedaceria(envasadoraId: number, ordenId: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?envasadoraId=${envasadoraId}&ordenId=${ordenId}`;
    return this.http.get(`${this.url_base}/registro-pedaceria/PDF${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
