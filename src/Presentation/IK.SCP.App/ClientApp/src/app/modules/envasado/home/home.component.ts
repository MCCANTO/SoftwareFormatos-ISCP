import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ControlEnvasado } from 'src/app/core/constants/envasado.constant';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import { PostArranqueMaquinaRequest } from 'src/app/core/models/envasado/arranque-maquina.model';
import { Envasadora } from 'src/app/core/models/envasado/envasado-data';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import { ArranqueMaquinaService } from 'src/app/services/envasado/arranque-maquina.service';
import { ArranqueService } from 'src/app/services/envasado/arranque.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { environment } from 'src/environments/environment';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  dataEnvasadora!: Envasadora;
  dataOrden!: OrdenEnvasado;

  aplicaciones: any[] = [];

  arranques_maquina = [];
  arranques_blending = [];

  mostrarBlending = false;

  constructor(
    private router: Router,
    public jwtTokenService: JwtTokenService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private arranqueService: ArranqueService,
    private localStorageService: LocalStorageService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
  ) { }

  ngOnInit(): void {

    this.cargarDataEnvasado();

    this.cargarAplicaciones();

    this.cargarListaArranque();

    this.cargarListaBlendingArranque();

  }

  createExcel() {
    this.arranqueMaquinaService.getFileExcelEnvasado()
      .subscribe((response: HttpResponse<Blob>) => {
      const fileName = 'archivo-modificado.xlsx';
      saveAs(response, fileName);
    }, error => {
      console.log('Error al descargar el archivo XLSX', error);
    });
  }

  createPDF(id: number) {
    this.arranqueMaquinaService.getFilePDFEnvasado(id)
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

  cargarAplicaciones() {

    let aplicaciones = this.jwtTokenService.getOpciones(eModulo.ENVASADO);
    this.mostrarBlending = false;

  }

  cargarDataEnvasado() {
    const data = JSON.parse(this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ENVASADO) ?? '');

    this.dataEnvasadora = data.envasadora;
    this.dataOrden = data.orden;
  }

  cargarListaArranque() {
    this.arranqueMaquinaService.getAll(this.dataEnvasadora.Id, this.dataOrden.Orden)
    .subscribe((res: any) => {
      this.arranques_maquina = res.data
    });
  }

  cargarListaBlendingArranque() {

  }

  get EsMaquinista(): boolean {
    return this.jwtTokenService.esMaquinistaEnv();
  }

  get EsPunta(): boolean {
    return this.jwtTokenService.esPuntaEstrellaEnv();
  }

  goTo(idAplicacion: number) {

    if (idAplicacion === 0) {
      this.jwtTokenService.removeToken();
      window.location.href = environment.UrlBandejaEnvasado;
    }

    if (idAplicacion === 1) {
      this.localStorageService.remove(LocalStorageConstant.DATA_ORDEN_ENVASADO);
      this.router.navigate(['/']);
    }

    if (idAplicacion === ControlEnvasado.ARRANQUE_MAQUINA) {
      this.cargarArranqueMaquina();
    }

    if (idAplicacion === ControlEnvasado.ARRANQUE) {
      this.cargarArranqueEnvasado();
    }

    if(idAplicacion === ControlEnvasado.BLENDING_ARRANQUE){
      this.cargarBlendingArranque();
    }

    if(idAplicacion === ControlEnvasado.BLENDING_CONTROL){
      this.cargarBlendingControl();
    }

  }

  cargarArranqueMaquina() {

    this.spinner.show();
    // Obtener arranque maquina abierto
    this.arranqueMaquinaService.getOpen(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe(resp => {

        if (resp.ok) {
          // Si existiera redirecciona a arranque
          this.spinner.hide();
          this.router.navigate(['/envasado/arranque-maquina']);
        } else {
          // Si no existe, solicita confirmación para crearlo
          this.spinner.hide();
          this.confirmationService.confirm({
            message: 'Se generará un nuevo <b>Checklist de Arranque de Máquina</b>. ¿Desea continuar con la generación?',
            accept: () => {
              this.spinner.show();
              const data: PostArranqueMaquinaRequest = {
                arranqueMaquinaId: 0,
                envasadoraId: this.dataEnvasadora.Id,
                ordenId: this.dataOrden.Orden,
                pesoSobreProducto1: null,
                pesoSobreProducto2: null,
                pesoSobreProducto3: null,
                pesoSobreProducto4: null,
                pesoSobreProducto5: null,
                pesoSobreProductoProm: 0,
                pesoSobreVacio: null,
                observacion: ''
              }
              this.arranqueMaquinaService.save(data)
                .subscribe(resp => {

                  if (resp.ok) {
                    this.spinner.hide();
                    this.router.navigate(['/envasado/arranque-maquina']);
                  } else {
                    this.spinner.hide();
                  }

                })

            }
          });
        }

      })

  }

  cargarArranqueEnvasado() {

    this.arranqueService.getArranque(this.dataEnvasadora.Id, this.dataOrden.Orden)
    .subscribe(resp => {

      if (resp.ok) {

        if (resp.data) {
          this.router.navigate(['/envasado/arranque']);
        } else {

          if (this.EsPunta) {

            this.confirmationService.confirm({
              message: 'Se generará un nuevo <b>Checklist de Arranque de Envasado</b>. ¿Desea continuar con la generación?',
              accept: () => {

                const data = {
                  envasadoraId: this.dataEnvasadora.Id,
                  ordenId: this.dataOrden.Orden,
                }

                this.arranqueService.postArranqueActivo(data)
                  .subscribe(resp => {

                    if (resp.ok) {
                      this.router.navigate(['/envasado/arranque']);
                    } else {
                      this.messageService.add({ severity: 'error', summary: 'Mensaje de Error', detail: 'No se pudo generar nuevo Arranque.' });
                    }

                  });

              }
            });

          } else {
            this.messageService.add({ severity: 'warn', summary: 'Mensaje Advertencia', detail: 'No se ha generado información de Arranque.' });
          }

        }

      } else {
        this.messageService.add({ severity: 'error', summary: 'Mensaje de Error', detail: 'No se pudieron obtener datos de Arranque.' });
      }

    })

  }

  cargarBlendingArranque() {

  }

  cargarBlendingControl() {


  }


  verArranqueMaquina(id: number) {
    this.router.navigate([`/envasado/arranque-maquina/${id}`]);
  }

  verBlendingArranque(id: number) {
    this.router.navigate([`/blending/detalle-checklist/${id}`]);
  }

}
