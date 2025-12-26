import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlendingRoutingModule } from './blending-routing.module';
import { ChecklistComponent } from './checklist/checklist.component';
import { ControlComponent } from './control/control.component';
import { PrimeNgModule } from '../../prime-ng/prime-ng.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NuevoControlComponent } from './control/modales/nuevo-control/nuevo-control.component';
// import { VerificacionEquipoComponent } from './modales/verificacion-equipo/verificacion-equipo.component';
import { SharedModule } from '../../shared/shared.module';
import { DetalleChecklistComponent } from './detalle-checklist/detalle-checklist.component';


@NgModule({
  declarations: [
    ChecklistComponent,
    ControlComponent,
    NuevoControlComponent,
    // VerificacionEquipoComponent,
    DetalleChecklistComponent
  ],
  imports: [
    CommonModule,
    BlendingRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule,
  ]
})
export class BlendingModule { }
