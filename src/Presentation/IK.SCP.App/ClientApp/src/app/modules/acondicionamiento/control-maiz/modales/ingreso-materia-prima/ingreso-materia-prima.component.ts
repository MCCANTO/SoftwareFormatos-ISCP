import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-ingreso-materia-prima',
  templateUrl: './ingreso-materia-prima.component.html',
  styleUrls: ['./ingreso-materia-prima.component.scss']
})
export class IngresoMateriaPrimaComponent implements OnInit {

  form: FormGroup = this.fb.group({
    calidad: [ null, Validators.required ],
    cantidad: [ null, Validators.required ],
    lote: [ null, Validators.required ],
  });

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
  }

  cerrar(){
    this.ref.close();
  }

  guardar(){

    this.form.markAllAsTouched();

    if(this.form.valid){
      const data = this.form.getRawValue();
      this.ref.close( data );
    }

  }
  
}
