import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-ingreso-insumo',
  templateUrl: './ingreso-insumo.component.html',
  styleUrls: ['./ingreso-insumo.component.scss'],
})
export class IngresoInsumoComponent implements OnInit {
  form = this.fb.group({
    peladoMaizInsumoId: [0],
    insumo: [{ value: 'CAL', disabled: true }, Validators.required],
    lote: [null, Validators.required],
    inicio: [null, Validators.required],
    fin: [0],
    consumo: [
      { value: 0, disabled: true },
      [Validators.required, Validators.min(0)],
    ],
  });

  constructor(
    private fb: FormBuilder,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.form
      .get('inicio')
      ?.valueChanges.subscribe((x) => this.calcularConsumo());
    this.form.get('fin')?.valueChanges.subscribe((x) => this.calcularConsumo());

    const data = this.config.data;

    if (data) {
      const datos = {
        peladoMaizInsumoId: data.peladoMaizInsumoId,
        insumo: data.insumo,
        lote: data.lote,
        inicio: data.cantidadInicio,
        fin: data.cantidadFin,
      };
      this.form.patchValue(datos);
      this.form.get('insumo')?.disable();
      this.form.get('lote')?.disable();
      this.form.get('inicio')?.disable();
    } else {
      this.form.get('insumo')?.enable();
      this.form.get('lote')?.enable();
      this.form.get('inicio')?.enable();
    }
  }

  calcularConsumo() {
    const inicio: number = this.form.get('inicio')?.value ?? 0;
    const fin: number = this.form.get('fin')?.value ?? 0;
    let consumo = 0;
    if (inicio > 0) {
      consumo = inicio - fin;
    }
    this.form.patchValue({
      consumo,
    });
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.ref.close(this.form.getRawValue());
    }
  }
}
