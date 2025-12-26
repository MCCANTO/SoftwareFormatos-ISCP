import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EnvasadoComponent } from 'src/app/layouts/envasado/envasado.component';
import { ChecklistComponent } from './checklist/checklist.component';
import { ControlComponent } from './control/control.component';
import { DetalleChecklistComponent } from './detalle-checklist/detalle-checklist.component';

const routes: Routes = [

  {
    path: '',
    component: EnvasadoComponent,
    children: [
      { path: 'checklist', component: ChecklistComponent },
      { path: 'detalle-checklist/:id', component: DetalleChecklistComponent },
      { path: 'control', component: ControlComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlendingRoutingModule { }
