import { Component, Inject, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-carga-codificacion',
  templateUrl: './carga-codificacion.component.html',
  styleUrls: ['./carga-codificacion.component.scss']
})
export class CargaCodificacionComponent implements OnInit {

  uploadedFiles: any[] = [];

  urlCarga = '' 

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this.urlCarga = baseUrl + `${environment.api_base}/envasado-granel/codificacion/carga` ;
  }

  ngOnInit(): void {
  }

  onUpload(event: any) {

    const { nombre, ruta } = event.originalEvent?.body?.data;
   
    const file = event.files[0];

    this.ref.close({
      nombre,
      ruta,
      tamanio: file.size,
      tipoArchivo: file.type
    })

  }

  cerrar() {
    this.ref.close();
  }

}


