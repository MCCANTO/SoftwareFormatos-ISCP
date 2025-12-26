import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-verificacion-equipo',
  templateUrl: './verificacion-equipo.component.html',
  styleUrls: ['./verificacion-equipo.component.scss']
})
export class VerificacionEquipoComponent implements OnInit {

  verificaciones: any[] = [];
  valores = [
    { id: 'C', texto: 'C' },
    { id: 'NC', texto: 'NC' },
    { id: 'NA', texto: 'N/A' },
  ];
  editable: boolean = false;
  cerrado: boolean = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
    this.verificaciones = this.config.data.variables;
    this.editable = this.config.data.esEditable;
    this.cerrado = this.config.data.cerrado;
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    this.ref.close( this.verificaciones );
  }
}
