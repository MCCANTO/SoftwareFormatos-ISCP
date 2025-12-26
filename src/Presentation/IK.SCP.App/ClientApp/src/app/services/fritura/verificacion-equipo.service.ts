import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ArranqueMaquinaVerificacionEquipo, VerificacionEquipo } from 'src/app/core/models/fritura/verificacion-equipo.interface';
import { StatusResponse, Response } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VerificacionEquipoService {
  
  private URL_BASE = environment.api_base + '/fritura/arranquemaquina/verificacionequipo'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this.URL_BASE = baseUrl + `${environment.api_base}/fritura/arranquemaquina/verificacionequipo`;
  }

  getAll(id: number) {
    return this.http.get<StatusResponse>(this.URL_BASE + `?arranqueMaquinaId=${ id }`);
  }

  getAllDetalle(tipoId: number, linea: number, id: number) {
    return this.http.get<Response<VerificacionEquipo>>(this.URL_BASE + `/detalle?tipoId=${tipoId}&linea=${linea}&arranqueMaquinaVerificacionEquipoCabId=${ id }`);
  }

  save( data: ArranqueMaquinaVerificacionEquipo ) {
    const body = { ...data };
    return this.http.post<Response<number>>(this.URL_BASE, body);
  }
  
}
