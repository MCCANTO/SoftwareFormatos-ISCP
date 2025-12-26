import { SecurityService } from './../../../services/security.service';
import { VerificacionEquipoComponent } from './../../../components/verificacion-equipo/verificacion-equipo.component';
import { Condicion, CondicionPrevia } from './../../../core/models/fritura/arranque-maquina.interface';
import { ArranqueMaquinaService } from './../../../services/fritura/arranque-maquina.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';

import { DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { VerificacionEquipoService } from 'src/app/services/fritura/verificacion-equipo.service';
import { ArranqueMaquinaVerificacionEquipo } from 'src/app/core/models/fritura/verificacion-equipo.interface';
import { ArranqueMaquina } from 'src/app/core/models/fritura/arranque-maquina.interface';
import { JwtTokenService } from 'src/app/services/jwt-token.service';

import { ConfirmationService, MessageService } from 'primeng/api';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { ACCION_FR_CHECKLIST_ARRANQUE_MANUFACTURA } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';

@Component({
  selector: 'app-arranque-maquina',
  templateUrl: './arranque-maquina.component.html',
  styleUrls: ['./arranque-maquina.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService]
})
export class ArranqueMaquinaComponent implements OnInit {

  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  }

  id = 0;

  dataFR: DataFrituraStorage = {};

  dataArranqueMaquina!: ArranqueMaquina;

  observacion = '';

  constructor(
    private dialogService: DialogService,
    private router: Router,
    private verificacionEquipoService: VerificacionEquipoService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    // private jwtTokenService: JwtTokenService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private storageAppService: StorageAppService,
    public securityService: SecurityService,
  ) { 
    this.securityService.validarAcciones([
      ACCION_FR_CHECKLIST_ARRANQUE_MANUFACTURA,
    ]).then(resp => {
      this.PERMISOS = resp[0];
    })
  }

  ngOnInit(): void {

    this.cargarData();

    this.cargarArranqueMaquina();

  }

  // get EsMaquinista(): boolean {
  //   return this.jwtTokenService.esMaquinistaFr();
  // }


  cargarArranqueMaquina() {

    this.arranqueMaquinaService.getOpen(this.dataFR.freidora?.Id ?? 0, this.dataFR.orden?.Orden ?? '')
      .subscribe(resp => {

        if (resp.ok) {

          if (resp.data) {
            this.dataArranqueMaquina = resp.data;
          } else {

          }

        } else {
          this.router.navigate(['/fritura']);
        }

      });

  }

  cargarData() {
    this.dataFR = this.storageAppService.DataFritura;
  }

  openModalVerificacionEquipo() {
    this.cargarDetalleVerficiacion(0);
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(verificacion.arranqueMaquinaVerificacionEquipoCabId, verificacion.cerrado);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {

    this.verificacionEquipoService.getAllDetalle(1, this.dataFR.freidora?.Id ?? 0, id)
      .subscribe(resp => {
        if (resp.success) {

          const variables = resp.data;

          const vb_ref = this.dialogService.open(VerificacionEquipoComponent, {
            header: 'Verificación de Equipo previa al arranque',
            width: '95%',
            data: {
              mostrarTipo: false,
              esEditable: this.PERMISOS.ESCRITURA && !cerrado,
              cerrado,
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

    const data: ArranqueMaquinaVerificacionEquipo = {
      arranqueMaquinaVerificacionEquipoCabId: id,
      arranqueMaquinaId: this.dataArranqueMaquina.arranqueMaquinaId,
      verificaciones: verificacionesEquipo
    }

    this.verificacionEquipoService.save(data)
      .subscribe(resp => {
        this.cargarArranqueMaquina();
      })
  }

  agregarObservacion() {
    if (this.observacion) {

      const data = {
        arranqueMaquinaId: this.dataArranqueMaquina.arranqueMaquinaId,
        observacion: this.observacion,
      }
      this.arranqueMaquinaService.insertObservacion(data)
        .subscribe(resp => {
          this.observacion = '';
          this.cargarArranqueMaquina();
        });

    }
  }

  regresar() {
    this.router.navigate(['/fritura']);
  }

  guardarCondiciones() {

    let condiciones: Condicion[] = []

    this.dataArranqueMaquina.condiciones.forEach(cond => {
      condiciones.push({
        arranqueMaquinaCondicionPreviaId: cond.arranqueMaquinaCondicionPreviaId,
        condicionPreviaId: cond.condicionPreviaId,
        valor: cond.valor,
        observacion: cond.observacion ?? ''
      });
    });

    const data: CondicionPrevia = {
      arranqueMaquinaId: 0,
      condiciones
    }

    this.arranqueMaquinaService.saveCondicionePrevia(data)
      .subscribe(resp => {
        this.cargarArranqueMaquina();
      });

  }

  guardar() {

    this.confirmationService.confirm({
      message: '¿Está seguro(a) de cerrar la información? <p>Una vez <b>cerrado</b> no podrá registrar más información.</p>',
      accept: () => {
        const data = {
          arranqueMaquinaId: this.dataArranqueMaquina.arranqueMaquinaId
        };
        this.arranqueMaquinaService.update(data)
          .subscribe(resp => {
            this.router.navigate(['/fritura']);
          });
      }
    });

  }
}
