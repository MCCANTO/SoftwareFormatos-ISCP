import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-ingreso-control-remojo',
  templateUrl: './ingreso-control-remojo.component.html',
  styleUrls: ['./ingreso-control-remojo.component.scss'],
  providers: [DatePipe],
})
export class IngresoControlRemojoComponent {
  form: FormGroup = this.fb.group({
    numeroBatch: [null, Validators.required],
    cantidadBatch: [null, Validators.required],
    fechaHoraInicioReposo: [null, Validators.required],
    fechaHoraInicioFritura: [null, Validators.required],
    observacion: [''],
  });

  constructor(
    private fb: FormBuilder,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {}

  cerrar() {
    this.ref.close();
  }

  guardar() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const data = this.form.getRawValue();
      if (typeof data.fechaHoraInicioReposo === 'object') {
        data.fechaHoraInicioReposo = this.datePipe.transform(
          this.form.get('fechaHoraInicioReposo')?.value,
          'dd/MM/yyyy HH:mm'
        );
      }
      this.ref.close(data);
    }
  }
}
