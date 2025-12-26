import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-titulo-tarjeta-modulo',
  templateUrl: './titulo-tarjeta-modulo.component.html',
  styleUrls: ['./titulo-tarjeta-modulo.component.scss'],
})
export class TituloTarjetaModuloComponent {
  @Input() titulo: string = '';
  @Input() icono: string = '';
  @Input() clase: string = '';
}
