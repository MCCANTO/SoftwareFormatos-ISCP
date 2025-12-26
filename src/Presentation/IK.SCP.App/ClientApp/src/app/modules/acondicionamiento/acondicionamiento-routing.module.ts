import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AcondicionamientoComponent as AcondicionamientoLayoutComponent } from '../../layouts/acondicionamiento/acondicionamiento.component';
import { AcondicionamientoComponent } from './acondicionamiento.component';
import { ArranqueElectroporadorComponent } from './arranque-electroporador/arranque-electroporador.component';
import { ArranqueLavadoTuberculoComponent } from './arranque-lavado-tuberculo/arranque-lavado-tuberculo.component';
import { ChecklistMaizComponent } from './checklist-maiz/checklist-maiz.component';
import { ControlElectroporadorComponent } from './control-electroporador/control-electroporador.component';
import { ControlMaizComponent } from './control-maiz/control-maiz.component';
import { ControlRemojoHabaComponent } from './control-remojo-haba/control-remojo-haba.component';
import { ControlReposoMaizComponent } from './control-reposo-maiz/control-reposo-maiz.component';
import { DetalleArranqueElectroporadorComponent } from './detalle-arranque-electroporador/detalle-arranque-electroporador.component';
import { DetalleArranqueLavadoTuberculoComponent } from './detalle-arranque-lavado-tuberculo/detalle-arranque-lavado-tuberculo.component';
import { DetalleChecklistMaizComponent } from './detalle-checklist-maiz/detalle-checklist-maiz.component';
import { PanelControlComponent } from './panel-control/panel-control.component';

const routes: Routes = [
  { path: '', component: AcondicionamientoComponent },
  {
    path: '',
    component: AcondicionamientoLayoutComponent,
    children: [
      {
        path: 'panel-control',
        component: PanelControlComponent,
      },

      {
        path: 'arranque-maiz',
        component: ChecklistMaizComponent,
      },

      {
        path: 'control-maiz',
        component: ControlMaizComponent,
      },

      {
        path: 'detalle-arranque-maiz/:id',
        component: DetalleChecklistMaizComponent,
      },

      {
        path: 'control-reposo-maiz',
        component: ControlReposoMaizComponent,
      },

      {
        path: 'control-remojo-haba',
        component: ControlRemojoHabaComponent,
      },

      {
        path: 'arranque-lavado-tuberculo',
        component: ArranqueLavadoTuberculoComponent,
      },

      {
        path: 'detalle-arranque-lavado-tuberculo/:id',
        component: DetalleArranqueLavadoTuberculoComponent,
      },

      {
        path: 'arranque-electroporador',
        component: ArranqueElectroporadorComponent,
      },

      {
        path: 'detalle-arranque-electroporador/:id',
        component: DetalleArranqueElectroporadorComponent,
      },

      {
        path: 'control-electroporador',
        component: ControlElectroporadorComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AcondicionadoRoutingModule {}
