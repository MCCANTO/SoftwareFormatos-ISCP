import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  { path: '', loadChildren: () => import('./modules/dashboard/dashboard.module').then( m => m.DashboardModule) },
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then( m => m.AuthModule) },
  { path: 'fritura', loadChildren: () => import('./modules/fritura/fritura.module').then( m => m.FrituraModule ) },
  { path: 'envasado', loadChildren: () => import('./modules/envasado/envasado.module').then( m => m.EnvasadoModule ) },
  { path: 'envasado-granel', loadChildren: () => import('./modules/envasado-granel/envasado-granel.module').then( m => m.EnvasadoGranelModule ) },
  { path: 'sazonado', loadChildren: () => import('./modules/saborizado/saborizado.module').then( m => m.SaborizadoModule ) },
  { path: 'blending', loadChildren: () => import('./modules/blending/blending.module').then( m => m.BlendingModule ) },
  { path: 'acondicionamiento', loadChildren: () => import('./modules/acondicionamiento/acondicionamiento.module').then( m => m.AcondicionadoModule ) },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
