import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-registro-control-aceite',
  templateUrl: './registro-control-aceite.component.html',
  styleUrls: ['./registro-control-aceite.component.scss'],
})
export class RegistroControlAceiteComponent implements OnInit {
  form: FormGroup = this.fb.group({
    lineaId: [null, [Validators.required]],
    ordenId: [null],
    producto: [{ value: null, disabled: true }],
    saborId: [null],
    otroSabor: [null],
    etapa: [null, [Validators.required]],
    aceite: [null, [Validators.required]],
    inicioFuente: [null],
    rellenoFuente: [null],
    observacion: [null],
    agl: [null],
    cp: [null],
    color: [null],
    olor: [null],
  });

  stateOptions: any[] = [
    { label: 'Conforme', value: 'C' },
    { label: 'No Conforme', value: 'NC' },
  ];

  freidoras: any[] = [];

  mostrarOrden = false;

  constructor(
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.cargarDatos();
  }

  cargarDatos() {
    const datos = this.config.data;
    this.freidoras = datos.freidoras;
    this.form.patchValue(datos);
    this.form.get('lineaId')?.disable();
    if (!datos.ordenId) {
      this.mostrarOrden = false;
      this.form.get('producto')?.enable();
    }
    else {
      this.mostrarOrden = true;
      this.form.get('ordenId')?.disable();
    }
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    if (!this.form.valid) return;
    this.ref.close(this.form.getRawValue());
  }
}
