import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EnvasadoCondicionPrevia, EnvasadoCondicionPreviaDetalle } from 'src/app/core/models/envasado/condicion-previa.model';
import { ParametroGeneralService } from 'src/app/services/envasado/parametro-general.service';

@Component({
  selector: 'app-condicion',
  templateUrl: './condicion.component.html',
  styleUrls: ['./condicion.component.scss']
})
export class CondicionComponent implements OnInit {

  dataCondicion: EnvasadoCondicionPrevia = {
    editable: false,
    mostrarTipo: false,
    tipos: [],
    condiciones: [],
    tipo: null
  };

  constructor(
    private parametroGeneralService: ParametroGeneralService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
  ) { }

  ngOnInit(): void {
    this.parametroGeneralService.getAll(1)
      .subscribe((resp: any) => {
        this.dataCondicion = this.config.data.condicion;
        this.dataCondicion.tipos = resp.data;
      });
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    if(this.dataCondicion.tipos?.length > 0) {
      if(!this.dataCondicion.tipo) return;
    }
    this.ref.close(this.dataCondicion);
  }

}
