import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ArranqueMaquina } from 'src/app/core/models/fritura/arranque-maquina.interface';
import { StatusResponse, Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArranqueMaquinaService {

  private URL_BASE = environment.api_base + '/fritura/arranquemaquina'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + `${environment.api_base}/fritura/arranquemaquina`;
  }

  getAll(linea: number, orden: string) {
    return this.http.get(`${ this.URL_BASE }?linea=${ linea }&ordenId=${ orden }`);
  }

  getById( id: number ) {
    return this.http.get(`${ this.URL_BASE }/${ id }`);
  }

  getOpen( linea: number, orden: string, id: number = 0 ) {
    return this.http.get<Response<ArranqueMaquina>>(`${ this.URL_BASE }/activo?linea=${ linea }&orden=${ orden }&arranqueMaquinaId=${ id }`);
  }

  save( data: any ) {
    return this.http.post<Response<number>>( this.URL_BASE , {...data} );
  }

  update( data: any ) {

    const body = { ...data };

    return this.http.put<Response<number>>( this.URL_BASE , body );
  }

  insertObservacion( data: any ) {
    const body = { ...data };
    return this.http.post<Response<number>>( this.URL_BASE + '/observacion', body );
  }

  saveCondicionePrevia( data: any ) {
    const body = { ...data };
    return this.http.post<Response<number>>( this.URL_BASE + '/condicionprevia', body );
  }

  generateFilePDFArranqueManufactura(orden:string, articulo:string, idArranqueMaquina: number, linea:number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    return this.http.get(`${this.URL_BASE}/PDF/${orden}/${linea}/${idArranqueMaquina}/${articulo}`, { headers: headers, responseType: 'blob' });
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
