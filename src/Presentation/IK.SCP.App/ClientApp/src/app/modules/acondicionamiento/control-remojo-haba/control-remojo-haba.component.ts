import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { DataAcondicionamientoStorage } from 'src/app/core/models/acondicionamiento/acondicionamiento-data';
import { ControlHabaService } from 'src/app/services/acondicionamiento/control-haba.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { IngresoControlRemojoComponent } from './modales/ingreso-control-remojo/ingreso-control-remojo.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-remojo-haba',
  templateUrl: './control-remojo-haba.component.html',
  styleUrls: ['./control-remojo-haba.component.scss'],
})
export class ControlRemojoHabaComponent {
  dataAcond!: DataAcondicionamientoStorage;
  datos: any;

  constructor(
    private storageAppService: StorageAppService,
    private router: Router,
    private dialogService: DialogService,
    private service: ControlHabaService
  ) {}

  ngOnInit(): void {
    this.dataAcond = this.storageAppService.DataAcondicionamiento;
    this.cargarControles();
  }

  cargarControles() {
    this.service.getAllRemojo(this.dataAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.datos = resp.data;
      }
    });
  }

  agregar() {
    this.abrirModal(false);
  }

  abrirModal(esEdicion: boolean, control: any = null) {
    const mp_ref = this.dialogService.open(IngresoControlRemojoComponent, {
      header: 'NUEVO CONTROL DE REMOJO',
      width: '35%',
      data: {
        esEdicion,
        datos: esEdicion ? control : null,
        esMaiz: true,
        ordenId: this.dataAcond.orden,
      },
    });

    mp_ref.onClose.subscribe((result) => {
      if (result) {
        this.guardar(result);
      }
    });
  }

  calcularPesoTotal() {
    let total = 0;
    this.datos?.controles.forEach((mp: any) => {
      total += mp.cantidadBatch;
    });
    return total;
  }

  guardar(data: any) {
    const dataInsert = {
      ordenId: this.dataAcond.orden,
      ...data,
    };
    this.service
      .insertRemojo(dataInsert)
      .subscribe((_) => this.cargarControles());
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  printControlRemojoHabas() {
    this.service.printControlRemojoHabas(this.dataAcond.orden)
      .subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }
}
