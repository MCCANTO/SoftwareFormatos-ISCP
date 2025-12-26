import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { DataAcondicionamientoStorage } from 'src/app/core/models/acondicionamiento/acondicionamiento-data';
import { ControlMaizService } from 'src/app/services/acondicionamiento/control-maiz.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-maiz',
  templateUrl: './control-maiz.component.html',
  styleUrls: ['./control-maiz.component.scss'],
  providers: [DialogService],
})
export class ControlMaizComponent implements OnInit {
  datosAcond!: DataAcondicionamientoStorage;
  materias: any[] = [];
  insumos: any[] = [];
  datosPelado: any;
  datosRemojo: any;
  datosSancochado: any;

  observacion = '';
  observaciones: any[] = [];

  constructor(
    private router: Router,
    private service: ControlMaizService,
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    this.datosAcond = this.storageAppService.DataAcondicionamiento;
    this.cargarMateriaPrima();
    this.cargarInsumos();
    this.cargarObservaciones();
    this.cargarDatosPelado();
    this.cargarDatosRemojo();
    this.cargarDatosSancochado();
  }

  cargarDatosSancochado() {
    this.service.getAllSancochado(this.datosAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.datosSancochado = resp.data;
      }
    });
  }

  cargarDatosRemojo() {
    this.service.getAllRemojo(this.datosAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.datosRemojo = resp.data;
      }
    });
  }

  cargarDatosPelado() {
    this.service.getAllPelado(this.datosAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.datosPelado = resp.data;
      }
    });
  }

  cargarInsumos() {
    this.service
      .getAllIngresoInsumo(this.datosAcond.orden)
      .subscribe((resp) => {
        if (resp.success) {
          this.insumos = resp.data;
        }
      });
  }

  cargarMateriaPrima() {
    this.service
      .getAllIngresoMateriaPrima(this.datosAcond.orden)
      .subscribe((resp) => {
        if (resp.success) {
          this.materias = resp.data;
        }
      });
  }

  guardarMP(data: any) {
    const dataInsert = {
      ordenId: this.datosAcond.orden,
      ...data,
    };

    this.service
      .insertIngresoMateriaPrima(dataInsert)
      .subscribe((resp) => this.cargarMateriaPrima());
  }

  guardarInsumo(data: any) {
    const dataInsert = {
      ordenId: this.datosAcond.orden,
      ...data,
    };

    this.service
      .insertIngresoInsumo(dataInsert)
      .subscribe((resp) => this.cargarInsumos());
  }

  guardarPelado(data: any) {
    const dataInsert = {
      ordenId: this.datosAcond.orden,
      ...data,
    };
    this.service
      .savePelado(dataInsert)
      .subscribe((_) => this.cargarDatosPelado());
  }

  guardarRemojo(data: any) {
    const dataInsert = {
      ordenId: this.datosAcond.orden,
      ...data,
    };
    this.service
      .insertRemojo(dataInsert)
      .subscribe((resp) => this.cargarDatosRemojo());
  }

  guardarSancochado(data: any) {
    const dataInsert = {
      ordenId: this.datosAcond.orden,
      ...data,
    };
    this.service
      .insertSancochado(dataInsert)
      .subscribe((resp) => this.cargarDatosSancochado());
  }

  cargarObservaciones() {
    this.service.getAllObservacion(this.datosAcond.orden).subscribe((resp) => {
      if (resp.success) {
        this.observaciones = resp.data;
      }
    });
  }

  agregarObservacion() {
    if (this.observacion) {
      const data = {
        ordenId: this.datosAcond.orden,
        observacion: this.observacion,
      };

      this.service.insertObservacion(data).subscribe((_) => {
        this.cargarObservaciones();
        this.observacion = '';
      });
    }
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  printControlMaiz() {
    this.service.printControlMaiz(this.storageAppService.DataAcondicionamiento.orden)
      .subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }
}
