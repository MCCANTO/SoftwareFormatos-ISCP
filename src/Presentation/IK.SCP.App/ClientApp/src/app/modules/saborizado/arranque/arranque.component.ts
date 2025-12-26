import { SazonadorService } from './../../../services/sazonado/sazonador.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';

@Component({
  selector: 'app-arranque',
  templateUrl: './arranque.component.html',
  styleUrls: ['./arranque.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService]
})
export class ArranqueComponent implements OnInit {

  id = 0;

  observacion = '';

  dataArranque!: any;

  constructor(
    private dialogService: DialogService,
    private sazonadorService: SazonadorService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
  ) {

    this.id = this.activatedRoute.snapshot.params['id'];

  }

  ngOnInit(): void {

    this.cargarArranque();

  }

  cargarArranque() {
    this.sazonadorService.getByIdArranque(this.id)
    .subscribe( resp => {
      if( resp.success ) this.dataArranque = resp.data;
    });
  }

  verVariables( variable: any ) {
    this.cargarDetalleVerficiacion(variable.ArranqueVerificacionEquipoId, variable.Cerrado);
  }

  agregarVerificacion() {
    this.cargarDetalleVerficiacion(0);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {

    this.sazonadorService.getAllVerificacionEquipoDetalle(id)
      .subscribe(resp => {
        if (resp.success) {

          const variables = resp.data;
          
          const vb_ref = this.dialogService.open(VerificacionEquipoComponent, {
            header: 'Verificación de Equipo previa al arranque',
            width: '95%',
            data: {
              mostrarTipo: false,
              esEditable: !cerrado && !this.dataArranque.cerrado,
              cerrado: cerrado || this.dataArranque.cerrado,
              variables
            }
          });

          vb_ref.onClose.subscribe((result: any) => {

            if (result) {
       
              this.confirmationService.confirm({
                message: '¿Está seguro(a) de guardar la información? <p>Una vez <b>guardada</b> no podrá modificar dicha información.</p>',
                accept: () => {
                  this.guardarVerificacionEquipo(id, result)
                }
              });

            }

          });
        }
      });
  }

  guardarVerificacionEquipo(id: number, verificaciones: any) {

    let verificacionesEquipo: any[] = [];
    verificaciones.forEach((item: any) => {
      verificacionesEquipo.push(...item.detalle);
    });

    const data = {
      arranqueId: this.id,
      arranqueVerificacionEquipoId: id,
      verificaciones: verificacionesEquipo
    }
  
    this.sazonadorService.saveVerificacionEquipoDetalle(data)
      .subscribe(resp => {
        this.cargarArranque();
      })
  }

  guardarObservacion(){
    const obs = this.observacion;

    if (obs.length > 4) {
      const data = { arranqueId: this.id, observacion: obs };
      this.sazonadorService.insertArranqueObservacion( data )
        .subscribe( resp => {
          this.cargarArranque();
          this.observacion = '';
        });
    }
  }

  guardarCondiciones() {

    const data = { 
      arranqueId: this.id,
      condiciones: this.dataArranque.condiciones 
    };

    this.sazonadorService.insertArranqueCondicion( data )
        .subscribe( resp => {
          if( resp.ok ) this.cargarArranque();
        });

  }

  guardarVariables() {

    const data = {
      arranqueId: this.id,
      pesoInicio: this.dataArranque.pesoInicio,
      pesoFin: this.dataArranque.pesoFin,
      observacionInicio: this.dataArranque.observacionInicio,
      observacionFin: this.dataArranque.observacionFin
    }

    this.sazonadorService.insertVariableBasica( data )
      .subscribe(resp => {});

  }

  regresar() {
    this.router.navigate(['/sazonado']);
  }

  cerrar() {
    this.confirmationService.confirm({
      message: '¿Está seguro(a) de cerrar la información? <p>Una vez <b>cerrada</b> no podrán registrar más datos.</p>',
      accept: () => {
        this.sazonadorService.closeArranque(
          { arranqueId: this.id }
        ).subscribe( resp => {
          this.router.navigate(['/sazonado']);        
        });
      }
    });
  }
}
