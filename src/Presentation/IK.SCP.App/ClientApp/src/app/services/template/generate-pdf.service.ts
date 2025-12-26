import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneratePdfService {

  private URL_BASE = environment.api_base + '/fritura/evaluacionatributo'

  constructor(
    private http: HttpClient
  ) {
      
  }

  public getInformationTemplateByID(id:number) {
    return this.http.get(`${this.URL_BASE}/${id}`);
  }

}
