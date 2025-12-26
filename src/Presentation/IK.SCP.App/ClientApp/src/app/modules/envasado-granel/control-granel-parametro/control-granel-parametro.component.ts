import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { NuevoControlGranelComponent } from '../modales/nuevo-control-granel/nuevo-control-granel.component';
import { GranelService } from 'src/app/services/envasado/granel.service';
import { DataEnvasadoGranelStorage } from 'src/app/core/models/envasado/envasado-granel-data';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { DatePipe } from '@angular/common';
import { SecurityService } from 'src/app/services/security.service';
import { ACCION_ENV_GRANEL_CONTROL_PARAMETROS } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';

@Component({
  selector: 'app-control-granel-parametro',
  templateUrl: './control-granel-parametro.component.html',
  styleUrls: ['./control-granel-parametro.component.scss'],
  providers: [DialogService, DatePipe]
})
export class ControlGranelParametroComponent implements OnInit {

  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  };
  
  dataEnvGra!: DataEnvasadoGranelStorage;

  columnas: any[] = [{ field: 'Parametro', header: ''}];
  controles: any[] = [];

  observaciones: any[] = [];
  observacion = '';

  constructor(
    private router: Router,
    public dialogService: DialogService,
    private granelService: GranelService,
    private storageAppService: StorageAppService,
    private datePipe: DatePipe,
    public securityService: SecurityService,
  ) { 
    this.securityService.validarAcciones([
      ACCION_ENV_GRANEL_CONTROL_PARAMETROS,
    ]).then(resp => {
      this.PERMISOS = resp[0];
    })
  }

  ngOnInit(): void {

    this.dataEnvGra = this.storageAppService.DataEnvasadoGranel;
    const { envasadora, orden  } = this.dataEnvGra;
    this.cargarControl(envasadora.Id, orden.Orden);
    this.cargarObservaciones(envasadora.Id, orden.Orden);
  }

  cargarControl( envasadoraId:number, orden:string ) {
    
    this.granelService.getAllGranelParametrosControl(
      envasadoraId,
      orden
    ).subscribe( resp => {

      if ( resp.success ) {
        this.columnas = [];
        const fechas: Date[] = resp.data.cabeceras;
        fechas.forEach(fecha => {
          this.columnas.push({ field: this.datePipe.transform(fecha, 'dd/MM/yyyy HH:mm:ss'), header: this.datePipe.transform(fecha, 'dd/MM/yy HH:mm')  })
        });
        this.controles = resp.data.controles;
      }

    });
  }

  cargarObservaciones( envasadoraId:number, orden:string ) {
    this.granelService.getAllGranelObservacionControl(
      envasadoraId,
      orden
    ).subscribe( resp => {

      if ( resp.success ) {
        this.observaciones = resp.data;
      }

    });
  }

  regresar() {
    this.router.navigate(['/envasado-granel']);
  }

  agregar() { 
    this.granelService.getAllParametrosControl()
      .subscribe( resp => {
 
        if( resp.success ) {
  
          const ref = this.dialogService.open(NuevoControlGranelComponent, {
            header: 'Nuevo Control de Parámetros',
            width: '50%',
            data: {
              parametros: resp.data
            }
          });
  
          ref.onClose.subscribe( result => {
  
            if ( result ) {
 
              const { envasadora, orden  } = this.dataEnvGra;
  
              const data = {
                envasadoraId: envasadora.Id,
                orden: orden.Orden,
                parametros: result,
              }
              
              this.granelService.saveGranelParametrosControl( data )
                .subscribe( () => this.cargarControl(envasadora.Id, orden.Orden) );
            }
  
          });

        }

      });
  }

  agregarObservacion(){

    if( this.observacion.length < 4 ) return;

    const { envasadora, orden  } = this.dataEnvGra;
  
    const data = {
      envasadoraId: envasadora.Id,
      orden: orden.Orden,
      observacion: this.observacion
    }
    
    this.granelService.saveGranelObservacionControl( data )
      .subscribe( () => {
        this.cargarObservaciones(envasadora.Id, orden.Orden);
        this.observacion = '';
      });
  }
}
