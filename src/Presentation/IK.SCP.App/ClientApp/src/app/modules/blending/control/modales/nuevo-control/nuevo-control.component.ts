import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-nuevo-control',
  templateUrl: './nuevo-control.component.html',
  styleUrls: ['./nuevo-control.component.scss']
})
export class NuevoControlComponent implements OnInit {

  componentes: any[] = [];

  observacion = '';

  constructor(
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig,
  ) { 
    this.componentes = this.config.data.componentes;
    
  }

  ngOnInit(): void {
  }

  guardar() {

    const data = {
      observacion: this.observacion,
      componentes: this.componentes
    }

    this.ref.close( data );

  }

  cerrar() {
    this.ref.close();
  }

}
