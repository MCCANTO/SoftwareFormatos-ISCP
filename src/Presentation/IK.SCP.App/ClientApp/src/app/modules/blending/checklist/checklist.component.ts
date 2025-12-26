import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ACCION_ENV_BLENDING_CHECKLIST } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { BlendingService } from 'src/app/services/blending/blending.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.scss'],
})
export class ChecklistComponent implements OnInit {
  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  dataEnv!: any;

  dataBlending!: any;

  observacion = '';

  constructor(
    private blendingService: BlendingService,
    public dialogService: DialogService,
    private storageAppService: StorageAppService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private router: Router,
    public securityService: SecurityService
  ) {
    this.securityService
      .validarAcciones([ACCION_ENV_BLENDING_CHECKLIST])
      .then((resp) => {
        this.PERMISOS = resp[0];
      });
  }

  ngOnInit(): void {
    this.cargarDataEnvasado();
    this.cargarData();
  }

  cargarDataEnvasado() {
    this.dataEnv = this.storageAppService.DataEnvasado;
  }

  cargarData() {
    const { orden } = this.dataEnv;

    this.blendingService.getArranqueActivo(orden.Orden).subscribe((resp) => {
      if (resp.success) {
        if (resp.data) {
          this.dataBlending = resp.data;
        }
      }
    });
  }

  agregarVerificacion() {
    this.cargarDetalleVerficiacion(0);
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(
      verificacion.BlendingArranqueVerificacionEquipoId,
      verificacion.Cerrado
    );
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.blendingService
      .getAllVerificacionEquipoDetalle(id)
      .subscribe((resp) => {
        if (resp.success) {
          const variables = resp.data;

          const vb_ref = this.dialogService.open(VerificacionEquipoComponent, {
            header: 'Verificación de Equipo previa al arranque',
            width: '95%',
            data: {
              mostrarTipo: false,
              esEditable:
                !cerrado &&
                !this.dataBlending.Cerrado &&
                this.PERMISOS.ESCRITURA,
              cerrado: cerrado || this.dataBlending.Cerrado,
              variables,
            },
          });

          vb_ref.onClose.subscribe((result: any) => {
            if (result) {
              this.confirmationService.confirm({
                message:
                  '¿Está seguro(a) de guardar la información? <p>Una vez <b>guardada</b> no podrá modificar dicha información.</p>',
                accept: () => {
                  this.guardarVerificacionEquipo(id, result);
                },
              });
            }
          });
        }
      });
  }

  guardarVerificacionEquipo(id: number, verificaciones: any) {
    let verificacionesEquipo: any[] = [];
    verificaciones.forEach((item: any) => {
      verificacionesEquipo.push(...item.detalle);
    });

    const data = {
      blendingArranqueId: this.dataBlending.blendingArranqueId,
      arranqueVerificacionEquipoId: id,
      verificaciones: verificacionesEquipo,
    };

    this.blendingService
      .saveVerificacionEquipoDetalle(data)
      .subscribe((resp) => {
        this.cargarData();
      });
  }

  guardarObservacion() {
    const obs = this.observacion;

    if (obs.length > 4) {
      const data = {
        blendingArranqueId: this.dataBlending.blendingArranqueId,
        observacion: obs,
      };
      this.blendingService.insertArranqueObservacion(data).subscribe((resp) => {
        this.cargarData();
        this.observacion = '';
      });
    }
  }

  guardarCondiciones() {
    const data = {
      blendingArranqueId: this.dataBlending.blendingArranqueId,
      condiciones: this.dataBlending.condiciones,
    };

    this.blendingService.insertArranqueCondicion(data).subscribe((resp) => {
      if (resp.ok) this.cargarData();
    });
  }

  regresar() {
    this.router.navigate(['/envasado']);
  }

  cerrar() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de cerrar la información? <p>Una vez <b>cerrada</b> no podrán registrar más datos.</p>',
      accept: () => {
        this.blendingService
          .closeArranque({
            blendingArranqueId: this.dataBlending.blendingArranqueId,
          })
          .subscribe((resp) => {
            this.router.navigate(['/envasado']);
          });
      },
    });
  }
}
