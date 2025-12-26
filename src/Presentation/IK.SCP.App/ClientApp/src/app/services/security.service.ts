import { Inject, Injectable } from '@angular/core';
import { JwtTokenService } from './jwt-token.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Response } from 'src/app/core/models/response.interface';
import { lastValueFrom } from 'rxjs';
import { PermisoAccion } from '../core/models/security/security.interface';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  private URL_BASE = '';

  constructor(
    private http: HttpClient,
    private jwtTokenService: JwtTokenService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + environment.api_base + '/seguridad';
  }

  listarOpciones(idPadre: number) {
    return this.jwtTokenService.getOpciones(idPadre);
  }

  usuarioActivo() {
    return this.jwtTokenService.getUser();
  }

  async validarAcciones(acciones: any[]): Promise<PermisoAccion[]> {
    const rolId = this.jwtTokenService.getRol();

    const resp = await lastValueFrom(
      this.http.get<Response<any>>(`${this.URL_BASE}/acciones?RolId=${rolId}`)
    );

    const accionesHabilitadas = resp.data ?? [];

    let result: PermisoAccion[] = [];

    acciones.forEach((accion: any) => {
      result.push({
        LECTURA: accionesHabilitadas.some(
          (t: any) => t.AccionId === accion.LECTURA
        ),
        ESCRITURA: accionesHabilitadas.some(
          (t: any) => t.AccionId === accion.ESCRITURA
        ),
        REVISION: accionesHabilitadas.some(
          (t: any) => t.AccionId === accion.REVISION
        ),
      });
    });

    return result;
  }
}
