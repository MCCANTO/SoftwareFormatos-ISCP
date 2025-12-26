import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-contramuestra',
  templateUrl: './contramuestra.component.html',
  styleUrls: ['./contramuestra.component.scss']
})
export class ContramuestraComponent implements OnInit {

  form: FormGroup = this.fb.group({
    cantidadSobre: [null, [Validators.required]],
    cantidadCaja: [null, [Validators.required]],
  });

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {}

  cerrar() {
    this.ref.close();
  }

  guardar() {

    if( !this.form.valid ) {
      this.form.markAllAsTouched();
      return;
    }

    const data = {
      ...this.form.value
    }

    this.ref.close(data);
  }
}
