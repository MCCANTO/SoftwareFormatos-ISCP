import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-ingreso-pelado',
  templateUrl: './ingreso-pelado.component.html',
  styleUrls: ['./ingreso-pelado.component.scss'],
})
export class IngresoPeladoComponent implements OnInit {
  form: FormGroup = this.fb.group({
    id: [null],
    numeroBatch: [null, Validators.required],
    cal: [null, Validators.required],
    temperaturaInicio: [null, Validators.required],
    temperaturaFin: [null],
    numeroTanque: [null],
    observacion: [null],
  });

  tanques: any[] = [
    { id: 1, nombre: '1' },
    { id: 2, nombre: '2' },
    { id: 3, nombre: '3' },
    { id: 4, nombre: '4' },
  ];

  numero_batch = 1;

  tanque = 1;

  salida = false;

  esEdicion: boolean = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.esEdicion = this.config.data.esEdicion;
    this.cargarFormEdicion();
    this.habilitarControles();
  }

  habilitarControles() {
    if (this.esEdicion) {
      this.form.get('observacion')?.setValue('');
      this.form.get('numeroBatch')?.disable();
      this.form.get('cal')?.disable();
      this.form.get('temperaturaInicio')?.disable();
    } else {
      this.form.get('numeroBatch')?.enable();
      this.form.get('cal')?.enable();
      this.form.get('temperaturaInicio')?.enable();
    }
  }

  cargarFormEdicion() {
    if (this.esEdicion) this.form.patchValue(this.config.data.datos);
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
