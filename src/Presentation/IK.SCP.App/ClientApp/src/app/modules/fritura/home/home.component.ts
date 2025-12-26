import { EvaluacionAtributoComponent } from '../modales/evaluacion-atributo/evaluacion-atributo.component';
import { Router } from '@angular/router';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { EvaluacionAtributoService } from 'src/app/services/fritura/evaluacion-atributo.service';
import { ControlFritura } from 'src/app/core/constants/fritura.constant';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { DataFritura, DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { ArranqueMaquinaService } from 'src/app/services/fritura/arranque-maquina.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, MessageService } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [ DialogService, MessageService, ConfirmationService ]
})
export class HomeComponent implements OnInit {

  dataFR: DataFrituraStorage = {};

  arranques_maquina = [];

  evaluaciones: any[] = [];

  constructor(
    private jwtTokenService: JwtTokenService,
    // private localStorageService: LocalStorageService,
    private router: Router,
    private dialogService: DialogService,
    private evaluacionAtributoService: EvaluacionAtributoService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private storageAppService: StorageAppService,
  ) { }

  ngOnInit(): void {
    this.cargarData();
    this.cargarArranqueMaquina();
    this.cargarEvaluaciones();
  }

  cargarData() {
    this.dataFR = this.storageAppService.DataFritura;
  }

  cargarArranqueMaquina() {
    this.arranqueMaquinaService.getAll(
      this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? ''
    ).subscribe( (resp: any) => {
      if( resp.ok ) { console.log(resp);
        this.arranques_maquina = resp.data;
      }
    });
  }

  cargarEvaluaciones() {
    this.evaluacionAtributoService.getAll(
      this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? ''
    ).subscribe( (resp: any) => {
      if( resp.ok ) {
        this.evaluaciones = resp.data;
      }
    });
  }

  get EsMaquinista(): boolean {
    return this.jwtTokenService.esMaquinistaFr();
  }

  goTo(index: number) {

    if (index === 0) { console.log(0);
      this.router.navigate(['/']);
    }

    if (index === 1) { console.log(1);
      this.jwtTokenService.removeToken();
      window.location.href = environment.UrlBandejaFritura;
    }

    if (index === ControlFritura.EVALUACION_ATRIBUTOS) {
      this.openEvaluacion();
    }

    if (index === ControlFritura.ARRANQUE_MAQUINA) {
      this.ValidarArranqueMaquina();
    }

  }

  ValidarArranqueMaquina() {
    this.arranqueMaquinaService.getOpen(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe(resp => {

        if (resp.ok) {

          if (resp.data) {
            this.router.navigate(['/fritura/arranque-maquina']);
          } else {

            if (this.EsMaquinista) {

              this.GenerarNuevoArranqueManufactura();

            } else {
              this.messageService.add({ severity: 'warn', summary: 'Mensaje Advertencia', detail: 'No se ha generado información de Arranque de manufactura.' });
            }

          }

        } else {
          this.messageService.add({ severity: 'error', summary: 'Mensaje de Error', detail: 'No se pudieron obtener datos del Arranque de manufactura.' });
        }

      });

  }

  GenerarNuevoArranqueManufactura() {

    this.confirmationService.confirm({
      message: 'Se generará un nuevo <b>Checklist de Arranque de Manufactura</b>. ¿Desea continuar con la generación?',
      accept: () => {

        const data = {
          linea: this.dataFR.freidora?.Id,
          ordenId: this.dataFR.orden?.Orden,
        };

        this.arranqueMaquinaService.save(data)
          .subscribe(resp => {

            if (resp.ok) {
              this.router.navigate(['/fritura/arranque-maquina']);
            } else {
              this.messageService.add({ severity: 'error', summary: 'Mensaje de Error', detail: 'No se pudo generar nuevo Arranque de manufactura.' });
            }

          });

      }
    });
  }

  getColorStyle( calificacion: number ): string {

    let color = '';

    if ( calificacion <=2 )
      color = '#EF7564';
    else if ( calificacion <=3 )
      color = '#F5DD29';
    else if ( calificacion <= 5)
      color = '#61BD4F';
    else
      color = '';

    return color;
  }

  openEvaluacion(id: number = 0) {
    const ref = this.dialogService.open(EvaluacionAtributoComponent, {
      header: 'Evaluación de Atributos',
      width: '80%',
      data: {
        id
      }
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          linea: this.dataFR.freidora?.Id,
          ordenId: this.dataFR.orden?.Orden,
          ...result
        }
        this.evaluacionAtributoService.create( data )
          .subscribe( ( data: any ) => {
            if ( data.ok ) {
              this.cargarEvaluaciones();
            }
          })
      }
    });
  }

  verArranqueMaquina( arranque: any ) {
    this.router.navigate([`/fritura/detalle-arranque-maquina/${ arranque.arranqueMaquinaId }`]);
  }

  createPDF(arranque: any) {

    console.log(arranque);

    this.arranqueMaquinaService.getFilePDFEnvasado(arranque.arranqueMaquinaId)
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

}
