import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ControlHabaService {
  private URL_BASE = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl + environment.api_base + '/acondicionamiento/control-haba';
  }

  getAllRemojo(ordenId: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/remojo?ordenId=${ordenId}`
    );
  }

  insertRemojo(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/remojo`, data);
  }

  printControlRemojoHabas(ordenId:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ordenId=${ordenId}`;
    return this.http.get(`${this.URL_BASE}/remojo/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
