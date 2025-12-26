import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { DataFrituraStorage, Freidora } from 'src/app/core/models/fritura/data-orden.interface';
import { AuthService } from 'src/app/services/auth.service';
import { OrdenService as OrdenFRService } from 'src/app/services/fritura/orden.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-validacion-usuario',
  templateUrl: './validacion-usuario.component.html',
  styleUrls: ['./validacion-usuario.component.scss'],
  providers: [ MessageService ],
})
export class ValidacionUsuarioComponent implements OnInit {

  data: any;

  freidoras: Freidora[] = [];

  dataFritura: DataFrituraStorage = {};

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private jwtTokenService: JwtTokenService,
    private authService: AuthService,
    private ordenFRService: OrdenFRService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private storageAppService: StorageAppService,
  ) {
    this.data = this.activatedRoute.snapshot.params;
  }

  ngOnInit(): void {

    const { linea, orden, usuario } = this.data;

    this.spinner.show();

    this.authService.validate(usuario)
      .subscribe((v: any) => {
        
        if (v.ok) {

          this.jwtTokenService.setToken(v.token);

          this.ordenFRService.getAllFreidora()
              .subscribe(resp => {

                this.freidoras = resp.data;
                this.cargarDatosFritura(linea, orden);

              });

        }

      });

  }


  cargarDatosFritura(nlinea:number, orden: string) {

    this.ordenFRService.getByIdOrden(nlinea, orden)
    .subscribe(resp => {

      if (resp.ok) {
        if (resp.data && resp.data.Orden) {
          const freidora = this.freidoras.filter(f => f.Id == nlinea)[0];

          this.dataFritura = {
            freidora,
            orden: resp.data
          }

          this.storageAppService.DataFritura = this.dataFritura;
         
          this.spinner.hide();
          this.router.navigate(['/fritura']);
        } else {
          this.spinner.hide();
          this.messageService.add({ severity: 'warn', summary: 'Mensaje Advertencia', detail: 'No se encontró información de la Orden de Fritura' });
        }
      } else {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Mensaje de Error', detail: 'No se ha podido obtener la información en consulta.' });
      }

    })

  }

}
