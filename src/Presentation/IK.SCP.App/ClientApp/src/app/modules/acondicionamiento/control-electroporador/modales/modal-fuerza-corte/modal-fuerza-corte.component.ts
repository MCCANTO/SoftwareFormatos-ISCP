import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-modal-fuerza-corte',
  templateUrl: './modal-fuerza-corte.component.html',
  styleUrls: ['./modal-fuerza-corte.component.scss'],
})
export class ModalFuerzaCorteComponent implements OnInit {
  esEditable: boolean = true;

  fuerzasCorte: any[] = [
    { item: 1, controlSinPef: null, pef: null },
    { item: 2, controlSinPef: null, pef: null },
    { item: 3, controlSinPef: null, pef: null },
    { item: 4, controlSinPef: null, pef: null },
    { item: 5, controlSinPef: null, pef: null },
    { item: 6, controlSinPef: null, pef: null },
    { item: 7, controlSinPef: null, pef: null },
    { item: 8, controlSinPef: null, pef: null },
    { item: 9, controlSinPef: null, pef: null },
    { item: 10, controlSinPef: null, pef: null },
  ];

  constructor(
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    if (this.config.data) {
      this.esEditable = this.config.data?.esEditable;
      if (this.config.data?.detalle.length > 0)
        this.fuerzasCorte = this.config.data?.detalle;
    }
  }

  get existenControlesVacios(): boolean {
    return this.fuerzasCorte.some((x) => x.controlSinPef == null);
  }

  get existenPefsVacios(): boolean {
    return this.fuerzasCorte.some((x) => x.pef == null);
  }

  calculoPromedioSinPef() {
    let promedio = 0;
    let suma = 0;
    const registros = this.fuerzasCorte.filter((x) => x.controlSinPef != null);
    if (registros?.length > 0) {      
      registros.forEach((x) => suma+=x.controlSinPef);
      promedio = suma / registros.length;
    }
    return promedio;
  }

  calculoPromedioPef() {
    let promedio = 0;
    let suma = 0;
    const registros = this.fuerzasCorte.filter((x) => x.pef != null);
    if (registros?.length > 0) {      
      registros.forEach((x) => suma+=x.pef);
      promedio = suma / registros.length;
    }
    return promedio;
  }

  calculoPorcentajePef() {
    let porcentaje = 0;
    const promedioSinPef = this.calculoPromedioSinPef();
    const promedioPef = this.calculoPromedioPef();
    if (promedioSinPef > 0) {
      porcentaje = (1 - promedioPef / promedioSinPef) * 100;
    }
    return porcentaje;
  }

  cerrar() {
    this.ref.close();
  }
  guardar() {
    if (this.existenControlesVacios || this.existenPefsVacios) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'Debe completar todos los campos',
      });
      return;
    }
    this.ref.close(this.fuerzasCorte);
  }
}
