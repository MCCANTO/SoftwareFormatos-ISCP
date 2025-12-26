import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { IngresoSancochadoComponent } from '../../modales/ingreso-sancochado/ingreso-sancochado.component';

@Component({
  selector: 'app-datos-sancochado',
  templateUrl: './datos-sancochado.component.html',
  styleUrls: ['./datos-sancochado.component.scss'],
})
export class DatosSancochadoComponent {
  @Input() datos: any;
  @Output() guardar = new EventEmitter<any>();

  constructor(private dialogService: DialogService) { }

  agregar() {
    this.abrirModal(false);
  }

  editar(control: any) {
    this.abrirModal(true, control);
  }

  abrirModal(esEdicion: boolean, control: any = null) {
    const mp_ref = this.dialogService.open(IngresoSancochadoComponent, {
      header: esEdicion ? 'FIN DE SANCOCHADO' : 'INICIO DE SANCOCHADO',
      width: '50%',
      data: {
        esEdicion,
        datos: esEdicion ? control : null,
      },
    });

    mp_ref.onClose.subscribe((result) => {
      if (result) {
        this.guardar.emit(result);
      }
    });
  }
}
