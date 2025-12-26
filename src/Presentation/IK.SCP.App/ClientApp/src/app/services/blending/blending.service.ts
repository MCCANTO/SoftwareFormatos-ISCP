import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root'
})
export class BlendingService {
  private URL_BASE = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + environment.api_base + '/blending'
  }

  validateMezcla( articulo: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/validacion?articulo=${articulo}`);
  }

  getAllComponentes( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/componentes?orden=${orden}`);
  }

  getAllArranques( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/arranque?orden=${orden}`);
  }

  getArranqueActivo( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/arranque/activo?orden=${orden}`);
  }

  getArranqueById( id: number ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/arranque/${id}`);
  }

  postArranque( data: any ) {
    return this.http.post<Response<any>>(this.URL_BASE + `/arranque`, data);
  }

  getAllVerificacionEquipoDetalle( id: number ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/verificacion-equipo-detalle/${id}`);
  }

  saveVerificacionEquipoDetalle( data: any ) {
    return this.http.post<Response<any>>(this.URL_BASE + `/verificacion-equipo-detalle`, data);
  }

  insertArranqueObservacion( data: any ) {
    return this.http.post<Response<any>>(this.URL_BASE + `/observacion`, data);
  }

  insertArranqueCondicion( data: any ) {
    return this.http.put<Response<any>>(this.URL_BASE + `/condicion-previa`, data);
  }

  closeArranque( data: any ) {
    return this.http.put<Response<any>>(this.URL_BASE + `/arranque/cierre`, data);
  }




  getAllControlComponentes( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/control-componentes?orden=${ orden }`);
  }

  insertControlComponente( data: any ) {
    return this.http.post<Response<any>>(this.URL_BASE + `/control-componentes`, data);
  }

  updateControlComponente( data: any ) {
    return this.http.put<Response<any>>(this.URL_BASE + `/control-componentes`, data);
  }


  getAllControl( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/control?orden=${ orden }`);
  }

  insertControl( data: any ) {
    return this.http.post<Response<any>>(this.URL_BASE + `/control`, data);
  }

  getAllControlMerma( orden: string ) {
    return this.http.get<Response<any>>(this.URL_BASE + `/control-merma?orden=${ orden }`);
  }

  updateControlMerma( data: any ) {
    return this.http.put<Response<any>>(this.URL_BASE + `/control-merma`, data);
  }

  printChecklistArranqueBlending(id:number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ArranqueVerificacionEquipoId=${id}`;
    return this.http.get(`${this.URL_BASE}/arranque/PDF${filtro}`, { headers: headers, responseType: 'blob' });
  }

  printControlBlending(orden:string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?orden=${orden}`;
    return this.http.get(`${this.URL_BASE}/control/PDF${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
