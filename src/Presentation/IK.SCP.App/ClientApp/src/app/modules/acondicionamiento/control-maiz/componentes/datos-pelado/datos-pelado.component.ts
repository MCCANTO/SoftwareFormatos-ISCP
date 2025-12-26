import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { IngresoPeladoComponent } from '../../modales/ingreso-pelado/ingreso-pelado.component';

@Component({
  selector: 'app-datos-pelado',
  templateUrl: './datos-pelado.component.html',
  styleUrls: ['./datos-pelado.component.scss'],
})
export class DatosPeladoComponent {
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
    const mp_ref = this.dialogService.open(IngresoPeladoComponent, {
      header: esEdicion ? 'FIN DE PELADO' : 'INICIO DE PELADO',
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
