import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-inspeccion',
  templateUrl: './inspeccion.component.html',
  styleUrls: ['./inspeccion.component.scss']
})
export class InspeccionComponent implements OnInit {

  urlCarga = '' ; 

  form: FormGroup = this.fb.group({
    cantidadCaja: [null],
    etiquetador: [''],
    posicion: [''],
    inspector: [''],
    imagen: [''],
  })

  posiciones = [
    {id: 'ARRIBA', nombre: 'ARRIBA'},
    {id: 'DERECHA', nombre: 'DERECHA'},
    {id: 'IZQUIERA', nombre: 'IZQUIERA'},
    {id: 'ABAJO', nombre: 'ABAJO'},
  ]

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
    @Inject('BASE_URL') baseUrl: string,
  ) { 
    this.urlCarga = baseUrl + `${environment.api_base}/envasado/arranque/carga-inspeccion`;
  }

  ngOnInit(): void {
  }

  cerrar() {
    this.ref.close();
  }

  onUpload(event: any) {

    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }

    const { nombre, ruta } = event.originalEvent?.body?.data;
   
    const file = event.files[0];

    this.ref.close({
      ...this.form.value,
      nombre,
      ruta,
      tamanio: file.size,
      tipoArchivo: file.type
    })

  }

}
