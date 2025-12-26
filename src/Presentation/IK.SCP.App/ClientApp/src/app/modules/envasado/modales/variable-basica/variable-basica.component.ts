import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ParametroGeneralService } from 'src/app/services/envasado/parametro-general.service';

@Component({
  selector: 'app-variable-basica',
  templateUrl: './variable-basica.component.html',
  styleUrls: ['./variable-basica.component.scss']
})
export class VariableBasicaComponent implements OnInit {

  tipo_variable: string = '';
  tipos_variable: any[] = [];

  esEditable = false;
  cerrado = false;
  canSave = true;

  variables: any[] = []

  valores = [
    { id: 'C', texto: 'Conforme'},
    { id: 'NC', texto: 'No Conforme'},
    { id: 'NA', texto: 'No Aplica'},
    { id: 'P', texto: 'Pendiente'},
  ]


  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private parametroGeneralService: ParametroGeneralService
  ) { }

  ngOnInit(): void {

    this.parametroGeneralService.getAll(1)
      .subscribe((resp: any) => {
        this.tipos_variable = resp.data;
        this.tipo_variable = this.config.data.tipoId;
      });

    this.variables = this.config.data.variables;
    this.esEditable = this.config.data.esEditable;
    this.cerrado = this.config.data.cerrado;
  }

  cerrar() {
    this.ref.close();
  }

  agregar() {

    let cerrado = true;

    this.variables.forEach( v => {
      if( v.valor === 'P' ) cerrado = false;
    });

    this.ref.close(
      {
        cerrado,
        fechaCreacion: new Date(),
        detalles: this.variables,
      }
    );
  }

}
