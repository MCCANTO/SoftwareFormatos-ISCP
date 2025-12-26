import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ParametroGeneralService } from 'src/app/services/envasado/parametro-general.service';

@Component({
  selector: 'app-variable-basica-arranque',
  templateUrl: './variable-basica-arranque.component.html',
  styleUrls: ['./variable-basica-arranque.component.scss']
})
export class VariableBasicaArranqueComponent implements OnInit {

  nro_grupo = 0;
  tipo_variable: string = '';
  tipos_variable: any[] = [];

  isEdit = false;
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
        if(this.tipo_variable)  this.isEdit = true;
        else this.isEdit = false;
      });

    this.variables = this.config.data.variables;

    this.validarEdicionVariables();

  }

  validarEdicionVariables() {
    
    this.canSave = false;
    this.variables.forEach( p => {

      if( !p.Cerrado ) {
        this.canSave = true;
      }

    })

    

  }

  cerrar() {
    this.ref.close();
  }

  agregar() {
    this.ref.close({
      tipoId: this.tipo_variable,
      variables: this.variables 
    });
  }

}
