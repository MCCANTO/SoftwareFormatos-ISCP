import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-nueva-orden',
  templateUrl: './nueva-orden.component.html',
  styleUrls: ['./nueva-orden.component.scss'],
  providers: [ DatePipe ]
})
export class NuevaOrdenComponent implements OnInit {

  materiasPrimas:any[] = [];

  form = this.fb.group({
    ordenId: [{ value: '', disabled: true }, [  Validators.required ]], 
    fechaEjecucion: [ null, Validators.required ],
    materiaPrimaId: [ 0, Validators.required ] 
  });

  constructor(
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.materiasPrimas = this.config.data.materiasPrima;
  }

  generarCodigoOrden() {
    const form = this.form.getRawValue();

    if ( form.materiaPrimaId && form.fechaEjecucion) { 
      const fechaCodigo: string = this.datePipe.transform( form.fechaEjecucion, 'yyyyMMdd') ?? '';
      const codigo: string = this.materiasPrimas.filter(f => f.Id == form.materiaPrimaId)[0].Codigo;
      this.form.patchValue({ ordenId: `${codigo}-${fechaCodigo}`})
    }
  }

  cancelar(){
    this.ref.close();
  }

  guardar(){
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const data = this.form.getRawValue();
      this.ref.close( data );
    }
  }

}
