import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtTokenService } from './jwt-token.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private URL_BASE =  "";

  constructor(
    private http: HttpClient,
    private jwtTokenService: JwtTokenService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.URL_BASE = baseUrl + environment.api_base + "/auth"
  }

  login(usuario: string, clave: string) {

    const body = {
      usuario,
      clave,
    }

    return this.http.post(`${ this.URL_BASE }/validate`, body)
  }

  validate(usuario: string) {

    const body = {
      usuario
    }

    return this.http.post(`${ this.URL_BASE }/login`, body)
  }

  logout() {
    this.jwtTokenService.removeToken();
    this.router.navigate(['/page-not-found'])
  }

}
