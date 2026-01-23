import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import {
  ACCION_ENV_BLENDING_CHECKLIST,
  ACCION_ENV_CHECKLIST_ARRANQUE_ENVASADO,
  ACCION_ENV_CHECKLIST_ARRANQUE_MAQUINA,
  ACCION_ENV_REGISTRO_PEDACERIA,
} from 'src/app/core/constants/accion.constant';
import { ControlEnvasado } from 'src/app/core/constants/envasado.constant';
import { MESSAGE_NO_ACCESS } from 'src/app/core/constants/mensaje.constant';
import { eModulo } from 'src/app/core/enums/modulo.enum';
import { PostArranqueMaquinaRequest } from 'src/app/core/models/envasado/arranque-maquina.model';
import { Envasadora } from 'src/app/core/models/envasado/envasado-data';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { BlendingService } from 'src/app/services/blending/blending.service';
import { ArranqueMaquinaService } from 'src/app/services/envasado/arranque-maquina.service';
import { ArranqueService } from 'src/app/services/envasado/arranque.service';
import { OrdenService } from 'src/app/services/envasado/orden.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { environment } from 'src/environments/environment';
import { RegistroPedaceriaComponent } from './modales/registro-pedaceria/registro-pedaceria.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-panel-control',
  templateUrl: './panel-control.component.html',
  styleUrls: ['./panel-control.component.scss'],
})
export class PanelControlComponent implements OnInit {
  PERMISOS_CHL_MAQ: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  PERMISOS_BLEND_CHL_MAQ: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  PERMISOS_CHL_ENV: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  PERMISOS_REG_PED: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  dataEnvasadora!: Envasadora;
  dataOrden!: OrdenEnvasado;

  aplicaciones: any[] = [];

  arranques_maquina = [];
  arranques_envasado = [];
  arranques_blending = [];
  registros_pedaceria: any[] = [];

  mostrarBlending = false;

  constructor(
    private router: Router,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private arranqueService: ArranqueService,
    private ordenService: OrdenService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private blendingService: BlendingService,
    private storageAppService: StorageAppService,
    private securityService: SecurityService,
    private dialogService: DialogService
  ) {
    this.securityService
      .validarAcciones([
        ACCION_ENV_CHECKLIST_ARRANQUE_MAQUINA,
        ACCION_ENV_CHECKLIST_ARRANQUE_ENVASADO,
        ACCION_ENV_BLENDING_CHECKLIST,
        ACCION_ENV_REGISTRO_PEDACERIA,
      ])
      .then((resp) => {
        this.PERMISOS_CHL_MAQ = resp[0];
        this.PERMISOS_CHL_ENV = resp[1];
        this.PERMISOS_BLEND_CHL_MAQ = resp[2];
        this.PERMISOS_REG_PED = resp[3];
      });
  }

  ngOnInit(): void {
    this.cargarDataEnvasado();

    this.cargarAplicaciones();

    this.cargarListaArranqueMaquina();

    this.cargarListaArranqueEnvasado();

    this.cargarListaBlendingArranque();

    this.cargarRegistroPedaceria();
  }

  cargarAplicaciones() {
    let aplicaciones = this.securityService.listarOpciones(eModulo.ENVASADO);

    this.blendingService
      .validateMezcla(this.dataOrden.Articulo)
      .subscribe((resp) => {
        if (!resp.success) {
          aplicaciones = aplicaciones.filter(
            (x) =>
              x.NodoId != ControlEnvasado.BLENDING_CONTROL &&
              x.NodoId != ControlEnvasado.BLENDING_ARRANQUE
          );
          this.mostrarBlending = false;
        } else {
          this.mostrarBlending = true;
        }

        aplicaciones.sort((a, b) => {
          if (a.Orden > b.Orden) {
            return 1;
          }
          if (a.Orden < b.Orden) {
            return -1;
          }
          return 0;
        });

        this.aplicaciones = aplicaciones;
      });
  }

  cargarDataEnvasado() {
    const data = this.storageAppService.DataEnvasado;

    this.dataEnvasadora = data.envasadora;
    this.dataOrden = data.orden;
  }

  cargarListaArranqueMaquina() {
    this.arranqueMaquinaService
      .getAll(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((res: any) => {
        this.arranques_maquina = res.data;
      });
  }

  cargarListaArranqueEnvasado() {
    this.arranqueService
      .getAllArranque(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((res: any) => {
        this.arranques_envasado = res.data;
      });
  }

  cargarListaBlendingArranque() {
    this.blendingService
      .getAllArranques(this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.success) {
          this.arranques_blending = resp.data;
        }
      });
  }

  cargarRegistroPedaceria() {
    this.ordenService
      .getAllRegistroPedaceria(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data.length > 0) {
            this.registros_pedaceria = resp.data;
          }
        }
      });
  }

  goTo(idAplicacion: number) {
      console.log('CLICK NodoId:', idAplicacion);
    if (idAplicacion === 0) {
      window.location.href = environment.UrlBandejaEnvasado;
    }

    if (idAplicacion === 1) {
      this.router.navigate(['/']);
    }

    if (idAplicacion === ControlEnvasado.ARRANQUE_MAQUINA) {
      this.cargarArranqueMaquina();
    }

    if (idAplicacion === ControlEnvasado.ARRANQUE) {
      this.cargarArranqueEnvasado();
    }

    if (idAplicacion === ControlEnvasado.BLENDING_ARRANQUE) {
      this.cargarBlendingArranque();
    }

    if (idAplicacion === ControlEnvasado.BLENDING_CONTROL) {
      this.cargarBlendingControl();
    }

    if (idAplicacion === ControlEnvasado.REGISTRO_PEDACERIA) {
      this.openRegistroPedaceria();
    }
  }

  cargarArranqueMaquina() {
    if (!this.PERMISOS_CHL_MAQ.ESCRITURA) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'No tiene permiso para realizar esta acción.',
      });
      return;
    }

    this.spinner.show();
    // Obtener arranque maquina abierto
    this.arranqueMaquinaService
      .getOpen(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((resp) => {

        if (resp.data) {
          // Si existiera redirecciona a arranque
          this.spinner.hide();
          this.router.navigate(['/envasado/arranque-maquina']);
        } else {
          // Si no existe, solicita confirmación para crearlo
          this.spinner.hide();
          this.confirmationService.confirm({
            message:
              'Se generará un nuevo <b>Checklist de Arranque de Máquina</b>. ¿Desea continuar con la generación?',
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
                observacion: '',
              };
              this.arranqueMaquinaService.save(data).subscribe((resp) => {

                if (resp.ok) {
                  this.spinner.hide();
                  this.router.navigate(['/envasado/arranque-maquina']);
                } else {
                  this.spinner.hide();
                }
              });
            },
          });
        }
      });
  }

  cargarArranqueEnvasado() {
    this.arranqueService
      .getArranque(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.ok) {
          if (resp.data) {
            this.router.navigate(['/envasado/arranque']);
          } else {
            if (!this.PERMISOS_CHL_ENV.ESCRITURA) {
              this.messageService.add({
                severity: 'warn',
                summary: 'Advertencia',
                detail: 'No tiene permiso para realizar esta acción.',
              });
              return;
            }

            this.confirmationService.confirm({
              message:
                'Se generará un nuevo <b>Checklist de Arranque de Envasado</b>. ¿Desea continuar con la generación?',
              accept: () => {
                const data = {
                  envasadoraId: this.dataEnvasadora.Id,
                  ordenId: this.dataOrden.Orden,
                };

                this.arranqueService
                  .postArranqueActivo(data)
                  .subscribe((resp) => {
                    if (resp.ok) {
                      this.router.navigate(['/envasado/arranque']);
                    } else {
                      this.messageService.add({
                        severity: 'error',
                        summary: 'Mensaje de Error',
                        detail: 'No se pudo generar nuevo Arranque.',
                      });
                    }
                  });
              },
            });
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Mensaje de Error',
            detail: 'No se pudieron obtener datos de Arranque.',
          });
        }
      });
  }

  cargarBlendingArranque() {
    if (!this.PERMISOS_BLEND_CHL_MAQ.ESCRITURA) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'No tiene permiso para realizar esta acción.',
      });
      return;
    }

    this.blendingService
      .getArranqueActivo(this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data) {
            this.router.navigate(['/blending/checklist']);
          } else {
            this.confirmationService.confirm({
              message:
                'Se generará un nuevo <b>Checklist de Arranque</b>. ¿Desea continuar con la generación?',
              accept: () => {
                this.spinner.show();
                const data: any = {
                  orden: this.dataOrden.Orden,
                };

                this.blendingService.postArranque(data).subscribe((resp) => {
                  if (resp.success) {
                    this.spinner.hide();
                    this.router.navigate(['/blending/checklist']);
                  } else {
                    this.spinner.hide();
                  }
                });
              },
            });
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Mensaje de Error',
            detail: 'No se pudieron obtener datos de Arranque.',
          });
        }
      });
  }

  cargarBlendingControl() {
    this.blendingService
      .getAllControlComponentes(this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data.length > 0) {
            this.router.navigate(['/blending/control']);
          } else {
            this.blendingService
              .insertControlComponente({
                orden: this.dataOrden.Orden,
              })
              .subscribe((resp) => {
                if (resp.success) {
                  this.router.navigate(['/blending/control']);
                }
              });
          }
        }
      });
  }

  verArranqueMaquina(id: number) {
    this.router.navigate([`/envasado/arranque-maquina/${id}`]);
  }

  verArranqueEnvasado(id: number) {
    this.router.navigate([`/envasado/arranque/${id}`]);
  }

  verBlendingArranque(id: number) {
    this.router.navigate([`/blending/detalle-checklist/${id}`]);
  }

  openRegistroPedaceria() {
    if (this.PERMISOS_REG_PED.ESCRITURA) {
      const ref = this.dialogService.open(RegistroPedaceriaComponent, {
        header: 'Registro de Pedacería',
        width: '500px',
      });

      ref.onClose.subscribe((result) => {
        if (result) {
          const data = {
            envasadoraId: this.dataEnvasadora.Id,
            ordenId: this.dataOrden.Orden,
            ...result,
          };
          this.guardarRegistroPedaceria(data);
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: `${MESSAGE_NO_ACCESS} registrar pedacería.`,
      });
    }
  }

  guardarRegistroPedaceria(data: any) {
    this.ordenService.saveRegistroPedaceria(data).subscribe((_) => {
      this.cargarRegistroPedaceria();
    });
  }

  public validarVisibilidad(idOpcion: number): boolean {
    if (idOpcion === ControlEnvasado.REGISTRO_PEDACERIA)
      return this.PERMISOS_REG_PED.ESCRITURA;

    return true;
  }

  public printArranqueMaquinaEnvasado(id:number) {
    this.arranqueMaquinaService.printArranqueMaquinaEnvasado(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }

  public printArranqueEnvasado(id:number) {
    this.arranqueService.printCheckListArranqueEnvasado(this.dataEnvasadora.Id, this.dataOrden.Orden, id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
  public printChecklistArranqueBlending(id:number) {
    this.blendingService.printChecklistArranqueBlending(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
  public printRegistrosPedaceria() {
    this.ordenService.printRegistrosPedaceria(this.dataEnvasadora.Id, this.dataOrden.Orden).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
