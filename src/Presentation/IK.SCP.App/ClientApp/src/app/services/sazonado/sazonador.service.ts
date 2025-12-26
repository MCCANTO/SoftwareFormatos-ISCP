import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Arranque, BandejaSazonado, Sazonador } from 'src/app/core/models/sazonado/sazonador.interface';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';

@Injectable({
  providedIn: 'root'
})
export class SazonadorService {
  private URL_BASE = environment.api_base + '/sazonado'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + `${environment.api_base}/sazonado`;
  }

  getAllSazonador() {
    return this.http.get<Response<Sazonador[]>>(`${this.URL_BASE}/linea`);
  }

  getAllSazonadorFritura(idSazonador: number) {
    return this.http.get<Response<any[]>>(`${this.URL_BASE}/freidora?SaborizadorId=${idSazonador}`);
  }


  getAllArranque( sazonadorId: number, fecha: Date | null, linea?: number, producto?: string, sabor?: string ) {

    let filter = `sazonadorId=${sazonadorId}`
    if (fecha) filter += `&fecha=${fecha}`;
    if (linea) filter += `&linea=${linea}`;
    if (producto) filter += `&producto=${producto}`;
    if (sabor) filter += `&sabor=${sabor}`

    return this.http.get<Response<BandejaSazonado[]>>(`${this.URL_BASE}/arranque?${ filter }`);
  }

  getByIdArranque(id: number) {
    return this.http.get<Response<Arranque>>(`${this.URL_BASE}/arranque/${id}`);
  }

  insertArranque( data: any ) {
    const body = { ...data };
    return this.http.post<Response<any>>(`${this.URL_BASE}/arranque`, body);
  }

  closeArranque( data: any ) {
    return this.http.put<Response<any>>(`${this.URL_BASE}/arranque`, data);
  }

  insertArranqueCondicion( data: any ) {
    const body = { ...data };
    return this.http.post<Response<number>>(`${this.URL_BASE}/arranque/condicion`, body);
  }

  insertArranqueObservacion( data: any ) {
    return this.http.post<Response<number>>(`${this.URL_BASE}/arranque/observacion`, data);
  }

  insertVariableBasica( data: any ) {
    return this.http.post<Response<number>>(`${this.URL_BASE}/arranque/variable-basica`, data);
  }

  getAllVerificacionEquipoDetalle( id: number ) {
    return this.http.get<Response<number>>(`${this.URL_BASE}/arranque/verificacion-equipo-detalle/${id}`);
  }

  saveVerificacionEquipoDetalle( data: any ) {
    return this.http.post<Response<number>>(`${this.URL_BASE}/arranque/verificacion-equipo-detalle`, data);
  }

  printTemplateArranqueSazonado(arranqueId:number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/pdf');
    let filtro = `?arranqueId=${arranqueId}`;
    return this.http.get(`${this.URL_BASE}/arranque/pdf${filtro}`, { headers: headers, responseType: 'blob' });
  }
}
