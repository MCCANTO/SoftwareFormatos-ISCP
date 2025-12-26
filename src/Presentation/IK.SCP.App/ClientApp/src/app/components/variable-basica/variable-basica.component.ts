import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ParametroGeneralService } from 'src/app/services/envasado/parametro-general.service';

@Component({
  selector: 'app-variable-basica',
  templateUrl: './variable-basica.component.html',
  styleUrls: ['./variable-basica.component.scss'],
})
export class VariableBasicaComponent implements OnInit {
  variables: any[] = [];

  valores = [
    { id: 'C', texto: 'Conforme' },
    { id: 'NC', texto: 'No Conforme' },
    { id: 'NA', texto: 'No Aplica' },
    { id: 'P', texto: 'Pendiente' },
  ];

  tipos_variable: any[] = [];

  tipoId!: number;
  maquinista: string = '';

  mostrarTipo = false;
  mostrarMaquinista = false;
  editable: boolean = false;
  cerrado: boolean = false;

  constructor(
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private parametroGeneralService: ParametroGeneralService
  ) { }

  ngOnInit(): void {
    this.variables = this.config.data.variables;

    this.mostrarTipo = this.config.data.mostrarTipo;
    this.mostrarMaquinista = this.config.data.mostrarMaquinista ?? false;
    if (this.mostrarTipo) {
      this.parametroGeneralService.getAll(1).subscribe((resp: any) => {
        this.tipos_variable = resp.data;
        this.tipoId = this.config.data.tipoId;
      });
    }
    if (this.mostrarMaquinista) {
      this.maquinista = this.config.data.maquinista;
    }

    this.editable = this.config.data.editable;
    this.cerrado = this.config.data.cerrado;
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {

    if (this.mostrarTipo && !this.tipoId) return;

    if (this.mostrarMaquinista && !this.mostrarMaquinista) return;

    let cantidadVariablesEditadas = 0;
    this.variables.forEach(p => {
      if (p.items.some((x: any) => x.valor != 'P'))
        cantidadVariablesEditadas += 1;
    })
    if (cantidadVariablesEditadas === 0) return;

    let data: any = { variables: this.variables };

    if (this.mostrarTipo) {
      data = { ...data, tipoId: this.tipoId };
    }

    if (this.mostrarMaquinista) {
      data = { ...data, maquinista: this.maquinista };
    }

    this.ref.close(data);
  }
}
