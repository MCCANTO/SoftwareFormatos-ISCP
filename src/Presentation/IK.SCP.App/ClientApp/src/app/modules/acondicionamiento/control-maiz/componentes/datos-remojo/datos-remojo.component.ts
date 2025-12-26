import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { IngresoRemojoComponent } from '../../modales/ingreso-remojo/ingreso-remojo.component';

@Component({
  selector: 'app-datos-remojo',
  templateUrl: './datos-remojo.component.html',
  styleUrls: ['./datos-remojo.component.scss'],
})
export class DatosRemojoComponent {
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
    const mp_ref = this.dialogService.open(IngresoRemojoComponent, {
      header: esEdicion ? 'FIN DE REMOJO' : 'INICIO DE REMOJO',
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
