import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { DataEnvasadoStorage } from 'src/app/core/models/envasado/envasado-data';
import { DataEnvasadoGranelStorage } from 'src/app/core/models/envasado/envasado-granel-data';
import { AuthService } from 'src/app/services/auth.service';
import { OrdenService } from 'src/app/services/envasado/orden.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-validar-orden',
  templateUrl: './validar-orden.component.html',
  styleUrls: ['./validar-orden.component.scss'],
  providers: [NgxSpinnerService]
})
export class ValidarOrdenComponent implements OnInit {

  envasadoras: any[] = [];
  data: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jwtTokenService: JwtTokenService,
    // private localStorageService: LocalStorageService,
    private authService: AuthService,
    private ordenService: OrdenService,
    private spinner: NgxSpinnerService,
    private storageAppService: StorageAppService,
  ) {

    this.data = this.route.snapshot.params;

  }
  ngOnInit(): void {
    const { envasadoraId, orden, usuario } = this.data;

    this.spinner.show();
    this.authService.validate(usuario)
      .subscribe((v: any) => {

        if (v.ok) {
          // this.jwtTokenService.removeToken();
          this.jwtTokenService.setToken(v.token);

          this.ordenService.getAllEnvasadora()
            .subscribe(t => {

              if (t.ok) {

                this.envasadoras = [...t.data];

                this.ordenService.getByIdOrden(orden)
                  .subscribe(d => {

                    if (d.ok) {

                      const envasadora = this.envasadoras.filter(p => p.Id == envasadoraId)[0];

                      if ( envasadoraId == 4 ) {
                        const data: DataEnvasadoGranelStorage = {
                          envasadora,
                          orden: d.data,
                        };
                        this.storageAppService.DataEnvasadoGranel = data;
                        this.router.navigate(['/envasado-granel']);
                      } else {
                        const data: DataEnvasadoStorage = {
                          envasadora,
                          orden: d.data,
                        };
                        this.storageAppService.DataEnvasado = data;
                        this.router.navigate(['/envasado']);
                      }


                    }

                  })

              }

            });

        }

      });

  }

}
