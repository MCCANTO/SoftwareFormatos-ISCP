import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-acondicionamiento',
  templateUrl: './acondicionamiento.component.html',
  styleUrls: ['./acondicionamiento.component.scss'],
})
export class AcondicionamientoComponent {
  mostrarComponenteBandeja = true;
  mostrarComponenteRayosX = false;

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
  ) {}

  regresar() {
    this.localStorageService.remove(LocalStorageConstant.FILTRO_BANDEJA_ACONDICIONAMIENTO);
    this.router.navigate(['/']);
  }

  mostrarComponente(index: number) {
    switch (index) {
      case 1:
        this.mostrarComponenteBandeja = true;
        this.mostrarComponenteRayosX = false;
        break;
      case 2:
        this.mostrarComponenteBandeja = false;
        this.mostrarComponenteRayosX = true;
        break;
      default:
        break;
    }
  }
}
