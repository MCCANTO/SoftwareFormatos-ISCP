import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ACCION_ACO_CONTROL_PEF } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { ControlPefService } from 'src/app/services/acondicionamiento/control-pef.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { SecurityService } from './../../../services/security.service';
import { ModalCondicionBasicaComponent } from './modales/modal-condicion-basica/modal-condicion-basica.component';
import { ModalControlTiempoComponent } from './modales/modal-control-tiempo/modal-control-tiempo.component';
import { ModalFuerzaCorteComponent } from './modales/modal-fuerza-corte/modal-fuerza-corte.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-electroporador',
  templateUrl: './control-electroporador.component.html',
  styleUrls: ['./control-electroporador.component.scss'],
})
export class ControlElectroporadorComponent implements OnInit {
  form: FormGroup = this.fb.group({
    controlTratamientoId: [0],
    proveedor: [null],
    lote: [null],
    humedad: [null],
    brix: [null],
  });

  condiciones: any[] = [];
  fuerzaCortes: any[] = [];
  tiempos: any[] = [];

  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private dialogService: DialogService,
    private messageService: MessageService,
    private service: ControlPefService,
    private storageAppService: StorageAppService,
    private securityService: SecurityService
  ) {
    this.securityService
      .validarAcciones([ACCION_ACO_CONTROL_PEF])
      .then((resp) => {
        this.PERMISOS = resp[0];
      });
  }

  ngOnInit(): void {
    this.InicializarControl();
  }

  private InicializarControl() {
    const dataAcond = this.storageAppService.DataAcondicionamiento;
    this.service.get(dataAcond.orden).subscribe((resp) => {
      if (resp.success) {
        if (resp.data) {
          this.form.patchValue(resp.data);
          this.condiciones = resp.data.Condiciones;
          this.fuerzaCortes = resp.data.FuerzaCortes;
          this.tiempos = resp.data.Tiempos;
        } else {
          this.router.navigate(['/acondicionamiento/panel-control']);
        }
      } else {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: resp.message,
        });
      }
    });
  }

  listarCondicionesPrevias() {}

  agregarCondicionPrevia() {
    this.cargarModalCondicionPrevia(0);
  }

  verCondicionPreviaDetalle(id: number) {
    this.cargarModalCondicionPrevia(id);
  }

  cargarModalCondicionPrevia(id: number) {
    this.service.getAllCondicionPreviaDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const { condicion, detalles } = resp.data;

        const ref = this.dialogService.open(ModalCondicionBasicaComponent, {
          header: 'Condición Previa',
          width: '750px',
          position: 'bottom',
          data: {
            esEditable: id == 0 ? true : false,
            tipoId: condicion?.tipoId,
            condiciones: detalles,
          },
        });

        ref.onClose.subscribe((result) => {
          if (result) {
            const data = {
              controlTratamientoId: this.form.get('controlTratamientoId')
                ?.value,
              ...result,
            };
            this.guardarCondicionPrevia(data);
          }
        });
      }
    });
  }

  guardarCondicionPrevia(data: any) {
    this.service
      .saveCondicionPrevia(data)
      .subscribe((_) => this.InicializarControl());
  }

  agregarFuerzaCorte() {
    this.cargarModalFuerzaCorte(0);
  }

  verFuerzaCorteDetalle(id: number, detalle: any[]) {
    this.cargarModalFuerzaCorte(id, detalle);
  }

  cargarModalFuerzaCorte(id: number, detalle: any[] = []) {
    const ref = this.dialogService.open(ModalFuerzaCorteComponent, {
      header: 'Fuerza de Corte (N)',
      autoZIndex: true,
      data: {
        detalle,
        esEditable: id == 0 ? true : false,
      },
    });

    ref.onClose.subscribe((result) => {
      if (result) {
        const data = {
          controlTratamientoId: this.form.get('controlTratamientoId')?.value,
          fuerzaCortes: result,
        };
        this.guardarFuerzaCorte(data);
      }
    });
  }

  guardarFuerzaCorte(data: any) {
    this.service
      .saveFuerzaCorte(data)
      .subscribe((_) => this.InicializarControl());
  }

  agregarTiempoPefeado() {
    this.cargarModalTiempoPefeado(0);
  }

  cargarModalTiempoPefeado(id: number) {
    const ref = this.dialogService.open(ModalControlTiempoComponent, {
      header: 'Tiempo de Pefeado',
    });

    ref.onClose.subscribe((result) => {
      if (result) {
        const data = {
          controlTratamientoId: this.form.get('controlTratamientoId')?.value,
          ...result,
        };

        this.guardarTiempoPefeado(data);
      }
    });
  }

  guardarTiempoPefeado(data: any) {
    this.service.saveTiempo(data).subscribe((_) => this.InicializarControl());
  }

  guardarDatos() {
    const data = {
      ...this.form.getRawValue(),
    };
    this.service.update(data).subscribe((_) => {
      this.InicializarControl();
    });
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  printControlPEF() {
    this.service.printControlPEF(this.storageAppService.DataAcondicionamiento.orden).subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }
}
