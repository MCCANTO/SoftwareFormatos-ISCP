import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from 'src/app/components/components.module';
import { LayoutsModule } from 'src/app/layouts/layouts.module';
import { PrimeNgModule } from 'src/app/prime-ng/prime-ng.module';
import { SharedModule } from '../../shared/shared.module';
import { ArranqueMaquinaComponent } from './arranque-maquina/arranque-maquina.component';
import { ArranqueComponent } from './arranque/arranque.component';
import { DetalleArranqueMaquinaComponent } from './detalle-arranque-maquina/detalle-arranque-maquina.component';
import { EnvasadoRoutingModule } from './envasado-routing.module';
import { CargaCodificacionComponent } from './modales/carga-codificacion/carga-codificacion.component';
import { ComponentesComponent } from './modales/componentes/componentes.component';
import { CondicionPreviaComponent } from './modales/condicion-previa/condicion-previa.component';
import { CondicionesPreviasEnvasadoComponent } from './modales/condiciones-previas-envasado/condiciones-previas-envasado.component';
import { ContramuestraComponent } from './modales/contramuestra/contramuestra.component';
import { InspeccionComponent } from './modales/inspeccion/inspeccion.component';
import { ObservacionComponent } from './modales/observacion/observacion.component';
import { PersonalComponent } from './modales/personal/personal.component';
import { RevisionComponent } from './modales/revision/revision.component';
import { VariableBasicaArranqueComponent } from './modales/variable-basica-arranque/variable-basica-arranque.component';
import { VariableBasicaComponent } from './modales/variable-basica/variable-basica.component';
import { RegistroPedaceriaComponent } from './panel-control/modales/registro-pedaceria/registro-pedaceria.component';
import { PanelControlComponent } from './panel-control/panel-control.component';
import { ValidarOrdenComponent } from './validar-orden/validar-orden.component';
import {DetalleArranqueComponent} from "./detalle-arranque/detalle-arranque.component";

@NgModule({
  declarations: [
    ArranqueMaquinaComponent,
    CondicionPreviaComponent,
    VariableBasicaComponent,
    ValidarOrdenComponent,
    ArranqueComponent,
    CargaCodificacionComponent,
    DetalleArranqueMaquinaComponent,
    DetalleArranqueComponent,
    ContramuestraComponent,
    PersonalComponent,
    ComponentesComponent,
    ObservacionComponent,
    InspeccionComponent,
    RevisionComponent,
    VariableBasicaArranqueComponent,
    CondicionesPreviasEnvasadoComponent,
    PanelControlComponent,
    RegistroPedaceriaComponent,
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    EnvasadoRoutingModule,
    PrimeNgModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    LayoutsModule,
    SharedModule,
  ],
})
export class EnvasadoModule {}
