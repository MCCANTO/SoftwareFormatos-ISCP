import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SaborizadoComponent } from 'src/app/layouts/saborizado/saborizado.component';
import { HomeComponent } from './home/home.component';
import { ArranqueComponent } from './arranque/arranque.component';

const routes: Routes = [
  {
    path: '', //canActivate: [AuthGuard],
    component: SaborizadoComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'arranque/:id', component: ArranqueComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SaborizadoRoutingModule { }
