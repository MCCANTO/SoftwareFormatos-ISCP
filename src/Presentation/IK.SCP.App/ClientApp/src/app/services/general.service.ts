import { Injectable } from '@angular/core';
import * as crypto from 'crypto-js'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  private key = 'REFGDTGSgsgsttgsg1234565';  // environment.cryptoKey;

  encriptar( cadena: string ) {

    return crypto.AES.encrypt( cadena, this.key );

  }

  desencriptar( cadena: string ) {

    return crypto.AES.decrypt( cadena, this.key );
    
  }

}
