import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-verificacion-equipo',
  templateUrl: './verificacion-equipo.component.html',
  styleUrls: ['./verificacion-equipo.component.scss']
})
export class VerificacionEquipoComponent implements OnInit {

  variablesBasicas = [
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "1. Bandeja",
      "nombre": "",
      "comentario": null,
      "valor": "P",
      "primerOrden": 1,
      "segundoOrden": 1,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "2. Fast back con balanza",
      "nombre": "",
      "comentario": null,
      "valor": "P",
      "primerOrden": 2,
      "segundoOrden": 1,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "3. Sistema de Elevador Piab",
      "nombre": "Tamizador",
      "comentario": null,
      "valor": "P",
      "primerOrden": 3,
      "segundoOrden": 1,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "3. Sistema de Elevador Piab",
      "nombre": "Manguera de succión",
      "comentario": null,
      "valor": "P",
      "primerOrden": 3,
      "segundoOrden": 2,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "3. Sistema de Elevador Piab",
      "nombre": "Bomba de vacio",
      "comentario": null,
      "valor": "P",
      "primerOrden": 3,
      "segundoOrden": 3,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "3. Sistema de Elevador Piab",
      "nombre": "Acople bomba de vacío - tolva de sazonado",
      "comentario": null,
      "valor": "P",
      "primerOrden": 3,
      "segundoOrden": 4,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "4. Sazonador",
      "nombre": "Tolva de Sazonado (Balanza interna, Tornillo, Rejilla (tamiz), Empaquetadura o tolva de jebe interna)",
      "comentario": null,
      "valor": "P",
      "primerOrden": 4,
      "segundoOrden": 1,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "4. Sazonador",
      "nombre": "Brazo alimentador o escalibur",
      "comentario": null,
      "valor": "P",
      "primerOrden":4,
      "segundoOrden": 2,
      "observacion": ""
    },
    {
      "id": 0,
      "variableBasicaId": 4,
      "padre": "4. Sazonador",
      "nombre": "Tambor rotativo",
      "comentario": null,
      "valor": "P",
      "primerOrden":4,
      "segundoOrden": 3,
      "observacion": ""
    },
  ]

  valores = [
    { id: 'C', texto: 'Conforme' },
    { id: 'NC', texto: 'No Conforme' },
    { id: 'NA', texto: 'No Aplica' },
    { id: 'P', texto: 'Pendiente' },
  ]


  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) { }

  ngOnInit(): void {
  }

  cerrar() {
    this.ref.close();
  }

  agregar() {

  }

}
