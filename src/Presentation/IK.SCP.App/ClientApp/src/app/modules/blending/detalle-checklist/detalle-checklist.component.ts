import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { BlendingService } from 'src/app/services/blending/blending.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-detalle-checklist',
  templateUrl: './detalle-checklist.component.html',
  styleUrls: ['./detalle-checklist.component.scss'],
})
export class DetalleChecklistComponent {
  id = 0;

  dataEnv!: any;

  dataBlending!: any;

  observacion = '';

  constructor(
    private blendingService: BlendingService,
    public dialogService: DialogService,
    private storageAppService: StorageAppService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.id = this.activatedRoute.snapshot.params.id;
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

    this.blendingService.getArranqueById(this.id).subscribe((resp) => {
      if (resp.success) {
        if (resp.data) {
          this.dataBlending = resp.data;
        }
      }
    });
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(
      verificacion.BlendingArranqueVerificacionEquipoId,
      true
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
              esEditable: !cerrado && !this.dataBlending.Cerrado,
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
