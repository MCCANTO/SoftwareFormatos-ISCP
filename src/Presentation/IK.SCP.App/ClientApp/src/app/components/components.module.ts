import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerOrdenEnvasadoComponent } from './banner-orden-envasado/banner-orden-envasado.component';
import { VariableVerificacionComponent } from './variable-verificacion/variable-verificacion.component';
import { CondicionComponent } from './condicion/condicion.component';
import { ListadoCondicionComponent } from './listado-condicion/listado-condicion.component';
import { ListadoVariableVerificacionComponent } from './listado-variable-verificacion/listado-variable-verificacion.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { FormsModule } from '@angular/forms';
import { VariableBasicaComponent } from './variable-basica/variable-basica.component';
import { VerificacionEquipoComponent } from './verificacion-equipo/verificacion-equipo.component';
import { VisorImagenComponent } from './visor-imagen/visor-imagen.component';
import { Base64Pipe } from '../pipes/base64.pipe';
import { BannerOrdenGranelComponent } from './banner-orden-granel/banner-orden-granel.component';
import { BannerFrituraComponent } from './banner-fritura/banner-fritura.component';
import { BannerAcondicionamientoComponent } from './banner-acondicionamiento/banner-acondicionamiento.component';
import { CondicionPreviaComponent } from './condicion-previa/condicion-previa.component';


@NgModule({
  declarations: [
    BannerOrdenEnvasadoComponent,
    VariableVerificacionComponent,
    CondicionComponent,
    ListadoCondicionComponent,
    ListadoVariableVerificacionComponent,
    VariableBasicaComponent,
    VerificacionEquipoComponent,
    VisorImagenComponent,
    Base64Pipe,
    BannerOrdenGranelComponent,
    BannerFrituraComponent,
    BannerAcondicionamientoComponent,
    CondicionPreviaComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    PrimeNgModule,
    
  ],
  exports: [
    BannerOrdenEnvasadoComponent,
    VariableVerificacionComponent,
    CondicionComponent,
    ListadoCondicionComponent,
    ListadoVariableVerificacionComponent,
    VariableBasicaComponent,
    VerificacionEquipoComponent,
    Base64Pipe,
    BannerOrdenGranelComponent,
    BannerFrituraComponent,
    BannerAcondicionamientoComponent,
  ]
})
export class ComponentsModule { }
