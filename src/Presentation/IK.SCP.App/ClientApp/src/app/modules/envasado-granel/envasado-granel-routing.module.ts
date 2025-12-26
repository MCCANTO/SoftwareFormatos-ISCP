import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EnvasadoGranelComponent } from 'src/app/layouts/envasado-granel/envasado-granel.component';
import { ControlGranelComponent } from './control-granel/control-granel.component';
import { ControlGranelParametroComponent } from './control-granel-parametro/control-granel-parametro.component';
import { ControlGranelChecklistComponent } from './control-granel-checklist/control-granel-checklist.component';
import { DetalleControlGranelChecklistComponent } from './detalle-control-granel-checklist/detalle-control-granel-checklist.component';

const routes: Routes = [
  {
    path: '',
    component: EnvasadoGranelComponent,
    children: [
      { path: '', component: ControlGranelComponent },
      { path: 'checklist', component: ControlGranelChecklistComponent },
      { path: 'checklist/detalle/:id', component: DetalleControlGranelChecklistComponent },
      { path: 'control', component: ControlGranelParametroComponent },
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnvasadoGranelRoutingModule { }
