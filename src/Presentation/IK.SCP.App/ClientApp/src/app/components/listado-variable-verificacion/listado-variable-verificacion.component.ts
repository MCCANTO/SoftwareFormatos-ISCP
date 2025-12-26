import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-listado-variable-verificacion',
  templateUrl: './listado-variable-verificacion.component.html',
  styleUrls: ['./listado-variable-verificacion.component.scss']
})
export class ListadoVariableVerificacionComponent implements OnInit {

  @Input() verificaciones : any[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
