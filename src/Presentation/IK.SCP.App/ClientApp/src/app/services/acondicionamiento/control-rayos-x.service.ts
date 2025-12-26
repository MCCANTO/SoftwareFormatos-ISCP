import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class ControlRayosXService {
  private URL_BASE = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE =
      baseUrl + environment.api_base + '/acondicionamiento/control-rayos-x';
  }

  getAll(periodo: string) {
    return this.http.get<Response<any>>(`${this.URL_BASE}?periodo=${periodo}`);
  }

  insert(data: any) {
    return this.http.post(this.URL_BASE, data);
  }

  review(ids: number[]) {
    return this.http.put(this.URL_BASE, { ids });
  }

  printControlMonitoreoRayosX(periodo:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?periodo=${periodo}`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
