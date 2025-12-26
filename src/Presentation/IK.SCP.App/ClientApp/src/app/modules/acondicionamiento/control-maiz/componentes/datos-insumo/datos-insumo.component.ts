import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { IngresoInsumoComponent } from '../../modales/ingreso-insumo/ingreso-insumo.component';

@Component({
  selector: 'app-datos-insumo',
  templateUrl: './datos-insumo.component.html',
  styleUrls: ['./datos-insumo.component.scss'],
})
export class DatosInsumoComponent {
  @Input() insumos: any[] = [];
  @Output() guardar = new EventEmitter<any>();

  constructor(private dialogService: DialogService) {}

  agregar() {
    this.abrirModal();
  }

  editar(item: any) {
    this.abrirModal(item);
  }

  abrirModal(data: any = null) {
    const mp_ref = this.dialogService.open(IngresoInsumoComponent, {
      header: 'INGRESO DE INSUMO',
      width: '35%',
      data,
    });

    mp_ref.onClose.subscribe((result) => {
      if (result) {
        this.guardar.emit(result);
      }
    });
  }
}
