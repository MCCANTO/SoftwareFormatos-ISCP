import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-modal-control-tiempo',
  templateUrl: './modal-control-tiempo.component.html',
  styleUrls: ['./modal-control-tiempo.component.scss'],
})
export class ModalControlTiempoComponent {
  form: FormGroup = this.fb.group({
    numeroPaleta: [null, [Validators.required]],
    cantidadKg: [null, [Validators.required]],
    horaInicioPef: [null, [Validators.required]],
    horaInicioFritura: [null, [Validators.required]],
    observacion: [null],
  });

  constructor(
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private fb: FormBuilder
  ) {}

  cerrar() {
    this.ref.close();
  }

  guardar() {
    if (this.form.valid) {
      this.ref.close(this.form.value);
    }
  }
}
