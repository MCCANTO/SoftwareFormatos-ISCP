import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { IngresoMateriaPrimaComponent } from '../../modales/ingreso-materia-prima/ingreso-materia-prima.component';

@Component({
  selector: 'app-datos-materia-prima',
  templateUrl: './datos-materia-prima.component.html',
  styleUrls: ['./datos-materia-prima.component.scss'],
})
export class DatosMateriaPrimaComponent implements OnInit {
  @Input() materias: any[] = [];
  @Output() guardar = new EventEmitter<any>();

  constructor(private dialogService: DialogService) {}

  ngOnInit(): void {}

  calcularPesoTotal() {
    let total = 0;
    this.materias.forEach((mp) => {
      total += mp.cantidad;
    });
    return total;
  }

  agregar() {
    const mp_ref = this.dialogService.open(IngresoMateriaPrimaComponent, {
      header: 'INGRESO DE MATERIA PRIMA',
      width: '40%',
    });

    mp_ref.onClose.subscribe((result) => {
      if (result) {
        this.guardar.emit(result);
      }
    });
  }
}
