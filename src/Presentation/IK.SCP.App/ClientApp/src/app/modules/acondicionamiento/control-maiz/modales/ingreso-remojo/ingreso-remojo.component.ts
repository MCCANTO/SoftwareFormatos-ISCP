import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

interface IngresoRemojo {
  id?: number;
  numeroTanque: number;
  olor: string;
  phAntes: number;
  phDespues?: number;
  inicioAgitacion?: Date;
  finAgitacion?: Date;
  observacion?: string;
}

@Component({
  selector: 'app-ingreso-remojo',
  templateUrl: './ingreso-remojo.component.html',
  styleUrls: ['./ingreso-remojo.component.scss'],
})
export class IngresoRemojoComponent implements OnInit {
  form: FormGroup = this.fb.group({
    id: [null],
    numeroTanque: [null, Validators.required],
    olor: [null],
    phAntes: [null, Validators.required],
    phDespues: [null],
    inicioAgitacion: [null],
    finAgitacion: [null],
    observacion: [null],
  });

  tanques: any[] = [
    { id: 1, nombre: '1' },
    { id: 2, nombre: '2' },
    { id: 3, nombre: '3' },
    { id: 4, nombre: '4' },
  ];

  olores: any[] = [
    { id: 'C', nombre: 'Conforme' },
    { id: 'NC', nombre: 'No Conforme' },
  ];

  esEdicion: boolean = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.esEdicion = this.config.data.esEdicion;
    this.cargarDataEdicion();
    this.form.get('observacion')?.setValue('');
  }

  habilitarEdicion() {
    if (this.esEdicion) {
      this.form.get('numeroTanque')?.disable();
      this.form.get('phAntes')?.disable();

      if (this.InicioAprobacion) this.form.get('inicioAgitacion')?.disable();

      if (this.FinAprobacion) this.form.get('finAgitacion')?.disable();
    } else {
      this.form.get('numeroTanque')?.enable();
      this.form.get('phAntes')?.enable();
    }
  }

  get InicioAprobacion(): Date {
    return this.form.get('inicioAgitacion')?.value;
  }

  get FinAprobacion(): Date {
    return this.form.get('finAgitacion')?.value;
  }

  cargarDataEdicion() {
    const datos: IngresoRemojo = this.config.data.datos;

    if (this.esEdicion) {
      this.form.patchValue(datos);

      this.form
        .get('inicioAgitacion')
        ?.setValue(
          datos.inicioAgitacion ? new Date(datos.inicioAgitacion) : null
        );

      this.form
        .get('finAgitacion')
        ?.setValue(datos.finAgitacion ? new Date(datos.finAgitacion) : null);
    }

    this.habilitarEdicion();
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const data = this.form.getRawValue();
      if (this.esEdicion) {
        if (typeof data.inicioAgitacion === 'object') {
          data.inicioAgitacion = this.datePipe.transform(
            this.form.get('inicioAgitacion')?.value,
            'dd/MM/yyyy HH:mm'
          );
        }
        if (typeof data.finAgitacion === 'object') {
          data.finAgitacion = this.datePipe.transform(
            this.form.get('finAgitacion')?.value,
            'dd/MM/yyyy HH:mm'
          );
        }
      }
      this.ref.close(data);
    }
  }
}
