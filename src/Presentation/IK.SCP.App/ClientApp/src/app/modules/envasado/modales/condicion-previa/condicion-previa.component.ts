import { ParametroGeneralService } from './../../../../services/envasado/parametro-general.service';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
@Component({
  selector: 'app-condicion-previa',
  templateUrl: './condicion-previa.component.html',
  styleUrls: ['./condicion-previa.component.scss'],
})
export class CondicionPreviaComponent implements OnInit {
  esEditable = false;

  tipo_condicion!: any;
  tipo_condiciones: any[] = [];

  condiciones: any[] = [];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private parametroGeneralService: ParametroGeneralService
  ) {}

  ngOnInit(): void {
    if (this.config.data.esEditable) this.esEditable = true;

    this.parametroGeneralService.getAll(1).subscribe((resp: any) => {
      this.tipo_condiciones = resp.data;
      this.tipo_condicion = this.config.data.tipoId;
    });

    this.condiciones = this.config.data.condiciones;
    this.esEditable = this.config.data.esEditable;
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    const tipoDesc = this.tipo_condiciones.filter(
      (f) => f.parametroGeneralId === this.tipo_condicion
    )[0].nombre;

    this.ref.close({
      tipoId: this.tipo_condicion,
      tipoDesc,
      fechaCreacion: new Date(),
      detalles: this.condiciones,
    });
  }
}
