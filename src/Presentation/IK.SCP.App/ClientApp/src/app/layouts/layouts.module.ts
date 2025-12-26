import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DefaultComponent } from './default/default.component';
import { FrituraComponent } from './fritura/fritura.component';
import { ComponentsModule } from '../components/components.module';
import { RouterModule } from '@angular/router';
import { EnvasadoComponent } from './envasado/envasado.component';
import { SaborizadoComponent } from './saborizado/saborizado.component';
import { AcondicionamientoComponent } from './acondicionamiento/acondicionamiento.component';
import { SharedModule } from '../shared/shared.module';
import { EnvasadoGranelComponent } from './envasado-granel/envasado-granel.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';


@NgModule({
  declarations: [
    DefaultComponent,
    FrituraComponent,
    EnvasadoComponent,
    SaborizadoComponent,
    AcondicionamientoComponent,
    EnvasadoGranelComponent,
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    RouterModule,
    SharedModule,
    PrimeNgModule,
  ],
  exports: [
    DefaultComponent,
    FrituraComponent,
    EnvasadoComponent,
    SaborizadoComponent,
    AcondicionamientoComponent,
    EnvasadoGranelComponent,
  ]
})
export class LayoutsModule { }
