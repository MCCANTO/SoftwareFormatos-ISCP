import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AcondicionamientoService } from 'src/app/services/acondicionamiento/acondicionamiento.service';

@Component({
  selector: 'app-nuevo-control',
  templateUrl: './nuevo-control.component.html',
  styleUrls: ['./nuevo-control.component.scss'],
})
export class NuevoControlComponent implements OnInit {
  materiasPrimas: any[] = [];

  valores: any[] = [
    { icono: 'fa fa-times', valor: false },
    { icono: 'fa fa-check', valor: true },
  ];

  valoresConformidad: any[] = [
    { texto: 'Conforme', valor: 'C' },
    { texto: 'No Conforme', valor: 'NC' },
  ];

  form: FormGroup = this.fb.group({
    materiaPrima: [null, Validators.required],
    deteccionUno: [null, Validators.required],
    deteccionDos: [null, Validators.required],
    conformidad: [null, Validators.required],
    observacion: [null],
  });

  constructor(
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private acondicionamientoService: AcondicionamientoService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.cargarMateriaPrima();
  }

  cargarMateriaPrima() {
    this.acondicionamientoService.getAllMateriaPrima().subscribe((resp) => {
      if (resp.success) {
        this.materiasPrimas = resp.data;
      } else {
      }
    });
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    const data = this.form.getRawValue();
    this.ref.close(data);
  }
}
