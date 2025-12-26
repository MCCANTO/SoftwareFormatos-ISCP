import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-ingreso-sancochado',
  templateUrl: './ingreso-sancochado.component.html',
  styleUrls: ['./ingreso-sancochado.component.scss'],
})
export class IngresoSancochadoComponent implements OnInit {
  form: FormGroup = this.fb.group({
    id: [null],
    numeroBatch: [null, Validators.required],
    numeroTanque: [null, Validators.required],
    temperaturaInicio: [null, Validators.required],
    temperaturaFin: [null],
    observacion: [null],
  });

  tanques: any[] = [
    { id: 1, nombre: '1' },
    { id: 2, nombre: '2' },
    { id: 3, nombre: '3' },
    { id: 4, nombre: '4' },
  ];

  esEdicion: boolean = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.esEdicion = this.config.data.esEdicion;
    this.cargarDataEdicion();
  }

  cargarDataEdicion() {
    if (this.esEdicion) {
      this.form.patchValue(this.config.data.datos);
      this.form.get('numeroBatch')?.disable();
      this.form.get('numeroTanque')?.disable();
      this.form.get('temperaturaInicio')?.disable();
    }
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const data = this.form.getRawValue();
      this.ref.close(data);
    }
  }
}
