import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EnvasadoGranelRoutingModule } from './envasado-granel-routing.module';
import { ControlGranelParametroComponent } from './control-granel-parametro/control-granel-parametro.component';
import { ControlGranelComponent } from './control-granel/control-granel.component';
import { ControlGranelChecklistComponent } from './control-granel-checklist/control-granel-checklist.component';
import { PrimeNgModule } from 'src/app/prime-ng/prime-ng.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { NuevoControlGranelComponent } from './modales/nuevo-control-granel/nuevo-control-granel.component';
import { FormsModule } from '@angular/forms';
import { CargaCodificacionComponent } from './modales/carga-codificacion/carga-codificacion.component';
import { DetalleControlGranelChecklistComponent } from './detalle-control-granel-checklist/detalle-control-granel-checklist.component';


@NgModule({
  declarations: [
    ControlGranelComponent,
    ControlGranelChecklistComponent,
    ControlGranelParametroComponent,
    NuevoControlGranelComponent,
    CargaCodificacionComponent,
    DetalleControlGranelChecklistComponent,
  ],
  imports: [
    CommonModule,
    EnvasadoGranelRoutingModule,
    PrimeNgModule,
    SharedModule,
    FormsModule,
  ]
})
export class EnvasadoGranelModule { }
