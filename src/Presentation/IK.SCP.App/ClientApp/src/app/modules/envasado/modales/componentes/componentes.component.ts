import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-componentes',
  templateUrl: './componentes.component.html',
  styleUrls: ['./componentes.component.scss']
})
export class ComponentesComponent implements OnInit {

  form = this.fb.group({
    componente: [ '', [ Validators.required ] ],
    lote: ['', [ Validators.required ]],
    humedad: [null, [ Validators.required ]],
    evaluacionSensorial: [null, [ Validators.required ]],
    observacion: [''],
  });

  valores = [
    { texto: 'C', valor: true },
    { texto: 'NC', valor: false },
  ];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
  ) { }


  ngOnInit(): void {
  }

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
