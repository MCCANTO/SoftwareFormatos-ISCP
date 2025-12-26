import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-nuevo-control-granel',
  templateUrl: './nuevo-control-granel.component.html',
  styleUrls: ['./nuevo-control-granel.component.scss']
})
export class NuevoControlGranelComponent implements OnInit {

  parametros: any[] = [];

  valores = [
    { id: 'C', texto: 'Conforme'},
    { id: 'NC', texto: 'No Conforme'},
    { id: 'N/A', texto: 'No Aplica'},
  ]

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
    this.parametros = this.config.data.parametros;
  }

  cerrar(){
    this.ref.close();
  }

  guardar() {
    this.ref.close(this.parametros);
  }
}
