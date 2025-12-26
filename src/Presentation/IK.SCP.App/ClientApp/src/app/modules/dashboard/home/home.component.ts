import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';

import {
  DataEnvasado,
  DataEnvasadoStorage,
  Envasadora,
} from 'src/app/core/models/envasado/envasado-data';
import {
  DataFritura,
  DataFrituraStorage,
  Freidora,
} from 'src/app/core/models/fritura/data-orden.interface';
import { OrdenService } from 'src/app/services/envasado/orden.service';
import { OrdenService as OrdenFRService } from 'src/app/services/fritura/orden.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';

import { SazonadorService } from '../../../services/sazonado/sazonador.service';
import { DataSazonadoStorage, Sazonador } from '../../../core/models/sazonado/sazonador.interface';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import {
  DataEnvasadoGranel,
  DataEnvasadoGranelStorage,
} from 'src/app/core/models/envasado/envasado-granel-data';
import { StorageAppService } from 'src/app/services/storage-app.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [MessageService],
})
export class HomeComponent implements OnInit {
  envasadoras: Envasadora[] = [];
  envasadoras_granel: Envasadora[] = [];
  freidoras: Freidora[] = [];
  sazonadores: Sazonador[] = [];

  dataEnvasado: DataEnvasado = {
    envasadoraId: 0,
    orden: '',
  };

  dataEnvasadoGranel: DataEnvasadoGranel = {
    envasadoraId: 4,
    orden: '',
  };

  dataFritura: DataFritura = {
    freidoraId: 0,
    orden: '',
  };

  sazonador: any;

  ACONDICIONAMIENTO = eModulo.ACONDICIONAMIENTO;
  FRITURA = eModulo.FRITURA;
  SAZONADO = eModulo.SAZONADO;
  ENVASADO = eModulo.ENVASADO;
  ENVASADO_GRANEL = eModulo.ENVASADO_GRANEL;

  modulos: any[] = [];

  constructor(
    private ordenService: OrdenService,
    private ordenFRService: OrdenFRService,
    private sazonadorService: SazonadorService,
    private router: Router,
    // private localStorageService: LocalStorageService,
    public jwtTokenService: JwtTokenService,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    this.cargarFreidoras();
    this.cargarEnvasadoras();
    this.cargarSazonadoras();

    this.cargarModulos();
  }

  cargarModulos() {
    const opciones = this.jwtTokenService.getOpciones();
    this.modulos = opciones.filter((x: any) => x.TipoNodoId === 6);
  }

  get EsUsuarioFR() {
    return this.jwtTokenService.esMaquinistaFr();
  }

  get EsUsuarioSAB() {
    return this.jwtTokenService.esSaborizador();
  }

  get EsUsuarioENV() {
    return (
      this.jwtTokenService.esMaquinistaEnv() ||
      this.jwtTokenService.esFacilitadorEnv() ||
      this.jwtTokenService.esPuntaEstrellaEnv()
    );
  }

  cargarSazonadoras() {
    this.sazonadorService.getAllSazonador().subscribe((resp) => {
      this.sazonadores = resp.data;
    });
  }

  cargarEnvasadoras() {
    this.ordenService.getAllEnvasadora().subscribe((resp) => {
      const envasadoras = resp.data;
      this.envasadoras = envasadoras.filter((x: Envasadora) => x.Id != 4);
      this.envasadoras_granel = envasadoras.filter(
        (x: Envasadora) => x.Id == 4
      );
    });
  }

  cargarFreidoras() {
    this.ordenFRService.getAllFreidora().subscribe((resp) => {
      this.freidoras = resp.data;
    });
  }

  goToFritura() {
    if (!this.dataFritura.freidoraId) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Mensaje Advertencia',
        detail: 'Debe seleccionar la Línea de Fritura',
      });
    }

    this.ordenFRService
      .getByIdOrden(this.dataFritura.freidoraId, this.dataFritura.orden)
      .subscribe((resp) => {
        if (resp.ok) {
          if (resp.data && resp.data.Orden) {
            const freidora = this.freidoras.filter(
              (f) => f.Id === this.dataFritura.freidoraId
            )[0];

            const data: DataFrituraStorage = {
              freidora,
              orden: resp.data,
            };

            this.storageAppService.DataFritura = data;

            this.router.navigate(['/fritura']);
          } else {
            this.messageService.add({
              severity: 'warn',
              summary: 'Mensaje Advertencia',
              detail: 'No se encontró información de la Orden de Fritura',
            });
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Mensaje de Error',
            detail: 'No se ha podido obtener la información en consulta.',
          });
        }
      });
  }

  goToEnvasado() {
    this.ordenService
      .getByIdOrden(this.dataEnvasado.orden)
      .subscribe((resp) => {
        if (resp.ok) {
          const envasadora = this.envasadoras.filter(
            (p) => p.Id === this.dataEnvasado.envasadoraId
          )[0];

          const data: DataEnvasadoStorage = {
            envasadora,
            orden: resp.data,
          };

          this.storageAppService.DataEnvasado = data;

          this.router.navigate(['/envasado']);
        }
      });
  }

  goToEnvasadoGranel() {
    this.ordenService
      .getByIdOrden(this.dataEnvasadoGranel.orden)
      .subscribe((resp) => {
        if (resp.ok) {
          const envasadora = this.envasadoras_granel.filter(
            (p) => p.Id === this.dataEnvasadoGranel.envasadoraId
          )[0];

          const data: DataEnvasadoGranelStorage = {
            envasadora,
            orden: resp.data,
          };

          this.storageAppService.DataEnvasadoGranel = data;

          this.router.navigate(['/envasado-granel']);
        }
      });
  }

  goToSaborizado() {
    if (this.sazonador) {
      const sazonador = this.sazonadores.filter(
        (p) => p.sazonadorId === this.sazonador
      )[0];

      const data: DataSazonadoStorage = {
        sazonador: sazonador,
      }

      this.storageAppService.DataSazonado = data; 

      this.router.navigate(['/sazonado']);
    }
  }

  goToAcondicionamiento() {
    this.router.navigate(['/acondicionamiento']);
  }
}
