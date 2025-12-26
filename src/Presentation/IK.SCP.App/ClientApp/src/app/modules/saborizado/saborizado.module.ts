import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SaborizadoRoutingModule } from './saborizado-routing.module';
import { HomeComponent } from './home/home.component';
import { PrimeNgModule } from 'src/app/prime-ng/prime-ng.module';
import { ArranqueComponent } from './arranque/arranque.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VerificacionEquipoComponent } from './modales/verificacion-equipo/verificacion-equipo.component';
import { NuevoArranqueComponent } from './modales/nuevo-arranque/nuevo-arranque.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    HomeComponent,
    ArranqueComponent,
    VerificacionEquipoComponent,
    NuevoArranqueComponent
  ],
  imports: [
    CommonModule,
    SaborizadoRoutingModule,
    PrimeNgModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
  ]
})
export class SaborizadoModule { }
