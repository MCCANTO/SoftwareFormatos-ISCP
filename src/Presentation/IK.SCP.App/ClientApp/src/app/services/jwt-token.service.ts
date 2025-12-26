import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { UserData } from '../core/models/user-data.interface';
import { LocalStorageService } from './local-storage.service';
import { eRolSCP } from '../core/enums/rol.enum';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {
  private token_name: string = 'ik.scp.current_user';
  private decodedToken!: { [key: string]: string };

  constructor(
    private localStorageService: LocalStorageService
  ) { }

  getToken() {
    return this.localStorageService.get(this.token_name);
  }

  setToken(token: string) {
    if (token) {
      this.localStorageService.set(this.token_name, token);
    }
  }

  removeToken() {
    this.localStorageService.remove(this.token_name);
  }

  decodeToken() {
    const jwtToken = this.localStorageService.get(this.token_name);
    if (jwtToken) {
      this.decodedToken = jwt_decode(jwtToken);
    }
  }

  getDecodeToken(): UserData {
    this.decodeToken();
    const data_decoded = this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata'];
    const data = JSON.parse(data_decoded);

    // const data: UserData = {
    //   Usuario: 'SYSTEM',
    //   Nombre: 'PUNTAESTRELLA EXT',
    //   Modulo: '',
    //   Linea: 1,
    //   LineaDesc: 'ENVASADORA 1',
    //   Rol:22,
    //   RolDesc: 'PUNTA ESTRELLA',
    //   Articulo: '2163IC000008 - ZANAHORIA FRITA INKA CROPS SIN SAL HOSO CJA X 01 UND X BLS 5KG - P/ID',
    //   Cantidad: '17.00 CJ',
    //   Fecha: new Date('2022-02-22 10:37:34.227'),
    //   Orden: 'EN162706'
    // };

    return data;
  }

  getUser() {
    const data = this.getDecodeToken();
    return data['Usuario'];
  }

  getLinea(): number {
    const data = this.getDecodeToken();
    return data['Linea'];
  }

  getOrden() {
    const data = this.getDecodeToken();
    return data['Orden'];
  }

  getPerfil() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['displayname'] : null;
  }

  getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['email'] : null;
  }

  getRol(): number {
    const data = this.getDecodeToken();
    return data['Rol'];
  }

  getOpciones(idPadre: number = 0): any[] {
    const data: any = this.getDecodeToken();
    return data.Opciones.filter((x: any) => idPadre === 0 || x.NodoPadre === idPadre);
  }

  getAcciones(): any[] {
    const data: any = this.getDecodeToken();
    return data.Acciones;
  }

  getAccionesXOpcion(idOpcion: number): any[] {
    const data: any = this.getDecodeToken();
    return data.Acciones.filter((x: any) => x.OpcionId === idOpcion);
  }

  tieneAccesoAccion(accion: string): boolean {
    const acciones = this.getAcciones();
    return acciones.some((t: any) => t.accionId === accion);
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['exp'] : null;
  }

  isTokenExpired(): boolean {
    const expiryTime: number = Number(this.getExpiryTime());
    if (expiryTime) {
      return ((1000 * expiryTime) - (new Date()).getTime()) < 5000;
    } else {
      return false;
    }
  }

  esMaquinistaEnv(): boolean {
    return this.getRol() === eRolSCP.MAQ_ENV;
  }

  esPuntaEstrellaEnv(): boolean {
    return this.getRol() === eRolSCP.PTE_ENV;
  }

  esFacilitadorEnv(): boolean {
    return this.getRol() === eRolSCP.FAC_ENV;
  }

  esInspector(): boolean {
    return false
  }

  esMaquinistaFr(): boolean {
    return this.getRol() === eRolSCP.MAQ_FR;
  }

  esSaborizador(): boolean {
    return this.getRol() === eRolSCP.SAB;
  }

}
