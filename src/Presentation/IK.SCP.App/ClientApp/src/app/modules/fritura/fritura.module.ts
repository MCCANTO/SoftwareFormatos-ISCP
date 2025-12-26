import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from 'src/app/prime-ng/prime-ng.module';

import { SharedModule } from 'src/app/shared/shared.module';
import { ComponentsModule } from '../../components/components.module';
import { ArranqueMaquinaComponent } from './arranque-maquina/arranque-maquina.component';
import { ControlAceiteComponent } from './control-aceite/control-aceite.component';
import { RegistroControlAceiteComponent } from './control-aceite/modales/registro-control-aceite/registro-control-aceite.component';
import { DetalleArranqueMaquinaComponent } from './detalle-arranque-maquina/detalle-arranque-maquina.component';
import { FrituraRoutingModule } from './fritura-routing.module';
import { EvaluacionAtributoComponent } from './modales/evaluacion-atributo/evaluacion-atributo.component';
import { VariableBasicaComponent } from './modales/variable-basica/variable-basica.component';
import { CaracterizacionProductoComponent } from './panel-control/modales/caracterizacion-producto/caracterizacion-producto.component';
import { PanelControlComponent } from './panel-control/panel-control.component';
import { ValidacionUsuarioComponent } from './validacion-usuario/validacion-usuario.component';

@NgModule({
  declarations: [
    EvaluacionAtributoComponent,
    ArranqueMaquinaComponent,
    VariableBasicaComponent,
    DetalleArranqueMaquinaComponent,
    ValidacionUsuarioComponent,
    PanelControlComponent,
    CaracterizacionProductoComponent,
    ControlAceiteComponent,
    RegistroControlAceiteComponent,
  ],
  imports: [
    CommonModule,
    FrituraRoutingModule,
    PrimeNgModule,
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule,
    SharedModule,
  ],
  providers: [DatePipe],
})
export class FrituraModule {}
