import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { LayoutsModule } from '../../layouts/layouts.module';
import { PrimeNgModule } from '../../prime-ng/prime-ng.module';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from 'src/app/components/components.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { AcondicionadoRoutingModule } from './acondicionamiento-routing.module';
import { AcondicionamientoComponent } from './acondicionamiento.component';
import { ArranqueElectroporadorComponent } from './arranque-electroporador/arranque-electroporador.component';
import { ArranqueLavadoTuberculoComponent } from './arranque-lavado-tuberculo/arranque-lavado-tuberculo.component';
import { BandejaOrdenComponent } from './bandeja-orden/bandeja-orden.component';
import { NuevaOrdenComponent } from './bandeja-orden/modales/nueva-orden/nueva-orden.component';
import { ChecklistMaizComponent } from './checklist-maiz/checklist-maiz.component';
import { ControlElectroporadorComponent } from './control-electroporador/control-electroporador.component';
import { ModalCondicionBasicaComponent } from './control-electroporador/modales/modal-condicion-basica/modal-condicion-basica.component';
import { ModalControlTiempoComponent } from './control-electroporador/modales/modal-control-tiempo/modal-control-tiempo.component';
import { ModalFuerzaCorteComponent } from './control-electroporador/modales/modal-fuerza-corte/modal-fuerza-corte.component';
import { DatosInsumoComponent } from './control-maiz/componentes/datos-insumo/datos-insumo.component';
import { DatosMateriaPrimaComponent } from './control-maiz/componentes/datos-materia-prima/datos-materia-prima.component';
import { DatosPeladoComponent } from './control-maiz/componentes/datos-pelado/datos-pelado.component';
import { DatosRemojoComponent } from './control-maiz/componentes/datos-remojo/datos-remojo.component';
import { DatosSancochadoComponent } from './control-maiz/componentes/datos-sancochado/datos-sancochado.component';
import { ControlMaizComponent } from './control-maiz/control-maiz.component';
import { IngresoInsumoComponent } from './control-maiz/modales/ingreso-insumo/ingreso-insumo.component';
import { IngresoMateriaPrimaComponent } from './control-maiz/modales/ingreso-materia-prima/ingreso-materia-prima.component';
import { IngresoPeladoComponent } from './control-maiz/modales/ingreso-pelado/ingreso-pelado.component';
import { IngresoRemojoComponent } from './control-maiz/modales/ingreso-remojo/ingreso-remojo.component';
import { IngresoSancochadoComponent } from './control-maiz/modales/ingreso-sancochado/ingreso-sancochado.component';
import { ControlRayosXComponent } from './control-rayos-x/control-rayos-x.component';
import { NuevoControlComponent } from './control-rayos-x/modales/nuevo-control/nuevo-control.component';
import { ControlRemojoHabaComponent } from './control-remojo-haba/control-remojo-haba.component';
import { IngresoControlRemojoComponent } from './control-remojo-haba/modales/ingreso-control-remojo/ingreso-control-remojo.component';
import { ControlReposoMaizComponent } from './control-reposo-maiz/control-reposo-maiz.component';
import { IngresoControlReposoComponent } from './control-reposo-maiz/modales/ingreso-control-reposo/ingreso-control-reposo.component';
import { DetalleArranqueElectroporadorComponent } from './detalle-arranque-electroporador/detalle-arranque-electroporador.component';
import { DetalleArranqueLavadoTuberculoComponent } from './detalle-arranque-lavado-tuberculo/detalle-arranque-lavado-tuberculo.component';
import { DetalleChecklistMaizComponent } from './detalle-checklist-maiz/detalle-checklist-maiz.component';
import { PanelControlComponent } from './panel-control/panel-control.component';

@NgModule({
  declarations: [
    BandejaOrdenComponent,
    ControlRayosXComponent,
    NuevaOrdenComponent,
    NuevoControlComponent,
    ChecklistMaizComponent,
    ControlMaizComponent,
    DatosInsumoComponent,
    DatosMateriaPrimaComponent,
    DatosPeladoComponent,
    DatosRemojoComponent,
    DatosSancochadoComponent,
    IngresoInsumoComponent,
    IngresoMateriaPrimaComponent,
    IngresoPeladoComponent,
    IngresoRemojoComponent,
    IngresoSancochadoComponent,
    ControlReposoMaizComponent,
    ControlRemojoHabaComponent,
    IngresoControlRemojoComponent,
    IngresoControlReposoComponent,
    ArranqueLavadoTuberculoComponent,
    ArranqueElectroporadorComponent,
    PanelControlComponent,
    ControlElectroporadorComponent,
    ModalControlTiempoComponent,
    ModalFuerzaCorteComponent,
    ModalCondicionBasicaComponent,
    AcondicionamientoComponent,
    DetalleChecklistMaizComponent,
    DetalleArranqueLavadoTuberculoComponent,
    DetalleArranqueElectroporadorComponent,
  ],
  imports: [
    CommonModule,
    AcondicionadoRoutingModule,
    LayoutsModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule,
  ],
  providers: [DatePipe],
})
export class AcondicionadoModule {}
