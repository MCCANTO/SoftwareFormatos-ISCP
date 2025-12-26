import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { VerificacionEquipoService } from 'src/app/services/fritura/verificacion-equipo.service';

@Component({
  selector: 'app-variable-basica',
  templateUrl: './variable-basica.component.html',
  styleUrls: ['./variable-basica.component.scss']
})
export class VariableBasicaComponent implements OnInit {

  variablesBasicas = [];

  calificaciones: any[] = [
    { texto: 'SÍ', valor: 'SI' },
    { texto: 'NO', valor: 'NO' },
    { texto: 'N/A', valor: 'NA' },
  ];

  esEditable = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
    this.variablesBasicas = this.config.data.variables;
    this.esEditable = this.config.data.esEditable;
  }

  cerrar() {
    this.ref.close();
  }

  agregar() {
    this.ref.close({
      detalle: this.variablesBasicas
    });
  }
}
