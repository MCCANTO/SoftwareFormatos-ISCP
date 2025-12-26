import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import {
  ACCION_FR_CARACTERIZACION_PRODUCTO,
  ACCION_FR_CHECKLIST_ARRANQUE_MANUFACTURA,
  ACCION_FR_REGISTRO_EVALUACION_ATRIBUTOS,
} from 'src/app/core/constants/accion.constant';
import { ControlFritura } from 'src/app/core/constants/fritura.constant';
import { MESSAGE_NO_ACCESS } from 'src/app/core/constants/mensaje.constant';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import { DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { ArranqueMaquinaService } from 'src/app/services/fritura/arranque-maquina.service';
import { EvaluacionAtributoService } from 'src/app/services/fritura/evaluacion-atributo.service';
import { OrdenService } from 'src/app/services/fritura/orden.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { environment } from 'src/environments/environment';
import { EvaluacionAtributoComponent } from '../modales/evaluacion-atributo/evaluacion-atributo.component';
import { CaracterizacionProductoComponent } from './modales/caracterizacion-producto/caracterizacion-producto.component';
import { saveAs } from 'file-saver';
import {ExcelExportService} from "../../../services/excel-export.service";

@Component({
  selector: 'app-panel-control',
  templateUrl: './panel-control.component.html',
  styleUrls: ['./panel-control.component.scss'],
})
export class PanelControlComponent implements OnInit {
  PERMISOS_CHK: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  PERMISOS_EVA: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  PERMISOS_CARACT_PROD: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  dataFR: DataFrituraStorage = {};

  arranques_maquina = [];

  evaluaciones: any[] = [];

  aplicaciones: any[] = [];

  defectos: any[] = [];
  caracterizaciones: any[] = [];

  dataCSV: any[] = [];
  columnsCSV: any[];
  footerDataCSV: any[][] = [];

  constructor(
    private router: Router,
    private dialogService: DialogService,
    private evaluacionAtributoService: EvaluacionAtributoService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private ordenService: OrdenService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private storageAppService: StorageAppService,
    public securityService: SecurityService,
    public excelExportService: ExcelExportService
  ) {
    this.securityService
      .validarAcciones([
        ACCION_FR_CHECKLIST_ARRANQUE_MANUFACTURA,
        ACCION_FR_REGISTRO_EVALUACION_ATRIBUTOS,
        ACCION_FR_CARACTERIZACION_PRODUCTO,
      ])
      .then((resp) => {
        this.PERMISOS_CHK = resp[0];
        this.PERMISOS_EVA = resp[1];
        this.PERMISOS_CARACT_PROD = resp[2];
      });
  }

  ngOnInit(): void {
    this.cargarData();
    this.cargarAplicaciones();
    this.cargarArranqueMaquina();
    this.cargarEvaluaciones();
    this.cargarCaracterizacion();
  }

  cargarCaracterizacion() {
    const ordenId = this.dataFR.orden?.Orden ?? '';
    const articulo = this.dataFR.orden?.Articulo ?? '';

    this.ordenService
      .getAllDefectoCaracterizacion(articulo)
      .subscribe((resp) => {
        if (resp.success) this.defectos = resp.data;
      });

    this.ordenService
      .getAllRegistroCaracterizacion(ordenId)
      .subscribe((resp) => {
        if (resp.success) {
          this.caracterizaciones = resp.data;
        }
      });
  }

  public validarVisibilidad(idOpcion: number): boolean {
    if (idOpcion === ControlFritura.CARACTERIZACION_PRODUCTO)
      return this.PERMISOS_CARACT_PROD.ESCRITURA;

    return true;
  }

  private cargarAplicaciones() {
    this.aplicaciones = this.securityService.listarOpciones(eModulo.FRITURA);
  }

  private cargarData() {
    this.dataFR = this.storageAppService.DataFritura;
  }

  private cargarArranqueMaquina() {
    this.arranqueMaquinaService
      .getAll(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe((resp: any) => {
        if (resp.ok) {
          this.arranques_maquina = resp.data;
        }
      });
  }

  private cargarEvaluaciones() {
    this.evaluacionAtributoService
      .getAll(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe((resp: any) => {
        if (resp.ok) {
          this.evaluaciones = resp.data;
        }
      });
  }

  goTo(index: number) {
    if (index === 0) {
      this.router.navigate(['/']);
    }

    if (index === 1) {
      window.location.href = environment.UrlBandejaFritura;
    }

    if (index === ControlFritura.EVALUACION_ATRIBUTOS) {
      this.openEvaluacion();
    }

    if (index === ControlFritura.ARRANQUE_MAQUINA) {
      this.validarArranqueMaquina();
    }

    if (index === ControlFritura.CARACTERIZACION_PRODUCTO) {
      this.openCaracterizacionProducto();
    }

    if (index === ControlFritura.CONTROL_ACEITE) {
      this.openControlAceite();
    }
  }

  private openControlAceite() {
    this.router.navigate(['/fritura/control-aceite']);
  }

  private openCaracterizacionProducto() {
    if (this.PERMISOS_CARACT_PROD.ESCRITURA) {
      const ref = this.dialogService.open(CaracterizacionProductoComponent, {
        header: 'Caracterización de Producto Terminado',
        width: '500px',
        data: {
          articulo: this.dataFR.orden?.Articulo,
        },
      });

      ref.onClose.subscribe((result) => {
        if (result) {
          const data = {
            ordenId: this.dataFR.orden?.Orden,
            ...result,
          };
          this.guardarCaracterizacionProducto(data);
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: `${MESSAGE_NO_ACCESS} registrar caracterización de producto terminado.`,
      });
    }
  }

  guardarCaracterizacionProducto(data: any) {
    this.ordenService
      .saveRegistroCaracterizacion(data)
      .subscribe((_) => this.cargarCaracterizacion());
  }

  obtenerDatoDefecto(registro: any, defecto: any, campo: string) {
    return registro.defectos.filter((f: any) => f.id === defecto.id)[0][campo];
  }

  private validarArranqueMaquina() {
    if (!this.PERMISOS_CHK.ESCRITURA) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'No tiene permiso para realizar esta acción.',
      });
      return;
    }

    this.arranqueMaquinaService
      .getOpen(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe((resp) => {
        if (resp.ok) {
          if (resp.data) {
            this.router.navigate(['/fritura/arranque-maquina']);
          } else {
            this.generarNuevoArranqueManufactura();
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Mensaje de Error',
            detail: 'No se pudieron obtener datos del Arranque de manufactura.',
          });
        }
      });
  }

  private generarNuevoArranqueManufactura() {
    this.confirmationService.confirm({
      message:
        'Se generará un nuevo <b>Checklist de Arranque de Manufactura</b>. ¿Desea continuar con la generación?',
      accept: () => {
        const data = {
          linea: this.dataFR.freidora?.Id,
          ordenId: this.dataFR.orden?.Orden,
        };

        this.arranqueMaquinaService.save(data).subscribe((resp) => {
          if (resp.ok) {
            this.router.navigate(['/fritura/arranque-maquina']);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Mensaje de Error',
              detail: 'No se pudo generar nuevo Arranque de manufactura.',
            });
          }
        });
      },
    });
  }

  getColorStyle(calificacion: number): string {
    let color = '';

    if (calificacion <= 2) color = '#EF7564';
    else if (calificacion <= 3) color = '#F5DD29';
    else if (calificacion <= 5) color = '#61BD4F';
    else color = '';

    return color;
  }

  public openEvaluacion(id: number = 0) {
    if (!this.PERMISOS_EVA.ESCRITURA) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'No tiene permiso para realizar esta acción.',
      });
      return;
    }

    const ref = this.dialogService.open(EvaluacionAtributoComponent, {
      header: 'Evaluación de Atributos',
      width: '80%',
      data: {
        id,
      },
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          linea: this.dataFR.freidora?.Id,
          ordenId: this.dataFR.orden?.Orden,
          ...result,
        };
        this.evaluacionAtributoService.create(data).subscribe((data: any) => {
          if (data.ok) {
            this.cargarEvaluaciones();
          }
        });
      }
    });
  }

  public verArranqueMaquina(arranque: any) {
    this.router.navigate([
      `/fritura/detalle-arranque-maquina/${arranque.arranqueMaquinaId}`,
    ]);
  }

  createPDFArranqueManufactura(arranque: any) {
    this.arranqueMaquinaService.generateFilePDFArranqueManufactura(this.storageAppService.DataFritura.orden.Orden,
                                                       this.storageAppService.DataFritura.orden.Articulo,
                                                       arranque.arranqueMaquinaId,
                                                       this.storageAppService.DataFritura.freidora.Id)
      .subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);

        //const file = new Blob([response], { type: 'application/pdf' });
        //saveAs(file, 'nombre_archivo.pdf'); // Descarga el archivo PDF
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }

  createPDFEvaluacionAtributos(arranque: any) {
    this.evaluacionAtributoService.generatePDFEvaluacionAtributos(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe((response: Blob) => {
        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar
        saveAs(response, fileName);
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }

  printCaracterizacionProductoTerminado() {
    const ordenId = this.dataFR.orden?.Orden ?? '';
    this.ordenService.printCaracterizacionProductoTerminado(ordenId).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    );
  }

  exportExcel() {
    this.columnsCSV = ['Fecha Hora', 'Peso (G)'];
    this.defectos.forEach((element:any) => { this.columnsCSV.push(element.nombre); this.columnsCSV.push(element.porcentaje); })
    this.columnsCSV.push('Etapa de proceso', 'Usuario', 'Inspector', 'Observación');

    this.dataCSV = this.caracterizaciones.map(caracterizacion => {
      const nuevoObjeto = {
        "FechaHora": this.formatDate(caracterizacion.fechaHora),
        "Peso": caracterizacion.peso,
        ...caracterizacion.defectos.reduce((acc, defecto, i) => {
          acc[defecto.nombreDefecto + (i + 1)] = this.obtenerDatoDefecto(caracterizacion, defecto, "valor");
          acc[defecto.nombrePorcentaje + (i + 1)] = this.obtenerDatoDefecto(caracterizacion, defecto, "porcentaje");
          return acc;
        }, {}),
        "EtapaProceso": caracterizacion.etapa,
        "Usuario": caracterizacion.usuario,
        "Inspector": caracterizacion.inspector,
        "Observación": caracterizacion.observacion,
      };

      return nuevoObjeto;
    });

    this.excelExportService.exportAsExcelFile('Caracterización del producto terminado', '',
      this.columnsCSV, this.dataCSV, this.footerDataCSV, 'Caracterización-del-producto-terminado', 'Sheet1');
  }

  formatDate(dateString: string): string {
    // Aquí puedes utilizar tu lógica para formatear la fecha según tus necesidades
    return new Date(dateString).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' });
  }

}
