import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import {
  ACCION_ACO_CHECKLIST_ARRANQUE_MAIZ,
  ACCION_ACO_CHECKLIST_LAVADO_TUBERCULO,
  ACCION_ACO_CONTROL_PEF,
} from 'src/app/core/constants/accion.constant';
import { ControlAcondicionamiento } from 'src/app/core/constants/acondicionamiento.constant';
import {
  MESSAGE_ERROR,
  MESSAGE_NO_ACCESS,
} from 'src/app/core/constants/mensaje.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { AcondicionamientoService } from 'src/app/services/acondicionamiento/acondicionamiento.service';
import { ArranqueElectroporadorService } from 'src/app/services/acondicionamiento/arranque-electroporador.service';
import { ArranqueLavadoTuberculoService } from 'src/app/services/acondicionamiento/arranque-lavado-tuberculo.service';
import { ArranqueMaizService } from 'src/app/services/acondicionamiento/arranque-maiz.service';
import { ControlPefService } from 'src/app/services/acondicionamiento/control-pef.service';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-panel-control',
  templateUrl: './panel-control.component.html',
  styleUrls: ['./panel-control.component.scss'],
})
export class PanelControlComponent implements OnInit {
  PERMISOS_CHL_MAIZ: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };
  PERMISOS_CHL_LAVADO_TUBERCULO: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };
  PERMISOS_CONTROL_PEF: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  listaControles: any[] = [];
  listaOpciones: any[] = [];

  listaArranquesMaiz: any[] = [];
  listaArranquesLavadoTuberculo: any[] = [];
  listaArranquesElectroporador: any[] = [];

  constructor(
    private acondicionamientoService: AcondicionamientoService,
    private arranqueMaizService: ArranqueMaizService,
    private arranqueLavadoTuberculoService: ArranqueLavadoTuberculoService,
    private arranqueElectroporadorService: ArranqueElectroporadorService,
    private controlPefService: ControlPefService,
    private securityService: SecurityService,
    private storageAppService: StorageAppService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {
    this.securityService
      .validarAcciones([
        ACCION_ACO_CHECKLIST_ARRANQUE_MAIZ,
        ACCION_ACO_CHECKLIST_LAVADO_TUBERCULO,
        ACCION_ACO_CONTROL_PEF,
      ])
      .then((resp) => {
        this.PERMISOS_CHL_MAIZ = resp[0];
        this.PERMISOS_CHL_LAVADO_TUBERCULO = resp[1];
        this.PERMISOS_CONTROL_PEF = resp[2];
      });
  }

  get MostrarListadoArranqueMaiz(): boolean {
    return this.listaControles.some(
      (f) => f.codigo === ControlAcondicionamiento.CHECKLIST_DE_ARRANQUE_MAIZ
    );
  }

  get MostrarListadoArranqueLavadoTuberculos(): boolean {
    return this.listaControles.some(
      (f) => f.codigo === ControlAcondicionamiento.ARRANQUE_LAVADO_TUBERCULOS
    );
  }

  get MostrarListadoArranqueElectroporador(): boolean {
    return this.listaControles.some(
      (f) => f.codigo === ControlAcondicionamiento.CHECKLIST_PEF
    );
  }

  ngOnInit(): void {
    const data = this.storageAppService.DataAcondicionamiento;

    this.acondicionamientoService
      .getAllProcesoMateriaPrima(data.materiaPrima?.id)
      .subscribe((resp) => {
        if (resp.success) {
          const documentos = resp.data;
          this.listaOpciones = this.securityService.listarOpciones(1216);
          documentos.forEach((doc: any) => {
            const opcion = this.listaOpciones.filter(
              (f) => f.NodoId === doc.Codigo
            )[0];
            if (opcion) {
              this.listaControles.push({
                codigo: doc.Codigo,
                nombre: doc.NombreAlterno,
                icono: opcion?.Icono,
              });
            }
          });
          this.cargarListados(data.orden);
        }
      });
  }

  cargarListados(ordenId: string) {
    if (this.MostrarListadoArranqueMaiz) this.listarArranquesMaiz(ordenId);
    if (this.MostrarListadoArranqueLavadoTuberculos)
      this.listarArranquesLavadoTuberculo(ordenId);
    if (this.MostrarListadoArranqueElectroporador)
      this.listarArranquesElectroporador(ordenId);
  }

  regresar() {
    this.router.navigate(['/acondicionamiento']);
  }

  goTo(index: number): void {
    switch (index) {
      case ControlAcondicionamiento.CHECKLIST_DE_ARRANQUE_MAIZ:
        this.cargarChecklistDeArranqueMaiz();
        break;
      case ControlAcondicionamiento.CONTROL_DE_PELADO_REMOJO_SANCOCHADO_MAIZ:
        this.router.navigate(['/acondicionamiento/control-maiz']);
        break;
      case ControlAcondicionamiento.CONTROL_DE_REPOSO_MAIZ:
        this.router.navigate(['/acondicionamiento/control-reposo-maiz']);
        break;
      case ControlAcondicionamiento.CONTROL_DE_REMOJO_HABA:
        this.router.navigate(['/acondicionamiento/control-remojo-haba']);
        break;
      case ControlAcondicionamiento.ARRANQUE_LAVADO_TUBERCULOS:
        this.cargarArranqueLavadoTuberculo();
        break;
      case ControlAcondicionamiento.CHECKLIST_PEF:
        this.cargarArranqueElectroporador();
        break;
      case ControlAcondicionamiento.CONTROL_PEF:
        this.cargarControlPef();
        break;

      default:
        break;
    }
  }

  private cargarControlPef() {
    if (
      this.PERMISOS_CONTROL_PEF.LECTURA ||
      this.PERMISOS_CONTROL_PEF.REVISION ||
      this.PERMISOS_CONTROL_PEF.ESCRITURA
    ) {
      const dataAco = this.storageAppService.DataAcondicionamiento;
      this.controlPefService.get(dataAco.orden).subscribe((resp) => {
        if (resp.success) {
          if (resp.data) {
            this.router.navigate(['/acondicionamiento/control-electroporador']);
          } else {
            this.crearControlPef();
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: MESSAGE_ERROR,
          });
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: `${MESSAGE_NO_ACCESS} consultar Control PEF`,
      });
    }
  }

  private crearControlPef() {
    if (this.PERMISOS_CONTROL_PEF.ESCRITURA) {
      const dataAcond = this.storageAppService.DataAcondicionamiento;
      const data = { ordenId: dataAcond.orden };
      this.controlPefService.create(data).subscribe((resp) => {
        if (resp.success) {
          this.router.navigate(['/acondicionamiento/control-electroporador']);
        } else {
          this.messageService.add({
            severity: 'warn',
            summary: 'Advertencia',
            detail: MESSAGE_ERROR,
          });
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: `${MESSAGE_NO_ACCESS} generar Control de PEF`,
      });
    }
  }

  cargarArranqueElectroporador() {
    const dataAco = this.storageAppService.DataAcondicionamiento;
    this.arranqueElectroporadorService
      .getOpen(dataAco.orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data) {
            this.router.navigate([
              '/acondicionamiento/arranque-electroporador',
            ]);
          } else {
            //if (this.PERMISOS_CHL_LAVADO_TUBERCULO.ESCRITURA) {
            this.confirmationService.confirm({
              message:
                'Se generará un nuevo <b>Checklist de Arranque de Electroporador</b>. ¿Desea continuar con la generación?',
              accept: () => {
                const data = {
                  ordenId: dataAco.orden,
                };
                this.arranqueElectroporadorService
                  .insert(data)
                  .subscribe((resp) => {
                    if (resp.success) {
                      this.router.navigate([
                        '/acondicionamiento/arranque-electroporador',
                      ]);
                    } else {
                    }
                  });
              },
            });
            // } else {
            // }
          }
        } else {
        }
      });
  }

  cargarArranqueLavadoTuberculo() {
    const dataAco = this.storageAppService.DataAcondicionamiento;
    this.arranqueLavadoTuberculoService
      .getArranqueLavadoTuberculoActivo(dataAco.orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data) {
            this.router.navigate([
              '/acondicionamiento/arranque-lavado-tuberculo',
            ]);
          } else {
            if (this.PERMISOS_CHL_LAVADO_TUBERCULO.ESCRITURA) {
              this.confirmationService.confirm({
                message:
                  'Se generará un nuevo <b>Checklist de Arranque de Lavado de Tubérculos</b>. ¿Desea continuar con la generación?',
                accept: () => {
                  const data = {
                    ordenId: dataAco.orden,
                  };
                  this.arranqueLavadoTuberculoService
                    .insertArranqueLavadoTuberculoActivo(data)
                    .subscribe((resp) => {
                      if (resp.success) {
                        this.router.navigate([
                          '/acondicionamiento/arranque-lavado-tuberculo',
                        ]);
                      } else {
                      }
                    });
                },
              });
            } else {
            }
          }
        } else {
        }
      });
  }

  cargarChecklistDeArranqueMaiz() {
    const dataAco = this.storageAppService.DataAcondicionamiento;
    this.arranqueMaizService
      .getArranqueMaizActivo(dataAco.orden)
      .subscribe((resp) => {
        if (resp.success) {
          if (resp.data) {
            this.router.navigate(['/acondicionamiento/arranque-maiz']);
          } else {
            if (this.PERMISOS_CHL_MAIZ.ESCRITURA) {
              this.confirmationService.confirm({
                message:
                  'Se generará un nuevo <b>Checklist de Arranque de Maíz</b>. ¿Desea continuar con la generación?',
                accept: () => {
                  const data = {
                    ordenId: dataAco.orden,
                  };
                  this.arranqueMaizService
                    .insertArranqueMaizActivo(data)
                    .subscribe((resp) => {
                      if (resp.success) {
                        this.router.navigate([
                          '/acondicionamiento/arranque-maiz',
                        ]);
                      } else {
                      }
                    });
                },
              });
            }
          }
        } else {
        }
      });
  }

  listarArranquesMaiz(ordenId: string) {
    this.arranqueMaizService.getAllArranqueMaiz(ordenId).subscribe((resp) => {
      if (resp.success) {
        this.listaArranquesMaiz = resp.data;
      }
    });
  }

  listarArranquesLavadoTuberculo(ordenId: string) {
    this.arranqueLavadoTuberculoService
      .getAllArranqueLavadoTuberculo(ordenId)
      .subscribe((resp) => {
        if (resp.success) {
          this.listaArranquesLavadoTuberculo = resp.data;
        }
      });
  }

  listarArranquesElectroporador(ordenId: string) {
    this.arranqueElectroporadorService.getAll(ordenId).subscribe((resp) => {
      if (resp.success) {
        this.listaArranquesElectroporador = resp.data;
      }
    });
  }

  verArranqueLavadoTuberculo(id: number) {
    this.router.navigate([
      '/acondicionamiento/detalle-arranque-lavado-tuberculo',
      id,
    ]);
  }

  verArranqueMaiz(id: number) {
    this.router.navigate(['/acondicionamiento/detalle-arranque-maiz', id]);
  }

  verArranqueElectroporador(id: number) {
    this.router.navigate([
      '/acondicionamiento/detalle-arranque-electroporador',
      id,
    ]);
  }

  printCheckListArranqueElectroporador(id: number){
    this.arranqueElectroporadorService.printCheckListArranqueElectroporador(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }

  printCheckListArranqueLavadoTuberculos(id: number){
    this.arranqueLavadoTuberculoService.printCheckListArranqueLavadoTuberculos(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }

  printCheckListArranqueMaiz(id: number) {
    this.arranqueMaizService.printCheckListArranqueMaiz(id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
