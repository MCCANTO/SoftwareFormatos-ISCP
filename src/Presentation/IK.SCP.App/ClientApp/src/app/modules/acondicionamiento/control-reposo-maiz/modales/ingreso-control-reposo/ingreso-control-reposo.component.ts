import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ControlMaizService } from 'src/app/services/acondicionamiento/control-maiz.service';

@Component({
  selector: 'app-ingreso-control-reposo',
  templateUrl: './ingreso-control-reposo.component.html',
  styleUrls: ['./ingreso-control-reposo.component.scss'],
  providers: [DatePipe],
})
export class IngresoControlReposoComponent implements OnInit {
  form: FormGroup = this.fb.group({
    id: [null],
    numeroBatch: [null, Validators.required],
    cantidadBatch: [null, Validators.required],
    fechaHoraInicioReposo: [
      { value: null, disabled: true },
      Validators.required,
    ],
    fechaHoraInicioFritura: [null, Validators.required],
    observacion: [''],
  });

  ordenId = '';
  esMaiz = false;

  constructor(
    private fb: FormBuilder,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private service: ControlMaizService,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.ordenId = this.config.data.ordenId;
    this.esMaiz = this.config.data.esMaiz ?? false;
  }

  obtenerDatosSancochado() {
    if (this.esMaiz && this.form.get('numeroBatch')?.value != null) {
      const numeroBatch = this.form.get('numeroBatch')?.value;

      this.service
        .getSancochadoReposo(this.ordenId, numeroBatch)
        .subscribe((resp) => {
          if (resp.success) {
            this.form
              .get('fechaHoraInicioReposo')
              ?.setValue(
                resp.data[0].fechaHoraFin
                  ? new Date(resp.data[0].fechaHoraFin)
                  : null
              );
          }
        });
    }
  }

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
