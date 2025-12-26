import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ParametroGeneralService {
  
  private URL_BASE = environment.api_base + '/envasado/parametrogeneral'

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this.URL_BASE = baseUrl + `${environment.api_base}/envasado/parametrogeneral`;
  }

  getAll(padreId: number) {
    return this.http.get(this.URL_BASE + '?padreId=' + padreId);
  }

}
