import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Inject, Injectable } from '@angular/core';
import { StatusResponse, Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArranqueMaquinaService {

  private URL_BASE = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + environment.api_base + '/envasado/arranquemaquina'
  }

  getAll(envasadoraId: number, ordenId: string) {
    return this.http.get(`${ this.URL_BASE }?envasadoraId=${ envasadoraId }&ordenId=${ ordenId }`);
  }

  getById( id: number ) {
    return this.http.get(`${ this.URL_BASE }/${ id }`);
    }

  getOpen( envasadoraId: number, ordenId: string ) {
    return this.http.get<StatusResponse>(`${ this.URL_BASE }/abierto?envasadoraId=${ envasadoraId }&ordenId=${ ordenId }`);
  }

  save( data: any ) {
    return this.http.post<Response<number>>( this.URL_BASE , {...data} );
  }

  getAllCondicionesById( id: number ) {
    return this.http.get<Response<any>>( `${ this.URL_BASE }/condicionprevia?id=${ id }` );
  }

  saveCondicionesPrevias( data: any ) {
    return this.http.post<Response<number>>( `${ this.URL_BASE }/condicionprevia`, {...data } );
  }

  getAllVariablesById( id: number ) {
    return this.http.get<Response<any>>( `${ this.URL_BASE }/variablebasica?id=${ id }` );
  }

  saveVariablesBasicas( data: any ) {
    return this.http.post<Response<number>>( `${ this.URL_BASE }/variablebasica`, {...data } );
  }

  saveObservacion( data: any ) {
    return this.http.post<Response<number>>( `${ this.URL_BASE }/observacion`, {...data } );
  }

  printArranqueMaquinaEnvasado(ArranqueMaquinaId: number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?ArranqueMaquinaId=${ArranqueMaquinaId}`;
    console.log("Imprimir melas")
    return this.http.get(`${this.URL_BASE}/PDF${filtro}`, { headers: headers, responseType: 'blob' });
  }

  getFileExcelEnvasado() {
    console.log(this.URL_BASE);
    const httpOptions = {
      responseType: 'blob' as 'json', // Cambiamos el tipo de respuesta a 'blob'
      headers: new HttpHeaders({
        'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' // Ajusta el tipo de contenido a XLSX
      })
    };
    return this.http.get(`${this.URL_BASE}/EXCEL`, httpOptions);
  }

}
