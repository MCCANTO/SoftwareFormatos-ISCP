import { NuevoControlComponent } from './modales/nuevo-control/nuevo-control.component';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { ACCION_ENV_BLENDING_CONTROL } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { BlendingService } from 'src/app/services/blending/blending.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control',
  templateUrl: './control.component.html',
  styleUrls: ['./control.component.scss'],
  providers: [ DialogService ]
})
export class ControlComponent implements OnInit {

  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  };

  cols: any[] = [];

  data: any[] = [];
  mermas: any[] = [];

  dataBlending: any;

  componentes: any[] = []

  constructor(
    private dialogService: DialogService,
    private router: Router,
    private blendingService: BlendingService,
    private storageAppService: StorageAppService,
    public securityService: SecurityService,
  ) {
    this.securityService.validarAcciones([
      ACCION_ENV_BLENDING_CONTROL,
    ]).then(resp => {
      this.PERMISOS = resp[0];
    })
  }

  async ngOnInit(): Promise<void> {

    this.dataBlending = this.storageAppService.DataEnvasado;

    this.cargarComponentes();

    this.cargarControl();

    this.cargarMerma();
  }

  cargarMerma() {
    this.blendingService.getAllControlMerma(this.dataBlending.orden.Orden)
    .subscribe( resp => {
        if (resp.success) {
          this.mermas = resp.data;
        }
      });
  }

  cargarComponentes() {

    const { orden } = this.dataBlending;
    this.blendingService.getAllControlComponentes(orden.Orden)
    .subscribe( resp => {
        if (resp.success) {
          this.componentes = resp.data;
        }
      });
  }

  agregar() {
    const vb_ref = this.dialogService.open(NuevoControlComponent, {
      header: 'Nuevo Control de Blending',
      width: '95%',
      data: {
        componentes: this.componentes
      }
    });

    vb_ref.onClose.subscribe( result => {

      if ( result ) {
        this.guardarNuevoControl( result );
      }

    })
  }

  guardarNuevoControl( dataControl: any ) {
    const data = {
      orden: this.dataBlending.orden.Orden,
      ...dataControl
    };

    this.blendingService.insertControl( data )
      .subscribe( resp => {
        this.cargarControl();
      })
  }

  cargarControl() {
    this.blendingService.getAllControl( this.dataBlending.orden.Orden )
      .subscribe( resp => {
        if ( resp.success ) {
          if ( resp.data ) {
            this.cols = resp.data.columnas;
            this.data = resp.data.controles;
          }
        }
      })
  }

  obtenerDatoArticulo( data: string, index: number ) {
    let datos = ' || || ';
    if (data) datos = data;
    return datos.split('||')[index];
  }

  regresar() {
    this.router.navigate(['/envasado']);
  }

  guardar(){

    const data = {
      orden: this.dataBlending.orden.Orden,
      componentes: this.componentes
    };

    this.blendingService.updateControlComponente( data )
      .subscribe( resp => {});

    const dataMerma = {
      orden: this.dataBlending.orden.Orden,
      componentes: this.mermas,
    }

    this.blendingService.updateControlMerma( dataMerma )
      .subscribe( resp => {});
  }

  calcularPesoTotal( articulo: string ) {

    let total = 0;
    this.data.forEach(d => {

      if( d[articulo] != null) {
        const datos = d[articulo].split('||');
        total += (Number(datos[0])*Number(datos[1]))
      }

    });

    return total;

  }

  async cerrar(){

  }

  printControlBlending() {
    this.blendingService.printControlBlending(this.dataBlending.orden.Orden).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
