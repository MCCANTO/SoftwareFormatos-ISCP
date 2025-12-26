import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueMaizService } from 'src/app/services/acondicionamiento/arranque-maiz.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-detalle-checklist-maiz',
  templateUrl: './detalle-checklist-maiz.component.html',
  styleUrls: ['./detalle-checklist-maiz.component.scss'],
})
export class DetalleChecklistMaizComponent {
  id: number = 0;

  dataAcond: any;

  arranque: any;

  observacion = '';

  constructor(
    private router: Router,
    private storageAppService: StorageAppService,
    private service: ArranqueMaizService,
    private dialogService: DialogService,
    private confirmationService: ConfirmationService,
    private activatedRoute: ActivatedRoute
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.cargarDataAcond();
    this.cargarArranqueMaiz();
  }

  cargarArranqueMaiz() {
    this.service.getByIdArranqueMaiz(this.id).subscribe((resp) => {
      if (resp.success) {
        this.arranque = resp.data;
      } else {
        this.router.navigate(['/acondicionamiento/maiz/panel-control']);
      }
    });
  }

  cargarDataAcond() {
    this.dataAcond = this.storageAppService.DataAcondicionamiento;
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(verificacion.id, true);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.service.getAllVerificacionEquipoDetalle(id).subscribe((resp) => {
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
      arranqueMaizId: this.arranque.ArranqueMaizId,
      arranqueMaizVerificacionEquipoId: id,
      verificaciones: verificacionesEquipo,
    };

    this.service.saveVerificacionEquipoDetalle(data).subscribe((resp) => {
      this.cargarArranqueMaiz();
    });
  }

  guardarCondiciones() {
    const data = {
      arranqueMaizId: this.arranque.ArranqueMaizId,
      condiciones: this.arranque.Condiciones,
    };
    this.service
      .saveCondicionPrevia(data)
      .subscribe((resp) => this.cargarArranqueMaiz());
  }

  guardarVariables() {
    const data = {
      arranqueMaizId: this.arranque.ArranqueMaizId,
      temperatura: this.arranque.Temperatura,
      observacionTemperatura: this.arranque.ObservacionTemperatura,
    };
    this.service
      .insertVariableBasica(data)
      .subscribe((resp) => this.cargarArranqueMaiz());
  }

  guardarObservacion() {
    const data = {
      arranqueMaizId: this.arranque.ArranqueMaizId,
      observacion: this.observacion,
    };
    this.service.insertArranqueObservacion(data).subscribe((resp) => {
      this.observacion = '';
      this.cargarArranqueMaiz();
    });
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  cerrar() {
    this.confirmationService.confirm({
      message:
        '¿Está seguro(a) de cerrar la información? <p>Una vez <b>cerrada</b> no podrán registrar más datos.</p>',
      accept: () => {
        this.service
          .closeArranqueMaizActivo(this.arranque.ArranqueMaizId)
          .subscribe((resp) => {
            if (resp.success) {
              this.router.navigate(['/acondicionamiento/panel-control']);
            }
          });
      },
    });
  }

  printCheckListArranqueMaiz() {
    this.service.printCheckListArranqueMaiz(this.id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
