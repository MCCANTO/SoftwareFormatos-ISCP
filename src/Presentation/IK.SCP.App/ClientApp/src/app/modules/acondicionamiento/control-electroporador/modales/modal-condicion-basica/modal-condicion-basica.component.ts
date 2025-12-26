import { AcondicionamientoService } from 'src/app/services/acondicionamiento/acondicionamiento.service';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ParametroGeneralService } from 'src/app/services/envasado/parametro-general.service';

@Component({
  selector: 'app-modal-condicion-basica',
  templateUrl: './modal-condicion-basica.component.html',
  styleUrls: ['./modal-condicion-basica.component.scss'],
})
export class ModalCondicionBasicaComponent implements OnInit {
  esEditable: boolean = true;
  tipos: any[] = [];
  tipoId!: number;
  condiciones: any[] = [];

  constructor(
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private parametroGeneralService: ParametroGeneralService
  ) {}

  ngOnInit(): void {
    this.parametroGeneralService.getAll(1).subscribe((resp: any) => {
      this.tipos = resp.data;
      this.tipoId = this.config.data?.tipoId;
      this.esEditable = this.config.data?.esEditable;
      this.condiciones = this.config.data?.condiciones;
    });
  }

  cerrar() {
    this.ref.close();
  }

  get deshabilitarBotonGuardar() {
    return this.condiciones.some((x) => !x.valor) || !this.tipoId;
  }

  guardar() {
    this.ref.close({
      tipoId: this.tipoId,
      condiciones: this.condiciones,
    });
  }
}
