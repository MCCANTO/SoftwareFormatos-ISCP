import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueElectroporadorService } from 'src/app/services/acondicionamiento/arranque-electroporador.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-arranque-electroporador',
  templateUrl: './arranque-electroporador.component.html',
  styleUrls: ['./arranque-electroporador.component.scss'],
})
export class ArranqueElectroporadorComponent implements OnInit {
  arranque!: any;

  constructor(
    private router: Router,
    private service: ArranqueElectroporadorService,
    private storageAppService: StorageAppService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.cargarDatosChecklist();
  }

  cargarDatosChecklist() {
    const dataAcond = this.storageAppService.DataAcondicionamiento;
    this.service.getOpen(dataAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.arranque = resp.data;
      }
    });
  }

  guardarCondiciones() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de guardar la información? <p>Una vez <b>guardada</b> no podrá modificar dicha información.</p>',
      accept: () => {
        const data = {
          arranqueElectroporadorId: this.arranque.ArranqueElectroporadorId,
          condiciones: this.arranque?.Condiciones,
        };
        this.service.saveCondiciones(data).subscribe((resp) => {
          if (resp.success) {
            this.cargarDatosChecklist();
          }
        });
      },
    });
  }

  get permiteGuardarCondiciones() {
    return this.arranque?.Condiciones.some((x: any) => !x.fechaModificacion);
  }

  agregarVerificacion() {
    this.cargarDetalleVerficiacion(0);
  }

  verVerificacion(row: any) {
    this.cargarDetalleVerficiacion(row.id, row.cerrado);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.service.getAllVerificacionDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const variables = resp.data;

        const vb_ref = this.dialogService.open(VerificacionEquipoComponent, {
          header: 'Verificación de Equipo previa al arranque',
          width: '95%',
          data: {
            mostrarTipo: false,
            esEditable: !cerrado && !this.arranque.Cerrado,
            cerrado: cerrado || this.arranque.Cerrado,
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
      arranqueElectroporadorVerificacionEquipoId: id,
      arranqueElectroporadorId: this.arranque.ArranqueElectroporadorId,
      verificaciones: verificacionesEquipo,
    };

    this.service.saveVerificacionDetalle(data).subscribe((resp) => {
      if (resp.success) {
        this.cargarDatosChecklist();
      }
    });
  }

  guardarVariablesBasicas() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de guardar la información? <p>Una vez <b>guardada</b> no podrá modificar dicha información.</p>',
      accept: () => {
        const data = {
          arranqueElectroporadorId: this.arranque.ArranqueElectroporadorId,
          variables: this.arranque?.Variables,
        };
        this.service.saveVariablesBasicas(data).subscribe((resp) => {
          if (resp.success) {
            this.cargarDatosChecklist();
          }
        });
      },
    });
  }

  cerrar() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de cerrar el arranque? <p>Una vez <b>cerrado</b> no podrá modificar dicha información.</p>',
      accept: () => {
        this.service
          .close({
            arranqueElectroporadorId: this.arranque.ArranqueElectroporadorId,
          })
          .subscribe((resp) => {
            if (resp.success) {
              this.messageService.add({
                severity: 'success',
                summary: 'Advertencia',
                detail: 'Arranque electroporador cerrado.',
              });
              this.router.navigate(['/acondicionamiento/panel-control']);
            }
          });
      },
    });
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }
}
