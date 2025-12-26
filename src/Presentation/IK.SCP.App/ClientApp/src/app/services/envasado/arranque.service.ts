import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StatusResponse } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArranqueService {

  private URL_BASE = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + `${environment.api_base}/envasado/arranque`;
  }

  getArranque(envasadoraId: number, orden: string): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}?envasadoraId=${ envasadoraId }&orden=${ orden }` );
  }

  save(data: any): Observable<StatusResponse> {
    return this.http.post<StatusResponse>(this.URL_BASE, { ...data } );
  }

  postArranqueActivo(data: any): Observable<StatusResponse> {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/activo`, { ...data } );
  }

  getAllCondicionPrevia( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/condicionprevia?arranqueId=` + arranqueId );
  }

  getAllVariableBasica( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/variablebasica?arranqueId=` + arranqueId );
  }

  getVariableBasica( arranqueVariableBasicaCabId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/variablebasica/${arranqueVariableBasicaCabId}`);
  }

  getAllCodificacion( arranqueId: number, tipo: string ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/codificacion?arranqueId=${arranqueId}&tipoCodificacion=${tipo}`);
  }

  getAllContramuestra( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/contramuestra?arranqueId=` + arranqueId );
  }

  getAllPersonal( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/personal?arranqueId=` + arranqueId );
  }

  getAllComponente( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/componente?arranqueId=` + arranqueId );
  }

  getAllObservacion( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/observacion?arranqueId=` + arranqueId );
  }

  getAllInspeccion( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/inspeccion?arranqueId=` + arranqueId );
  }

  getAllRevision( arranqueId: number ): Observable<StatusResponse> {
    return this.http.get<StatusResponse>( `${this.URL_BASE}/revision?arranqueId=` + arranqueId );
  }

  postCodificacion(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/codificacion`, { ...data } );
  }

  postContramuestra(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/contramuestra`, { ...data } );
  }

  postPersonal(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/personal`, { ...data } );
  }

  postComponente(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/componente`, { ...data } );
  }

  postObservacion(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/observacion`, { ...data } );
  }

  postInspeccion(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/inspeccion`, { ...data } );
  }

  postRevision(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/revision`, { ...data } );
  }

  putCondicionesPrevias(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/condicionprevia`, { condiciones: data } );
  }

  postVariableBasica(data: any) {
    return this.http.post<StatusResponse>( `${this.URL_BASE}/variablebasica`, { ...data } );
  }

  printCheckListArranqueEnvasado(envasadoraId: number, orden: string, idArranqueEnvasado: number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?envasadoraId=${ envasadoraId }&orden=${ orden }&arranqueId=${ idArranqueEnvasado }`;
    return this.http.get(`${this.URL_BASE}/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }

  getAllArranque(envasadoraId: number, ordenId: string) {
    return this.http.get(`${ this.URL_BASE }/all?envasadoraId=${ envasadoraId }&orden=${ ordenId }`);
  }


}
