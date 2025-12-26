import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EnvasadoComponent } from 'src/app/layouts/envasado/envasado.component';
import { ArranqueMaquinaComponent } from './arranque-maquina/arranque-maquina.component';
import { ArranqueComponent } from './arranque/arranque.component';
import { DetalleArranqueMaquinaComponent } from './detalle-arranque-maquina/detalle-arranque-maquina.component';
import { PanelControlComponent } from './panel-control/panel-control.component';
import { ValidarOrdenComponent } from './validar-orden/validar-orden.component';
import {DetalleArranqueComponent} from "./detalle-arranque/detalle-arranque.component";

const routes: Routes = [
  {
    path: 'validacion-usuario/:usuario/:envasadoraId/:orden',
    component: ValidarOrdenComponent,
  },
  {
    path: '', //canActivate: [AuthGuard],
    component: EnvasadoComponent,
    children: [
      { path: '', component: PanelControlComponent },
      { path: 'arranque-maquina', component: ArranqueMaquinaComponent },
      {
        path: 'arranque-maquina/:id',
        component: DetalleArranqueMaquinaComponent,
      },
      { path: 'arranque', component: ArranqueComponent },
      {
        path: 'arranque/:id',
        component: DetalleArranqueComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EnvasadoRoutingModule {}
