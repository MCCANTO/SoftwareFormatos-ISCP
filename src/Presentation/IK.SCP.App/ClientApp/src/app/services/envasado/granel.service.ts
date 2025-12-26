import { Inject, Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Response } from '../../core/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class GranelService {
  private URL_BASE = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.URL_BASE = baseUrl + `${environment.api_base}/envasado-granel`;
  }

  getAllChecklist(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/checklists?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  getChecklist(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/checklist?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  getChecklistById(arranqueGranelId: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/checklist/${arranqueGranelId}`
    );
  }

  postChecklist(envasadoraId: number, orden: string) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/checklist`, {
      envasadoraId,
      orden,
    });
  }

  closeChecklist(id: number) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/checklist/cierre/${id}`,
      {}
    );
  }

  saveDatosPrincipales(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/checklist/datos`,
      data
    );
  }

  getChecklistEspecificaciones(id: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/checklist/especificacion?arranqueGranelId=${id}`
    );
  }

  saveEspecificaciones(especificaciones: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/checklist/especificacion`,
      { especificaciones }
    );
  }

  getCondicionesOperativasDetalle(id: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/condicion-operativa/${id}`
    );
  }

  saveCondicionesOperativasDetalle(data: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/condicion-operativa`,
      data
    );
  }

  getCondicionesProcesoDetalle(id: number) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/condicion-proceso/${id}`
    );
  }

  saveCondicionesProcesoDetalle(data: any) {
    return this.http.put<Response<any>>(
      `${this.URL_BASE}/condicion-proceso`,
      data
    );
  }

  saveObservacion(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/observacion`, data);
  }

  saveRevision(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/checklist/revision`,
      data
    );
  }

  // CONTROL DE PARAMETROS

  getAllGranelParametrosControl(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/control?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  saveGranelParametrosControl(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/control`, data);
  }

  getAllGranelObservacionControl(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/control/observacion?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  saveGranelObservacionControl(data: any) {
    return this.http.post<Response<any>>(
      `${this.URL_BASE}/control/observacion`,
      data
    );
  }

  getAllParametrosControl() {
    return this.http.get<Response<any>>(`${this.URL_BASE}/parametros-control`);
  }

  // EVALUACION DE PT

  getAllGranelEvaluacionPT(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/evaluacion?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  getGranelEvaluacionPT(id: number) {
    return this.http.get<Response<any>>(`${this.URL_BASE}/evaluacion/${id}`);
  }

  saveGranelEvaluacionPT(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/evaluacion`, data);
  }

  // Codificacion de Caja

  getAllGranelCodificacion(envasadoraId: number, orden: string) {
    return this.http.get<Response<any>>(
      `${this.URL_BASE}/codificacion?envasadoraId=${envasadoraId}&orden=${orden}`
    );
  }

  saveGranelCodificacion(data: any) {
    return this.http.post<Response<any>>(`${this.URL_BASE}/codificacion`, data);
  }

  printCheckListArranqueGranelEnvasado(id:number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ArranqueGranelId=${id}`;
    return this.http.get(`${this.URL_BASE}/checklist/PDF${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
