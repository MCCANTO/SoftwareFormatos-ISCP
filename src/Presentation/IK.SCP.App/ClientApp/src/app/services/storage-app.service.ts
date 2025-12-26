import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { LocalStorageConstant } from '../core/constants/local-storage.constant';
import { DataEnvasadoStorage } from '../core/models/envasado/envasado-data';
import { DataFrituraStorage } from '../core/models/fritura/data-orden.interface';
import { DataEnvasadoGranelStorage } from '../core/models/envasado/envasado-granel-data';
import { DataSazonadoStorage } from '../core/models/sazonado/sazonador.interface';
import { DataAcondicionamientoStorage } from '../core/models/acondicionamiento/acondicionamiento-data';

@Injectable({
  providedIn: 'root'
})
export class StorageAppService {

  constructor(
    private localStorageService: LocalStorageService
  ) { }

  get DataAcondicionamiento(): DataAcondicionamientoStorage {
    return JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ACONDICIONAMIENTO) ?? '');
  }

  set DataAcondicionamiento(value: DataAcondicionamientoStorage) {
    this.localStorageService.set(LocalStorageConstant.DATA_ORDEN_ACONDICIONAMIENTO, JSON.stringify(value));
  }

  get DataEnvasado(): DataEnvasadoStorage {
    return JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ENVASADO) ?? '');
  }

  set DataEnvasado(value: DataEnvasadoStorage) {
    this.localStorageService.set(LocalStorageConstant.DATA_ORDEN_ENVASADO, JSON.stringify(value));
  }
  
  get DataEnvasadoGranel(): DataEnvasadoGranelStorage {
    return JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ENVASADO_GRANEL) ?? '');
  }
  
  set DataEnvasadoGranel(value: DataEnvasadoGranelStorage) {
    this.localStorageService.set(LocalStorageConstant.DATA_ORDEN_ENVASADO_GRANEL, JSON.stringify(value));
  }

  get DataFritura(): DataFrituraStorage{
    return JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_FRITURA) ?? '');
  }

  set DataFritura(value: DataFrituraStorage) {
    this.localStorageService.set(LocalStorageConstant.DATA_ORDEN_FRITURA, JSON.stringify(value));
  }

  get DataSazonado(): DataSazonadoStorage{
    return JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_SAZONADO) ?? '');
  }

  set DataSazonado(value: DataSazonadoStorage) {
    this.localStorageService.set(LocalStorageConstant.DATA_ORDEN_SAZONADO, JSON.stringify(value));
  }

}
