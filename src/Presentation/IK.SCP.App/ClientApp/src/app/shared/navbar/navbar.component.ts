import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { UserData } from 'src/app/core/models/user-data.interface';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-custom-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  @Input() subtitulo: string = '';
  @Input() classSubtitulo: string = '';
  @Input() headerTemplate!: TemplateRef<any>;

  userData!: UserData;

  constructor(
    private jwtTokenService: JwtTokenService,
    private localStorageService: LocalStorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userData = this.jwtTokenService.getDecodeToken();
  }

  logout() {
    this.localStorageService.remove(
      LocalStorageConstant.DATA_ORDEN_ACONDICIONAMIENTO
    );
    this.localStorageService.remove(LocalStorageConstant.DATA_ORDEN_ENVASADO);
    this.localStorageService.remove(
      LocalStorageConstant.DATA_ORDEN_ENVASADO_GRANEL
    );
    this.localStorageService.remove(LocalStorageConstant.DATA_ORDEN_FRITURA);
    this.jwtTokenService.removeToken();
    this.router.navigate(['/auth/login']);
  }
}
