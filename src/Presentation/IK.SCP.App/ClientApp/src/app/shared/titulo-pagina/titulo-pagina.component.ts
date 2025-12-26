import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-titulo-pagina',
  templateUrl: './titulo-pagina.component.html',
  styleUrls: ['./titulo-pagina.component.scss']
})
export class TituloPaginaComponent implements OnInit {

  @Input() titulo: string = "";

  @Input() mostrarBotonGuardar = false;
  @Input() mostrarBotonGuardarCerrar = false;
  @Input() mostrarBotonCerrar = false;
  @Input() mostrarBotonRegresarAnt = true;
  @Input() mostrarBotonRegresar = false;

  @Output() guardar = new EventEmitter();
  @Output() guardarCerrar = new EventEmitter();
  @Output() cerrar = new EventEmitter();
  @Output() regresar = new EventEmitter();
  @Output() regresarAnt = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onGuardar() {
    this.guardar.emit();
  }

  onGuardarCerrar() {
    this.guardarCerrar.emit();
  }

  onCerrar() {
    this.cerrar.emit();
  }

  onRegresar() {
    this.regresar.emit();
  }
  onRegresarAnt() {
    this.regresarAnt.emit();
  }
 
}
