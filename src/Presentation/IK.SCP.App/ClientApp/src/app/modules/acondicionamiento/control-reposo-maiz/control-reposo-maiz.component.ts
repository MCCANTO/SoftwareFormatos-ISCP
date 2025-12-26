import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { DataAcondicionamientoStorage } from 'src/app/core/models/acondicionamiento/acondicionamiento-data';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { IngresoControlReposoComponent } from './modales/ingreso-control-reposo/ingreso-control-reposo.component';
import { ControlMaizService } from 'src/app/services/acondicionamiento/control-maiz.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-reposo-maiz',
  templateUrl: './control-reposo-maiz.component.html',
  styleUrls: ['./control-reposo-maiz.component.scss'],
})
export class ControlReposoMaizComponent implements OnInit {
  dataAcond!: DataAcondicionamientoStorage;
  datos: any;

  constructor(
    private storageAppService: StorageAppService,
    private router: Router,
    private dialogService: DialogService,
    private service: ControlMaizService
  ) {}

  ngOnInit(): void {
    this.dataAcond = this.storageAppService.DataAcondicionamiento;
    this.cargarControles();
  }

  cargarControles() {
    this.service.getAllReposo(this.dataAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.datos = resp.data;
      }
    });
  }

  agregar() {
    this.abrirModal(false);
  }

  editar(control: any) {
    this.abrirModal(true, control);
  }

  abrirModal(esEdicion: boolean, control: any = null) {
    const mp_ref = this.dialogService.open(IngresoControlReposoComponent, {
      header: 'NUEVO CONTROL DE REPOSO',
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
      .insertReposo(dataInsert)
      .subscribe((_) => this.cargarControles());
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  printControlReposoMaiz() {
    this.service.printControlReposoMaiz(this.dataAcond.orden)
      .subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      });
  }
}
