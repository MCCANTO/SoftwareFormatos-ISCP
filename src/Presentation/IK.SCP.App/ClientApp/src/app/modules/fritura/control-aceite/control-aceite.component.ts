import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { RegistroControlAceiteComponent } from './modales/registro-control-aceite/registro-control-aceite.component';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OrdenService as OrdenFRService } from 'src/app/services/fritura/orden.service';
import { DatePipe } from '@angular/common';
import { SecurityService } from 'src/app/services/security.service';
import { ACCION_FR_CONTROL_ACEITE } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { saveAs } from 'file-saver';
import {ExcelExportService} from "../../../services/excel-export.service";

@Component({
  selector: 'app-control-aceite',
  templateUrl: './control-aceite.component.html',
  styleUrls: ['./control-aceite.component.scss'],
})
export class ControlAceiteComponent implements OnInit {
  form: FormGroup = this.fb.group({
    fecha: [[new Date(), new Date()]],
    lineaId: [null],
    ordenId: [null],
  });
  controles: any[] = [];
  freidoras: any[] = [];
  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };
  dataCSV: any[] = [];
  columnsCSV: any[];
  footerDataCSV: any[][] = [];
  constructor(
    private ordenFRService: OrdenFRService,
    private dialogService: DialogService,
    private storageAppService: StorageAppService,
    private router: Router,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    public securityService: SecurityService,
    public excelExportService: ExcelExportService
  ) {
    this.securityService
      .validarAcciones([ACCION_FR_CONTROL_ACEITE])
      .then((resp) => {
        this.PERMISOS = resp[0];
      });
  }

  ngOnInit(): void {
    this.cargarLineas();
  }

  cargarLineas() {
    this.ordenFRService.getAllFreidora().subscribe((resp) => {
      this.freidoras = resp.data;
      this.cargarData();
    });
  }

  cargarData() {
    const dataFritura = this.storageAppService.DataFritura;

    this.form.patchValue({
      fecha: [new Date(), new Date()],
      lineaId: dataFritura.freidora?.Id,
      ordenId: dataFritura.orden?.Orden ?? '',
    });
    this.form.get('lineaId')?.disable();
    if (dataFritura.orden) this.form.get('ordenId')?.disable();
    this.buscar();
  }

  regresar() {
    const dataFritura = this.storageAppService.DataFritura;
    if (dataFritura.orden) this.router.navigate(['/fritura']);
    else this.router.navigate(['/']);
  }

  agregar() {
    const dataFr = this.storageAppService.DataFritura;
    const lineaId = dataFr.freidora?.Id;
    const ordenId = dataFr.orden?.Orden;
    const producto = dataFr.orden
      ? `${dataFr.orden?.Articulo} - ${dataFr.orden?.Descripcion}`
      : '';

    const ref = this.dialogService.open(RegistroControlAceiteComponent, {
      header: 'Control de Aceite',
      width: '600px',
      data: { lineaId, ordenId, producto, freidoras: this.freidoras },
    });

    ref.onClose.subscribe((result) => {
      if (result) {
        const data = {
          lineaId,
          ordenId,
          ...result,
        };
        this.guardar(data);
      }
    });
  }

  guardar(data: any) {
    this.ordenFRService.saveControlAceite(data).subscribe((_) => this.buscar());
  }

  buscar() {
    const filtros = this.form.getRawValue();

    const desde = this.datePipe.transform(filtros.fecha[0], 'yyyy-MM-dd');
    const hasta = this.datePipe.transform(filtros.fecha[1], 'yyyy-MM-dd');
    filtros.fecha = [desde, hasta];

    this.ordenFRService
      .getAllControlAceite(filtros.fecha, filtros.lineaId, filtros.ordenId)
      .subscribe((resp) => {
        if (resp.success) this.controles = resp.data;
        else this.controles = [];
      });
  }

  printControlParametrosCalidadAceite() {
    const filtros = this.form.getRawValue();

    const desde = this.datePipe.transform(filtros.fecha[0], 'yyyy-MM-dd');
    const hasta = this.datePipe.transform(filtros.fecha[1], 'yyyy-MM-dd');
    filtros.fecha = [desde, hasta];

    this.ordenFRService.printControlParametrosCalidadAceite(filtros.fecha, filtros.lineaId, filtros.ordenId).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }

  exportExcel() {
    this.columnsCSV = ['Fecha Hora', 'Linea', 'Orden', 'Producto', 'Sal/Sabor', 'Etapa proceso', 'Aceite', 'Inicio Fuente',
                       'Relleno Fritura', 'AGL', 'CP', 'Color', 'Olor', 'Usuario', 'Observacion'];


    this.dataCSV = this.controles.map(control => ({
      "Fecha Hora": this.formatDate(control.fechaHora),
      "Línea": control.linea,
      "Orden ID": control.ordenId,
      "Producto": control.producto,
      "Sabor": control.sabor,
      "Etapa": control.etapa,
      "Aceite": control.aceite,
      "Inicio Fuente": control.inicioFuente,
      "Relleno Fuente": control.rellenoFuente,
      "AGL": control.agl,
      "CP": control.cp ,
      "Color": control.color,
      "Olor": control.olor,
      "Usuario": control.usuario,
      "Observación": control.observacion,
    }));

     this.excelExportService.exportAsExcelFile('Control de Parámetros de Calidad de Aceite', '',
       this.columnsCSV, this.dataCSV, this.footerDataCSV, 'Control-Parámetros-Calidad-Aceite', 'Sheet1');
  }

  formatDate(dateString: string): string {
    // Aquí puedes utilizar tu lógica para formatear la fecha según tus necesidades
    return new Date(dateString).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' });
  }
}
