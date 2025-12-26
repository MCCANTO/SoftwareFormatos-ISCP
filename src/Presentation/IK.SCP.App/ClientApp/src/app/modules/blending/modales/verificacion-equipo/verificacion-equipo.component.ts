import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-verificacion-equipo',
  templateUrl: './verificacion-equipo.component.html',
  styleUrls: ['./verificacion-equipo.component.scss']
})
export class VerificacionEquipoComponent implements OnInit {

  calificaciones: any[] = [
    { texto: 'SÍ', valor: 'SI' },
    { texto: 'NO', valor: 'NO' },
    { texto: 'N/A', valor: 'NA' },
  ];

  verificaciones: any[] = [
    { nombre: 'Tablero principal' },
    { nombre: 'Monitor y teclado' },
    { nombre: 'Tolva Superior N°1' },
    { nombre: 'Tolva Superior N°2' },
    { nombre: 'Tolva Superior N°3' },
    { nombre: 'Tolva Superior N°4' },
    { nombre: 'Fast Back N°1' },
    { nombre: 'Fast Back N°2' },
    { nombre: 'Fast Back N°3' },
    { nombre: 'Fast Back N°4' },
    { nombre: 'Balanza N°1' },
    { nombre: 'Balanza N°2' },
    { nombre: 'Balanza N°3' },
    { nombre: 'Balanza N°4' },
    { nombre: 'Fast back acumulador' },
    { nombre: 'Mezclador (tambor rotativo, tolva, brazo)' },
    { nombre: 'Elevador Blending' },
    { nombre: 'Utensilios (tijera, fundas)' },
  ]

  constructor() { }

  ngOnInit(): void {
  }

  cerrar() {

  }

  guardar() {

  }

}
