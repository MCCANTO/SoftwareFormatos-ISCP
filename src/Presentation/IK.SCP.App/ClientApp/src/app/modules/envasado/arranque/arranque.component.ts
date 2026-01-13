import { RevisionComponent } from './../modales/revision/revision.component';
import { InspeccionComponent } from './../modales/inspeccion/inspeccion.component';
import { ObservacionComponent } from './../modales/observacion/observacion.component';
import { ComponentesComponent } from './../modales/componentes/componentes.component';
import { PersonalComponent } from './../modales/personal/personal.component';
import { ContramuestraComponent } from './../modales/contramuestra/contramuestra.component';
import { CargaCodificacionComponent } from './../modales/carga-codificacion/carga-codificacion.component';

import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Router } from '@angular/router';
import { ArranqueService } from 'src/app/services/envasado/arranque.service';
import { VariableBasicaArranqueComponent } from '../modales/variable-basica-arranque/variable-basica-arranque.component';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { ConfirmationService, MessageService } from 'primeng/api';
import { VariableBasicaComponent } from 'src/app/components/variable-basica/variable-basica.component';
import { VisorImagenComponent } from 'src/app/components/visor-imagen/visor-imagen.component';
import { SecurityService } from 'src/app/services/security.service';
import { ACCION_ENV_CHECKLIST_ARRANQUE_ENVASADO } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { saveAs } from 'file-saver';
import {NgxSpinnerService} from "ngx-spinner";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-arranque',
  templateUrl: './arranque.component.html',
  styleUrls: ['./arranque.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService],
})
export class ArranqueComponent implements OnInit {
  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  id = 0;

  dataEnvasadora!: any;

  dataOrden!: OrdenEnvasado;

  condiciones: any[] = [];

  variablesBasicas: any[] = [];

  codificacionSobres: any[] = [];

  codificacionCajas: any[] = [];

  contramuestras: any[] = [];

  participantes: any[] = [];

  componentes: any[] = [];

  form!: FormGroup;

  observaciones: any[] = [];

  inspecciones: any[] = [];

  revisiones: any[] = [];

  constructor(
    private dialogService: DialogService,
    private arranqueService: ArranqueService,
    private router: Router,
    private fb: FormBuilder,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private localStorageService: LocalStorageService,
    private spinner: NgxSpinnerService,
    public securityService: SecurityService
  ) {
    this.securityService
      .validarAcciones([ACCION_ENV_CHECKLIST_ARRANQUE_ENVASADO])
      .then((resp) => {
        this.PERMISOS = resp[0];
      });
  }

  ngOnInit(): void {
    this.cargarDataEnvasado();

    this.arranqueService
      .getArranque(this.dataEnvasadora.Id, this.dataOrden.Orden)
      .subscribe((resp) => {
        if (resp.ok) {
          if (resp.data) {
            this.id = resp.data['arranqueId'];

            this.cargarCondicionesPrevias();

            this.cargarVariablesBasicas();

            this.cargarCodificacionSobre();

            this.cargarCodificacionCaja();

            this.cargarContramuestras();

            this.cargarPersonal();

            this.cargarComponentes();

            this.cargarObservaciones();

            this.cargarInspecciones();

            this.cargarRevisiones();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Mensaje de Error',
              detail: 'No se pudieron obtener los datos del Arranque.',
            });
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Mensaje de Error',
            detail: 'No se pudieron obtener los datos del Arranque.',
          });
        }
      });

    this.createForm();
  }

  createForm() {
    this.form = this.fb.group({

    });

  }
  cargarDataEnvasado() {
    const data = JSON.parse(
      this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ENVASADO) ??
        ''
    );

    this.dataEnvasadora = data.envasadora;
    this.dataOrden = data.orden;
  }

  cargarCodificacionCaja() {
    this.arranqueService.getAllCodificacion(this.id, 'C').subscribe((resp) => {
      if (resp.ok) this.codificacionCajas = resp.data;
    });
  }

  cargarCodificacionSobre() {
    this.arranqueService.getAllCodificacion(this.id, 'S').subscribe((resp) => {
      if (resp.ok) this.codificacionSobres = resp.data;
    });
  }

  cargarVariablesBasicas() {
    this.arranqueService.getAllVariableBasica(this.id).subscribe((resp) => {
      if (resp.ok) this.variablesBasicas = resp.data;
    });
  }

  cargarCondicionesPrevias() {
    this.arranqueService.getAllCondicionPrevia(this.id).subscribe((resp) => {
      if (resp.ok) this.condiciones = resp.data;
    });
  }

  cargarRevisiones() {
    this.arranqueService.getAllRevision(this.id).subscribe((resp) => {
      if (resp.ok) this.revisiones = resp.data;
    });
  }

  cargarInspecciones() {
    this.arranqueService.getAllInspeccion(this.id).subscribe((resp) => {
      if (resp.ok) this.inspecciones = resp.data;
    });
  }

  cargarObservaciones() {
    this.arranqueService.getAllObservacion(this.id).subscribe((resp) => {
      if (resp.ok) this.observaciones = resp.data;
    });
  }

  cargarComponentes() {
    this.arranqueService.getAllComponente(this.id).subscribe((resp) => {
      if (resp.ok) this.componentes = resp.data;
    });
  }

  cargarPersonal() {
    this.arranqueService.getAllPersonal(this.id).subscribe((resp) => {
      if (resp.ok) this.participantes = resp.data;
    });
  }

  cargarContramuestras() {
    this.arranqueService.getAllContramuestra(this.id).subscribe((resp) => {
      if (resp.ok) this.contramuestras = resp.data;
    });
  }

  agregarVariablesBasicas() {
    this.cargarVariablesBasicasDetalle(0);
  }

  cargarVariablesBasicasDetalle(
    id: number,
    cerrado: boolean = false,
    tipoId: number = 0,
    maquinista: string = ''
  ) {
    this.arranqueService.getVariableBasica(id).subscribe((resp: any) => {
      const variables = resp.data;

      const vb_ref = this.dialogService.open(VariableBasicaComponent, {
        header: 'Variables Básicas1',
        width: '95%',
        data: {
          mostrarTipo: true,
          tipoId,
          mostrarMaquinista: true,
          maquinista,
          editable: this.PERMISOS.ESCRITURA,
          cerrado,
          variables,
        },
      });

      vb_ref.onClose.subscribe((result: any) => {
        if (result) {
          let variablesBasicas: any[] = [];
          result.variables.forEach((item: any) => {
            variablesBasicas.push(...item.items);
          });

          const data = {
            arranqueId: this.id,
            arranqueVariableBasicaCabId: id,
            tipoId: result.tipoId,
            maquinista: result.maquinista.toUpperCase().trim(),
            variables: variablesBasicas,
          };

          this.arranqueService.postVariableBasica(data).subscribe((resp) => {
            this.cargarVariablesBasicas();
          });
        }
      });
    });
  }

  openModalCodificacion(tipo: string) {
    const ref = this.dialogService.open(CargaCodificacionComponent, {
      header: 'Imagen de Codificación',
      width: '60%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        this.arranqueService
          .postCodificacion({
            arranqueId: this.id,
            tipoCodificacion: tipo,
            ...result,
          })
          .subscribe((resp) => {
            this.cargarCodificacionCaja();
            this.cargarCodificacionSobre();
          });
      }
    });
  }

  openModalContramuestra() {
    const ref = this.dialogService.open(ContramuestraComponent, {
      header: 'Contramuestras',
      width: '50%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
          ...result,
        };

        this.arranqueService.postContramuestra(data).subscribe((resp) => {
          this.cargarContramuestras();
        });
      }
    });
  }

  openModalPersonal() {
    const ref = this.dialogService.open(PersonalComponent, {
      header: 'Empacador / Paletizador',
      width: '50%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
          ...result,
        };

        this.arranqueService.postPersonal(data).subscribe((resp) => {
          this.cargarPersonal();
        });
      }
    });
  }

  openModalComponente() {
    const ref = this.dialogService.open(ComponentesComponent, {
      header: 'Componentes',
      width: '50%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
          ...result,
        };

        this.arranqueService.postComponente(data).subscribe((resp) => {
          this.cargarComponentes();
        });
      }
    });
  }

  openModalObservacion() {
    const ref = this.dialogService.open(ObservacionComponent, {
      header: 'Observación',
      width: '50%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
          ...result,
        };

        this.arranqueService.postObservacion(data).subscribe((resp) => {
          this.cargarObservaciones();
        });
      }
    });
  }

  openModalInspeccion() {
    const ref = this.dialogService.open(InspeccionComponent, {
      header: 'Inspección del Etiquetado',
      width: '50%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
          ...result,
        };

        this.arranqueService.postInspeccion(data).subscribe((resp) => {
          this.cargarInspecciones();
        });
      }
    });
  }

  openModalRevision() {
    const ref = this.dialogService.open(RevisionComponent, {
      header: 'Revisión',
      width: '30%',
    });

    ref.onClose.subscribe((result: any) => {
      if (result) {
        const data = {
          arranqueId: this.id,
        };

        this.arranqueService.postRevision(data).subscribe((resp) => {
          this.cargarRevisiones();
        });
      }
    });
  }

  verVariables(variable: any) {
    this.arranqueService
      .getVariableBasica(variable.ArranqueVariableBasicaCabId)
      .subscribe((resp: any) => {
        const variables = resp.data;

        const vb_ref = this.dialogService.open(
          VariableBasicaArranqueComponent,
          {
            header: 'Variables Básicas',
            width: '95%',
            data: {
              tipoId: variable.TipoId,
              variables,
            },
          }
        );

        vb_ref.onClose.subscribe((result: any) => {
          if (result) {
            const data = {
              arranqueVariableBasicaCabId: variable.ArranqueVariableBasicaCabId,
              arranqueId: this.id,
              ...result,
            };

            this.arranqueService.postVariableBasica(data).subscribe((resp) => {
              this.cargarVariablesBasicas();
            });
          }
        });
      });
  }

  guardarCondiciones() {
    const data = this.condiciones;

    this.arranqueService.putCondicionesPrevias(data).subscribe((resp) => {
      this.cargarCondicionesPrevias();
    });
  }

  regresar() {
    this.router.navigate(['/envasado']);
  }

  cerrar() {}

  verCodificacion(imagen: string, contentType: string) {
    this.dialogService.open(VisorImagenComponent, {
      header: 'VISUALIZACIÓN DE IMAGEN',
      width: '50%',
      data: {
        imagen,
        contentType,
      },
    });
  }

  printCheckListArranqueEnvasado() {
    this.arranqueService.printCheckListArranqueEnvasado(this.dataEnvasadora.Id, this.dataOrden.Orden, this.id)
      .subscribe((response: Blob) => {
      const fileName = 'archivo-modificado.pdf';
      saveAs(response, fileName);
    }, error => {
      console.log('Error al descargar el archivo PDF', error);
    });
  }

  guardar(cerrado: boolean) {
    const mensaje = '¿Está seguro(a) de registrar la información?';

    this.confirmationService.confirm({
      message: cerrado
        ? `<p>${mensaje}.</p> <p>Una vez <b>cerrado</b> no podrá registrar más información.</p>`
        : mensaje,
      accept: () => {
        this.spinner.show();
        const data = this.form.getRawValue();

        const arranque = {
          arranqueId: this.id,
          ...data,
          cerrado: cerrado,
        };

        this.arranqueService.save(arranque).subscribe({
          next: (v) => {
            this.spinner.hide();
            if (v.data === 0) {
              this.messageService.add({
                severity: 'warn',
                summary: 'Mensaje Advertencia',
                detail: v.message,
              });
            } else {
              this.messageService.add({
                severity: 'success',
                summary: 'Mensaje Exitoso',
                detail: v.message,
              });
            }

            //this.cargarData();

            if (cerrado) this.router.navigate(['/envasado']);
          },
          error: (e) => {
            this.spinner.hide();
            this.messageService.add({
              severity: 'error',
              summary: 'Mensaje de Error',
              detail: 'No se pudo completar el proceso.',
            });
          },
          complete: () => { },
        });
      },
    });
  }
}
