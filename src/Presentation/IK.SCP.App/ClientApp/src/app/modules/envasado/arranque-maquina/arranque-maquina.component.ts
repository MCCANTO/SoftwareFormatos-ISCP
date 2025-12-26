import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';

import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ArranqueMaquinaService } from 'src/app/services/envasado/arranque-maquina.service';

import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, MessageService } from 'primeng/api';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import { EnvasadoCondicionPrevia } from '../../../core/models/envasado/condicion-previa.model';

import { VariableBasicaComponent } from 'src/app/components/variable-basica/variable-basica.component';
import { ACCION_ENV_CHECKLIST_ARRANQUE_MAQUINA, NODO } from 'src/app/core/constants/accion.constant';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { SecurityService } from 'src/app/services/security.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { CondicionComponent } from '../../../components/condicion/condicion.component';
@Component({
  selector: 'app-arranque-maquina',
  templateUrl: './arranque-maquina.component.html',
  styleUrls: ['./arranque-maquina.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService],
})
export class ArranqueMaquinaComponent implements OnInit {

  NODO = NODO.ENV_CHECKLIST_ARRANQUE_MAQUINA;
  PERMISOS: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false
  };

  id: number = 0;

  esCerrado: boolean = false;

  creador: string = '';

  condiciones: any[] = [];

  variables: any[] = [];

  observaciones: any[] = [];
  observacion = '';

  form!: FormGroup;

  dataEnvasadora!: any;

  dataOrden!: OrdenEnvasado;

  constructor(
    public dialogService: DialogService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private router: Router,
    private fb: FormBuilder,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,
    private storageAppService: StorageAppService,
    public securityService: SecurityService,
  ) {
    this.securityService.validarAcciones([
      ACCION_ENV_CHECKLIST_ARRANQUE_MAQUINA,
    ]).then(resp => {
      this.PERMISOS = resp[0];
    })

    this.createForm();
  }

  get EsCreador(): boolean {
    return this.securityService.usuarioActivo() === this.creador;
  }

  get PesoSobre1() {
    return this.form.get('pesoSobreProducto1');
  }

  get PesoSobre2() {
    return this.form.get('pesoSobreProducto2');
  }

  get PesoSobre3() {
    return this.form.get('pesoSobreProducto3');
  }

  get PesoSobre4() {
    return this.form.get('pesoSobreProducto4');
  }

  get PesoSobre5() {
    return this.form.get('pesoSobreProducto5');
  }

  get Promedio() {
    return this.form.get('pesoSobreProductoProm');
  }

  calcularPromedio() {
    let contador = 0;
    let total = 0;

    if (this.PesoSobre1?.value > 0) {
      total += this.PesoSobre1?.value;
      contador++;
    }

    if (this.PesoSobre2?.value > 0) {
      total += this.PesoSobre2?.value;
      contador++;
    }

    if (this.PesoSobre3?.value > 0) {
      total += this.PesoSobre3?.value;
      contador++;
    }

    if (this.PesoSobre4?.value > 0) {
      total += this.PesoSobre4?.value;
      contador++;
    }

    if (this.PesoSobre5?.value > 0) {
      total += this.PesoSobre5?.value;
      contador++;
    }

    const promedio = contador > 0 ? total / contador : 0;

    this.Promedio?.setValue(promedio);
  }

  createForm() {
    this.form = this.fb.group({
      pesoSobreProducto1: [null],
      pesoSobreProducto2: [null],
      pesoSobreProducto3: [null],
      pesoSobreProducto4: [null],
      pesoSobreProducto5: [null],
      pesoSobreProductoProm: [{ value: null, disabled: true }],
      pesoSobreVacio: [''],
    });

    this.PesoSobre1?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre2?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre3?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre4?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre5?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
  }

  ngOnInit(): void {
    this.cargarDataEnvasado();
    this.cargarData();
  }

  cargarDataEnvasado() {
    const data = this.storageAppService.DataEnvasado;

    this.dataEnvasadora = data.envasadora;
    this.dataOrden = data.orden;
  }

  cargarData() {
    const linea = this.dataEnvasadora.Id;
    const orden = this.dataOrden.Orden;

    this.spinner.show();
    this.arranqueMaquinaService.getOpen(linea, orden).subscribe((resp: any) => {

      if (resp.data) {
        const arranque = resp.data;

        this.id = arranque.arranqueMaquinaId;
        this.creador = arranque.usuarioCreacion;
        this.esCerrado = arranque.cerrado;

        this.form.patchValue({
          pesoSobreProducto1: arranque.pesoSobreProducto1,
          pesoSobreProducto2: arranque.pesoSobreProducto2,
          pesoSobreProducto3: arranque.pesoSobreProducto3,
          pesoSobreProducto4: arranque.pesoSobreProducto4,
          pesoSobreProducto5: arranque.pesoSobreProducto5,
          pesoSobreProductoProm: arranque.pesoSobreProductoProm,
          pesoSobreVacio: arranque.pesoSobreVacio,
        });

        if (!this.PERMISOS.ESCRITURA) {
          this.form.get('pesoSobreProducto1')?.disable();
          this.form.get('pesoSobreProducto2')?.disable();
          this.form.get('pesoSobreProducto3')?.disable();
          this.form.get('pesoSobreProducto4')?.disable();
          this.form.get('pesoSobreProducto5')?.disable();
          this.form.get('pesoSobreVacio')?.disable();
        } else {
          this.form.get('pesoSobreProducto1')?.enable();
          this.form.get('pesoSobreProducto2')?.enable();
          this.form.get('pesoSobreProducto3')?.enable();
          this.form.get('pesoSobreProducto4')?.enable();
          this.form.get('pesoSobreProducto5')?.enable();
          this.form.get('pesoSobreVacio')?.enable();
        }

        this.observaciones = arranque.observaciones;

        this.condiciones = arranque.condiciones;

        this.variables = arranque.variables;
      } else {
        this.router.navigate(['/envasado']);
      }
      this.spinner.hide();
    });
  }

  goTo(index: number) {
    if (index === 0) {
      this.router.navigate(['/envasado']);
    }

    if (index === 1) {
    }

    if (index === 2) {
    }
  }

  agregarCondicionPrevia() {
    this.cargarCondicionesPrevias(0);
  }

  cargarCondicionesPrevias(id: number) {
    this.arranqueMaquinaService
      .getAllCondicionesById(id)
      .subscribe((resp: any) => {
        const condicion: EnvasadoCondicionPrevia = {
          editable: id == 0,
          mostrarTipo: true,
          tipo: resp.data.tipoId,
          condiciones: resp.data.items,
          tipos: [],
        };

        const ref = this.dialogService.open(CondicionComponent, {
          header: 'Condiciones Previas',
          width: '85%',
          data: {
            condicion,
          },
        });

        ref.onClose.subscribe((result: any) => {
          if (result) {
            const condiciones = {
              arranqueMaquinaId: this.id,
              tipoId: result.tipo,
              condiciones: result.condiciones,
            };

            this.guardarCondiciones(condiciones);
          }
        });
      });
  }

  guardarCondiciones(condiciones: {
    arranqueMaquinaId: number;
    tipoId: any;
    condiciones: any;
  }) {
    this.arranqueMaquinaService
      .saveCondicionesPrevias(condiciones)
      .subscribe((resp) => {
        if (resp.success) {
          this.cargarData();
        }
      });
  }

  agregarVariablesBasicas() {
    const abiertos: any[] = this.variables.filter((f) => f.cerrado == false);

    if (this.variables.length > 0 && abiertos.length > 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Mensaje Advertencia',
        detail:
          'Existe un registro pendiente. Debe completarlo para poder agregar uno nuevo.',
      });
      return;
    }

    this.cargarVariablesBasicas(0);
  }

  cargarVariablesBasicas(id: number, cerrado: boolean = false) {
    this.arranqueMaquinaService
      .getAllVariablesById(id)
      .subscribe((resp: any) => {

        const variables = resp.data;

        const vb_ref = this.dialogService.open(VariableBasicaComponent, {
          header: 'Variables Básicas',
          width: '95%',
          data: {
            mostrarTipo: false,
            editable: this.PERMISOS.ESCRITURA,
            cerrado: cerrado,
            variables,
          },
        });

        vb_ref.onClose.subscribe((result: any) => {
          if (result) {
            const { variables } = result;

            let variablesBasicas: any[] = [];

            variables.forEach((item: any) => {
              variablesBasicas.push(...item.items);
            });

            const dataVariables = {
              arranqueMaquinaVarBasCabId: id,
              arranqueMaquinaId: this.id,
              variables: variablesBasicas,
            };

            this.guardarVariables(dataVariables);
          }
        });
      });
  }

  guardarVariables(variables: { arranqueMaquinaId: number; arranqueMaquinaVarBasCabId: any; variables: any; }) {
    this.arranqueMaquinaService
      .saveVariablesBasicas(variables)
      .subscribe((resp) => {
        if (resp.success) {
          this.cargarData();
        }
      });
  }

  agregarObservacion() {
    if (!this.observacion) return;
    const data = {
      arranqueMaquinaId: this.id,
      observacion: this.observacion,
    };
    this.arranqueMaquinaService.saveObservacion(data)
      .subscribe(resp => {
        this.cargarData();
        this.observacion = '';
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
          arranqueMaquinaId: this.id,
          ...data,
          cerrado: cerrado,
        };

        this.arranqueMaquinaService.save(arranque).subscribe({
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

            this.cargarData();

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
