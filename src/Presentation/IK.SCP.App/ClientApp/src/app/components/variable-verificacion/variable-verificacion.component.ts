import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-variable-verificacion',
  templateUrl: './variable-verificacion.component.html',
  styleUrls: ['./variable-verificacion.component.scss']
})
export class VariableVerificacionComponent implements OnInit {

  variables: any[] = [];
  valores: any[] = [];
  editable: boolean = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
    this.variables = this.config.data.variables;
    this.valores = this.config.data.valores;
    this.editable = this.config.data.editable;
  }

  cerrar() {
    this.ref.close({ ok: false});
  }

  guardar() {
    this.ref.close({ ok: true, variables: this.variables });
  }
}
