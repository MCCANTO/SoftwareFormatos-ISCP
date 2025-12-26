import { Component, OnInit } from '@angular/core';
import { EnvasadoCondicionPrevia } from '../../../../core/models/envasado/condicion-previa.model';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-condiciones-previas-envasado',
  templateUrl: './condiciones-previas-envasado.component.html',
  styleUrls: ['./condiciones-previas-envasado.component.scss']
})
export class CondicionesPreviasEnvasadoComponent implements OnInit {

  condiciones!: EnvasadoCondicionPrevia;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
    this.condiciones = this.config.data;
  }

  cerrar() {
    const result = { ok: false };
    this.ref.close(result);
  }

  guardar() {
    const result = {
      ok: true,
      condiciones: this.condiciones
    }
    this.ref.close(result);
  }

}
