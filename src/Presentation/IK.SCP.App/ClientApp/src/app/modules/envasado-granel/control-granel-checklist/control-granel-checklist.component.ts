import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { CondicionComponent } from 'src/app/components/condicion/condicion.component';
import { VariableBasicaComponent } from 'src/app/components/variable-basica/variable-basica.component';
import { ACCION_ENV_GRANEL_CHECKLIST, NODO } from 'src/app/core/constants/accion.constant';
import { EnvasadoCondicionPrevia } from 'src/app/core/models/envasado/condicion-previa.model';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { GranelService } from 'src/app/services/envasado/granel.service';
import { OrdenService } from 'src/app/services/fritura/orden.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-control-granel-checklist',
  templateUrl: './control-granel-checklist.component.html',
  styleUrls: ['./control-granel-checklist.component.scss'],
  providers: [DatePipe],
})
export class ControlGranelChecklistComponent implements OnInit {

  NODO = NODO.ENV_GRANEL_CHECKLIST;

  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  };

  id: number = 0;

  dataOrden: any;

  condiciones = [];
  condicionesProceso = [];

  observaciones = [];
  observacion = '';

  especificaciones: any[] = [];

  tiposGranel = [
    { id: 'E', nombre: 'Exportación' },
    { id: 'L', nombre: 'Local' },
    { id: 'T', nombre: 'Tránsito' },
  ];

  tipoGranel = '';

  lineasFritura: any[] = [];
  lineaFritura = 0;

  fechaVctoLote = new Date();
  maquinista = '';
  selladora = '';

  personalPesa: string[] = [];
  personalSella: string[] = [];

  revisiones: string[] = [];
  revisado = false;

  constructor(
    private router: Router,
    private granelService: GranelService,
    private dialogService: DialogService,
    private storageAppService: StorageAppService,
    private confirmationService: ConfirmationService,
    private frituraService: OrdenService,
    public securityService: SecurityService,
    private datePipe: DatePipe
  ) {
    this.securityService.validarAcciones([
      ACCION_ENV_GRANEL_CHECKLIST,
    ]).then(resp => {
      this.PERMISOS = resp[0];
    });
  }

  ngOnInit(): void {
    this.cargarFreidoras();
    this.cargarData();
  }

  cargarFreidoras() {
    this.frituraService.getAllFreidora().subscribe((resp) => {
      if (resp.ok) {
        this.lineasFritura = resp.data;
      }
    });
  }

  cargarData() {
    const { envasadora, orden } = this.storageAppService.DataEnvasadoGranel;

    this.granelService
      .getChecklist(envasadora.Id, orden.Orden)
      .subscribe((resp) => {
        if (resp.success) {
          const data = resp.data;

          this.id = data.arranqueGranelId;

          this.listarEspecificaciones();
          this.tipoGranel = data.tipoId;
          this.fechaVctoLote = data.fechaVencimiento == null ? new Date() : new Date(data.fechaVencimiento);
          this.lineaFritura = data.lineaFrituraId;
          this.maquinista = data.maquinista;
          this.selladora = data.selladora;
          this.personalPesa = data.personalPesa?.split(',');
          this.personalSella = data.personalSella?.split(',');
          this.condiciones = data.condicionesOperativas;
          this.condicionesProceso = data.condicionesProceso;
          this.observaciones = data.observaciones;

          data.revisiones.forEach((rev: any) => {
            this.revisiones.push(
              `${rev.usuario} - ${this.datePipe.transform(
                rev.fecha,
                'dd/MM/yyyy HH:mm'
              )}`
            );
          });
        }
      });
  }

  guardarDatosPrincipales() {
    const data = {
      arranqueGranelId: this.id,
      tipoId: this.tipoGranel,
      fechaVencimiento: this.fechaVctoLote,
      lineaFrituraId: this.lineaFritura,
      maquinista: this.maquinista,
      selladora: this.selladora,
      personalPesa: this.personalPesa?.join(','),
      personalSella: this.personalSella?.join(','),
    };

    this.granelService.saveDatosPrincipales(data).subscribe((resp) => {
      // this.listarEspecificaciones();
    });
  }

  listarEspecificaciones() {
    this.granelService
      .getChecklistEspecificaciones(this.id)
      .subscribe((resp) => {
        if (resp.success) {
          this.especificaciones = resp.data;
        }
      });
  }

  guardarEspecificaciones() {
    const especificaciones: any[] = [];

    this.especificaciones.forEach((especificacion) => {
      especificaciones.push({
        ...especificacion,
        arranqueGranelId: this.id,
      });
    });

    this.granelService
      .saveEspecificaciones(especificaciones)
      .subscribe((resp) => {
        this.listarEspecificaciones();
      });
  }

  regresar() {
    this.router.navigate(['/envasado-granel']);
  }

  esOtro(esp: any): boolean {
    const result: boolean = esp.valores.some(
      (v: any) => v.nombre === 'OTRO' && v.id == esp.valor
    );
    if (!result) esp.otro = '';
    return result;
  }

  agregarCondicionOperativa() {
    this.listarCondicionesOperativas(0);
  }

  listarCondicionesOperativas(id: number) {
    this.granelService.getCondicionesOperativasDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const condicion: EnvasadoCondicionPrevia = {
          editable: id == 0,
          mostrarTipo: true,
          tipo: resp.data.tipoId,
          condiciones: resp.data.detalle,
          tipos: [],
        };

        const ref = this.dialogService.open(CondicionComponent, {
          header: 'Condiciones Operativas',
          width: '85%',
          data: {
            condicion,
          },
        });

        ref.onClose.subscribe((result: any) => {
          if (result) {
            const condiciones = {
              arranqueGranelId: this.id,
              tipoId: result.tipo,
              condiciones: result.condiciones,
            };

            this.guardarCondicionesOperativas(condiciones);
          }
        });
      }
    });
  }

  guardarCondicionesOperativas(condiciones: any) {
    this.granelService
      .saveCondicionesOperativasDetalle(condiciones)
      .subscribe((resp) => {
        this.cargarData();
      });
  }

  agregarCondicionProceso() {
    this.listarCondicionesProcesoDetalle(0);
  }

  listarCondicionesProcesoDetalle(id: number, cerrado: boolean = false) {
    this.granelService.getCondicionesProcesoDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const condicion = {
          variables: resp.data,
          mostrarTipo: false,
          editable: !cerrado,
          cerrado,
        };

        const ref = this.dialogService.open(VariableBasicaComponent, {
          header: 'Condiciones de Proceso',
          width: '95%',
          data: condicion,
        });

        ref.onClose.subscribe((result: any) => {
          if (result) {
            let condicionesProceso: any[] = [];
            result.variables.forEach((item: any) => {
              condicionesProceso.push(...item.items);
            });

            const dataCondicionesProceso = {
              arranqueGranelCondicionProcesoId: id,
              arranqueGranelId: this.id,
              condiciones: condicionesProceso,
            };

            this.guardarCondicionesProceso(dataCondicionesProceso);
          }
        });
      }
    });
  }

  guardarCondicionesProceso(condiciones: any) {
    this.granelService
      .saveCondicionesProcesoDetalle(condiciones)
      .subscribe((resp) => {
        this.cargarData();
      });
  }

  guardarObservacion() {
    if (this.observacion.length > 3) {
      const data = {
        arranqueGranelId: this.id,
        observacion: this.observacion,
      };

      this.granelService.saveObservacion(data).subscribe((resp) => {
        this.observacion = '';
        this.cargarData();
      });
    }
  }

  guardarRevision() {
    this.confirmationService.confirm({
      message:
        'Se guardará <b>conformidad</b> a los datos registrados. ¿Desea continuar con su confirmación?',
      accept: () => {
        const _body = {
          arranqueGranelId: this.id,
        };
        this.granelService.saveRevision(_body).subscribe((resp) => {
          this.revisado = true;
          this.cargarData();
        });
      },
    });
  }

  cerrar() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de cerrar el checklist? <p>Una vez <b>cerrado</b> no podrá registrar más información.</p>',
      accept: () => {
        this.granelService.closeChecklist(this.id).subscribe((resp) => {
          this.router.navigate(['/envasado-granel']);
        });
      },
    });
  }
}
