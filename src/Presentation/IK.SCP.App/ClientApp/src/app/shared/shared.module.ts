import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TituloPaginaComponent } from './titulo-pagina/titulo-pagina.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { TarjetaSeccionComponent } from './tarjeta-seccion/tarjeta-seccion.component';
import { CardButtonComponent } from './card-button/card-button.component';
import { NavbarComponent } from './navbar/navbar.component';
import { TituloTarjetaModuloComponent } from './titulo-tarjeta-modulo/titulo-tarjeta-modulo.component';

@NgModule({
  declarations: [
    TituloPaginaComponent,
    TarjetaSeccionComponent,
    CardButtonComponent,
    NavbarComponent,
    TituloTarjetaModuloComponent,
  ],
  imports: [CommonModule, PrimeNgModule],
  exports: [
    TituloPaginaComponent,
    TarjetaSeccionComponent,
    CardButtonComponent,
    NavbarComponent,
    TituloTarjetaModuloComponent,
  ],
})
export class SharedModule {}
