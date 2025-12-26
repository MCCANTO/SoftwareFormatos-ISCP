import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FrituraComponent } from 'src/app/layouts/fritura/fritura.component';
import { ArranqueMaquinaComponent } from './arranque-maquina/arranque-maquina.component';
import { ControlAceiteComponent } from './control-aceite/control-aceite.component';
import { DetalleArranqueMaquinaComponent } from './detalle-arranque-maquina/detalle-arranque-maquina.component';
import { EvaluacionAtributoComponent } from './modales/evaluacion-atributo/evaluacion-atributo.component';
import { PanelControlComponent } from './panel-control/panel-control.component';
import { ValidacionUsuarioComponent } from './validacion-usuario/validacion-usuario.component';

const routes: Routes = [
  {
    path: '', //canActivate: [AuthGuard],
    component: FrituraComponent,
    children: [
      { path: '', component: PanelControlComponent },
      { path: 'arranque-maquina', component: ArranqueMaquinaComponent },
      {
        path: 'detalle-arranque-maquina/:id',
        component: DetalleArranqueMaquinaComponent,
      },
      { path: 'evaluacion-atributo', component: EvaluacionAtributoComponent },
      { path: 'control-aceite', component: ControlAceiteComponent },
    ],
  },
  {
    path: 'validacion-usuario/:usuario/:linea/:orden',
    component: ValidacionUsuarioComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FrituraRoutingModule {}
