import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-registro-pedaceria',
  templateUrl: './registro-pedaceria.component.html',
  styleUrls: ['./registro-pedaceria.component.scss'],
})
export class RegistroPedaceriaComponent implements OnInit {
  form: FormGroup = this.fb.group({
    peso: [null, [Validators.required]],
    pedaceria: [null, [Validators.required]],
    porcentajePedaceria: [
      { value: null, disabled: true },
      [Validators.required],
    ],
    hojuelasEnteras: [{ value: null, disabled: true }, [Validators.required]],
    porcentajeHojuelasEnteras: [
      { value: null, disabled: true },
      [Validators.required],
    ],
    observacion: [null],
    inspector: [null, [Validators.required]],
  });

  constructor(
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.form.get('peso')?.valueChanges.subscribe(() => {
      this.calcularPorcentaje();
    });
    this.form.get('pedaceria')?.valueChanges.subscribe(() => {
      this.calcularPorcentaje();
    });
  }

  calcularPorcentaje() {
    const peso = this.form.get('peso')?.value ?? 0;
    const pedaceria = this.form.get('pedaceria')?.value ?? 0;

    if (peso > 0) {
      const porcentajePedaceria = (pedaceria * 100) / peso;
      const hojuelasEnteras = peso - pedaceria;
      const porcentajeHojuelasEnteras = (hojuelasEnteras * 100) / peso;
      this.form.patchValue({
        porcentajePedaceria,
        hojuelasEnteras,
        porcentajeHojuelasEnteras,
      });
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
