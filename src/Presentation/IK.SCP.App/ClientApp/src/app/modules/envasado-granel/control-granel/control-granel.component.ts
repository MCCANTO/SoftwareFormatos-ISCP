import { GranelService } from './../../../services/envasado/granel.service';
import { EvaluacionAtributoComponent } from './../../fritura/modales/evaluacion-atributo/evaluacion-atributo.component';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { environment } from 'src/environments/environment';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import { CargaCodificacionComponent } from '../modales/carga-codificacion/carga-codificacion.component';
import { DataEnvasadoGranelStorage } from 'src/app/core/models/envasado/envasado-granel-data';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { ControlEnvasadoGranel } from 'src/app/core/constants/envasado.constant';
import { VisorImagenComponent } from 'src/app/components/visor-imagen/visor-imagen.component';
import { SecurityService } from 'src/app/services/security.service';
import { ACCION_ENV_GRANEL_CHECKLIST, ACCION_ENV_GRANEL_CODIFICACION_CAJA, ACCION_ENV_GRANEL_EVALUACION_PT } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-granel',
  templateUrl: './control-granel.component.html',
  styleUrls: ['./control-granel.component.scss'],
  providers: [MessageService, ConfirmationService, DialogService],
})
export class ControlGranelComponent implements OnInit {

  PERMISOS_CHK: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  }

  PERMISOS_EVA: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  }

  PERMISOS_CAJ: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  }

  aplicaciones: any[] = [];

  dataEnvGra!: DataEnvasadoGranelStorage;

  checklists = [];

  evaluaciones = [];

  codificaciones = [];

  constructor(
    private granelService: GranelService,
    private router: Router,
    public dialogService: DialogService,
    private confirmationService: ConfirmationService,
    private jwtTokenService :JwtTokenService,
    private storageAppService: StorageAppService,
    public securityService: SecurityService,
    private messageService: MessageService,
  ) {

    this.securityService.validarAcciones([
      ACCION_ENV_GRANEL_CHECKLIST,
      ACCION_ENV_GRANEL_EVALUACION_PT,
      ACCION_ENV_GRANEL_CODIFICACION_CAJA,
    ]).then(resp => {
      this.PERMISOS_CHK = resp[0];
      this.PERMISOS_EVA = resp[1];
      this.PERMISOS_CAJ = resp[2];
    })

  }

  ngOnInit(): void {
    this.cargarAplicaciones();
    this.cargarDataEnvasado();
    this.listarChecklists();
    this.listarEvaluaciones();
    this.listarCodificacionCaja();
  }

  cargarAplicaciones() {
    const aplicaciones = this.jwtTokenService.getOpciones(eModulo.ENVASADO_GRANEL);
    this.aplicaciones = aplicaciones;
  }

  cargarDataEnvasado() {
    this.dataEnvGra = this.storageAppService.DataEnvasadoGranel;
  }

  regresar(): void {
    this.jwtTokenService.removeToken();
    window.location.href = environment.UrlBandejaEnvasado;
  }

  regresarAnt() {
    this.router.navigate(['/']);
  }

  listarChecklists() {
    this.granelService.getAllChecklist( this.dataEnvGra.envasadora.Id, this.dataEnvGra.orden.Orden)
      .subscribe( resp => {
        this.checklists = resp.data;
      });
  }

  listarEvaluaciones() {
    this.granelService.getAllGranelEvaluacionPT( this.dataEnvGra.envasadora.Id, this.dataEnvGra.orden.Orden)
    .subscribe( resp => {
      this.evaluaciones = resp.data;
    });
  }

  goTo( idAplicacion: number ) {

    switch (idAplicacion) {
      case ControlEnvasadoGranel.ARRANQUE:
        this.goToChecklist();
        break;
      case ControlEnvasadoGranel.CONTROL:
        this.goToControl();
        break;
      case ControlEnvasadoGranel.EVALUACION_PT:
        this.agregarEvaluacionPT();
        break;
      case ControlEnvasadoGranel.CODIFICACION_CAJA:
        this.agregarCodificacionCaja();
        break;
      default:
        break;
    }

  }

  agregarCodificacionCaja() {
    if (!this.PERMISOS_CAJ.ESCRITURA) {
      this.messageService.add({ severity: 'warn', summary: 'Advertencia', detail: 'No tiene permiso para realizar esta acción.' });
      return;
    }

    const ref = this.dialogService.open(CargaCodificacionComponent, {
      header: 'Codificación de Caja',
      width: '60%'
    });

    ref.onClose.subscribe((result: any) => {

      if( result ) {

        const { envasadora, orden } = this.dataEnvGra;

        this.granelService.saveGranelCodificacion({
          envasadoraId: envasadora.Id,
          orden: orden.Orden,
          ... result
        }).subscribe( resp => {
          this.listarCodificacionCaja();
        })
      }


    });
  }

  listarCodificacionCaja() {
    const { envasadora, orden } = this.dataEnvGra;
    this.granelService.getAllGranelCodificacion(envasadora.Id, orden.Orden)
      .subscribe( resp => {
        if (resp.success){
          this.codificaciones = resp.data;
        }
      });
  }

  agregarEvaluacionPT() {
    if (!this.PERMISOS_EVA.ESCRITURA) {
      this.messageService.add({ severity: 'warn', summary: 'Advertencia', detail: 'No tiene permiso para realizar esta acción.' });
      return;
    }
    this.abrirEvaluacionPT(0);
  }

  verEvaluacion(id : number) {
    this.abrirEvaluacionPT(id);
  }

  abrirEvaluacionPT(id : number) {
    const ref = this.dialogService.open(EvaluacionAtributoComponent, {
      header: 'Evaluación de P.T.',
      width: '80%',
      data: {
        id,
        esPT: true,
      }
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {

        const { envasadora, orden } = this.dataEnvGra;

        const data = {
          envasadoraId: envasadora.Id,
          orden: orden.Orden,
          ...result
        }
        this.granelService.saveGranelEvaluacionPT( data )
          .subscribe( ( data: any ) => {
            if ( data.success ) {
              this.listarEvaluaciones();
            }
          })
      }
    });
  }

  goToChecklist() {

    if (!this.PERMISOS_CHK.ESCRITURA) {
      this.messageService.add({ severity: 'warn', summary: 'Advertencia', detail: 'No tiene permiso para realizar esta acción.' });
      return;
    }

    this.granelService
      .getChecklist(this.dataEnvGra?.envasadora?.Id, this.dataEnvGra?.orden?.Orden)
      .subscribe((resp) => {

        if ( resp.data ) {
          this.router.navigate(['/envasado-granel/checklist']);
        } else {
          this.createChecklist();
        }

      });

  }

  createChecklist() {
    this.confirmationService.confirm({
      message:
        '<p>Se generará un <b>nuevo Checklist</b>. ¿Desea continuar con la generación?.</p>',
      accept: () => {
        this.granelService
            .postChecklist(this.dataEnvGra?.envasadora?.Id, this.dataEnvGra?.orden?.Orden)
            .subscribe((res) => {
              if (res.success) {
                this.router.navigate(['/envasado-granel/checklist']);
              }
            });
      },
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

  goToControl() {
    this.router.navigate(['/envasado-granel/control']);
  }

  verCodificacion( imagen: string, contentType: string ) {
    this.dialogService.open(VisorImagenComponent, {
      header: 'VISUALIZACIÓN DE IMAGEN',
      width: '50%',
      data: {
        imagen,
        contentType
      }
    });
  }

  verChecklist( id: number ) {
    this.router.navigate([`envasado-granel/checklist/detalle/${id}`]);
  }

  printCheckListArranqueGranelEnvasado(id: number) {
    this.granelService.printCheckListArranqueGranelEnvasado(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
