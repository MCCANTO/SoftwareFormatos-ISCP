import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StatusResponse } from 'src/app/core/models/response.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaestroService {

  URL_BASE = `${environment.api_base}/fritura`

  constructor(
    private http: HttpClient
  ) { }

  getAllPanelista() {
    return this.http.get<StatusResponse>(`${this.URL_BASE}/panelista`);
  }

}
