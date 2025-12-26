import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-tarjeta-seccion',
  templateUrl: './tarjeta-seccion.component.html',
  styleUrls: ['./tarjeta-seccion.component.scss']
})
export class TarjetaSeccionComponent implements OnInit {

  @Input() icono: string = "";
  @Input() titulo: string = "";

  @Input() headerTemplate!: TemplateRef<any>;
  @Input() contentTemplate!: TemplateRef<any>;

  constructor() { }

  ngOnInit(): void {
  }

}
