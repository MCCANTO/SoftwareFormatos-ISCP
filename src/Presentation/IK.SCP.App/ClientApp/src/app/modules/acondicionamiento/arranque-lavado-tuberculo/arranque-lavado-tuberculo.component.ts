import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueLavadoTuberculoService } from 'src/app/services/acondicionamiento/arranque-lavado-tuberculo.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-arranque-lavado-tuberculo',
  templateUrl: './arranque-lavado-tuberculo.component.html',
  styleUrls: ['./arranque-lavado-tuberculo.component.scss'],
})
export class ArranqueLavadoTuberculoComponent implements OnInit {
  arranque!: any;

  constructor(
    private service: ArranqueLavadoTuberculoService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private storageAppService: StorageAppService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.cargarData();
  }

  cargarData() {
    const dataAco = this.storageAppService.DataAcondicionamiento;
    this.service
      .getArranqueLavadoTuberculoActivo(dataAco.orden)
      .subscribe((resp) => {
        if (resp.success) {
          this.arranque = resp.data;
        }
      });
  }

  guardarCondiciones() {
    const data = {
      arranqueLavadoTuberculoId: this.arranque.ArranqueLavadoTuberculoId,
      condiciones: this.arranque.Condiciones,
    };
    this.service
      .saveCondicionPrevia(data)
      .subscribe((resp) => this.cargarData());
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(verificacion.id, verificacion.cerrado);
  }

  agregarVerificacion() {
    this.cargarDetalleVerficiacion(0);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.service.getAllVerificacionEquipoDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const verificaciones = resp.data;

        const vb_ref = this.dialogService.open(VerificacionEquipoComponent, {
          header: 'Verificación de Equipo previa al arranque',
          width: '95%',
          data: {
            mostrarTipo: false,
            esEditable: !cerrado && !this.arranque.Cerrado,
            cerrado: cerrado || this.arranque.Cerrado,
            variables: verificaciones,
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
      arranqueLavadoTuberculoId: this.arranque.ArranqueLavadoTuberculoId,
      arranqueLavadoTuberculoVerificacionEquipoId: id,
      verificaciones: verificacionesEquipo,
    };

    this.service.saveVerificacionEquipoDetalle(data).subscribe((resp) => {
      this.cargarData();
    });
  }

  cerrar() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de cerrar el arranque? <p>Una vez <b>cerrado</b> no podrá modificar dicha información.</p>',
      accept: () => {
        this.service
          .closeArranqueLavadoTuberculoActivo({
            arranqueLavadoTuberculoId: this.arranque.ArranqueLavadoTuberculoId,
          })
          .subscribe((resp) => {
            if (resp.success) {
              this.messageService.add({
                severity: 'success',
                summary: 'Advertencia',
                detail: 'Arranque Lavado Tuberculo cerrado',
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
