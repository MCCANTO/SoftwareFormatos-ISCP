import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { ACCION_FR_CONTROL_ACEITE } from 'src/app/core/constants/accion.constant';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import {
  DataEnvasado,
  DataEnvasadoStorage,
  Envasadora,
} from 'src/app/core/models/envasado/envasado-data';
import {
  DataEnvasadoGranel,
  DataEnvasadoGranelStorage,
} from 'src/app/core/models/envasado/envasado-granel-data';
import {
  DataFritura,
  DataFrituraStorage,
  Freidora,
} from 'src/app/core/models/fritura/data-orden.interface';
import {
  DataSazonadoStorage,
  Sazonador,
} from 'src/app/core/models/sazonado/sazonador.interface';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { OrdenService } from 'src/app/services/envasado/orden.service';
import { OrdenService as OrdenFRService } from 'src/app/services/fritura/orden.service';
import { SazonadorService } from 'src/app/services/sazonado/sazonador.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
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

  dataControlAceite: DataFritura = {
    freidoraId: 0,
    orden: '',
  };

  sazonador: any;

  ACONDICIONAMIENTO = eModulo.ACONDICIONAMIENTO;
  FRITURA = eModulo.FRITURA;
  SAZONADO = eModulo.SAZONADO;
  ENVASADO = eModulo.ENVASADO;
  ENVASADO_GRANEL = eModulo.ENVASADO_GRANEL;
  CONTROL_ACEITE = eModulo.CONTROL_ACEITE;

  modulos: any[] = [];

  PERMISOS_CTRL_ACEITE: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  constructor(
    private ordenService: OrdenService,
    private ordenFRService: OrdenFRService,
    private sazonadorService: SazonadorService,
    private router: Router,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,
    private storageAppService: StorageAppService,
    private securityService: SecurityService
  ) {
    this.securityService
      .validarAcciones([ACCION_FR_CONTROL_ACEITE])
      .then((resp) => {
        this.PERMISOS_CTRL_ACEITE = resp[0];
      });
  }

  ngOnInit(): void {
    this.cargarFreidoras();
    this.cargarEnvasadoras();
    this.cargarSazonadoras();

    this.cargarModulos();
  }

  estiloModulo(nodoId: number): string {
    let estilo = '';
    switch (nodoId) {
      case this.ACONDICIONAMIENTO:
        estilo = 'bg-aco';
        break;
      case this.ENVASADO:
        estilo = 'bg-env';
        break;
      case this.ENVASADO_GRANEL:
        estilo = 'bg-env-gra';
        break;
      case this.FRITURA:
        estilo = 'bg-fr';
        break;
      case this.SAZONADO:
        estilo = 'bg-saz';
        break;
      case this.CONTROL_ACEITE:
        estilo = 'bg-fr';
        break;
      default:
        break;
    }
    return estilo;
  }

  cargarModulos() {
    const opciones = this.securityService.listarOpciones(0);
    this.modulos = opciones.filter((x: any) => x.TipoNodoId === 6);
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
      };

      this.storageAppService.DataSazonado = data;

      this.router.navigate(['/sazonado']);
    }
  }

  goToAcondicionamiento() {
    this.router.navigate(['/acondicionamiento']);
  }

  goToControlAceite() {
    if (!this.dataControlAceite.freidoraId) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Mensaje Advertencia',
        detail: 'Debe seleccionar la Línea de Fritura',
      });
      return;
    }

    const freidora = this.freidoras.filter(
      (f) => f.Id === this.dataControlAceite.freidoraId
    )[0];

    const data: DataFrituraStorage = {
      freidora,
    };

    this.storageAppService.DataFritura = data;
    this.router.navigate(['/fritura/control-aceite']);
  }
}
