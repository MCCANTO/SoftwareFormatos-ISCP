import { VerificacionEquipoComponent } from './../../../components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueMaquinaService } from './../../../services/fritura/arranque-maquina.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { DataFritura, DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { VerificacionEquipoService } from 'src/app/services/fritura/verificacion-equipo.service';
import { ArranqueMaquina } from 'src/app/core/models/fritura/arranque-maquina.interface';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { eRol } from 'src/app/core/enums/rol.enum';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-detalle-arranque-maquina',
  templateUrl: './detalle-arranque-maquina.component.html',
  styleUrls: ['./detalle-arranque-maquina.component.scss'],
  providers: [DialogService]
})
export class DetalleArranqueMaquinaComponent implements OnInit {

  id = 0;
  dataFR: DataFrituraStorage = {};

  dataArranqueMaquina!: ArranqueMaquina;

  condicionesPrevias = [];
  verificacionesEquipo = [];
  observaciones = [];

  observacion = '';

  constructor(
    private dialogService: DialogService,
    private router: Router,
    private verificacionEquipoService: VerificacionEquipoService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private jwtTokenService: JwtTokenService,
    private activatedRoute: ActivatedRoute,
    private storageAppService: StorageAppService
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
    if ( !this.id ) {
      this.router.navigate(['/fritura']);
    }
  }

  ngOnInit(): void {

    this.cargarData();

    this.cargarArranqueMaquina();

  }

  get EsMaquinista(): boolean {
    return this.jwtTokenService.esMaquinistaFr();
  }


  cargarArranqueMaquina() {

    this.arranqueMaquinaService.getOpen( 0, '0', this.id )
      .subscribe( resp => {

        if ( resp.ok ) {

          if ( resp.data) {
            this.dataArranqueMaquina = resp.data;
          } else {

          }

        } else {
          this.router.navigate(['/fritura']);
        }

      });

  }

  cargarData() {
    this.dataFR = this.storageAppService.DataFritura;
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerificacion(verificacion.arranqueMaquinaVerificacionEquipoCabId, verificacion.cerrado);
  }

  cargarDetalleVerificacion(id: number, cerrado: boolean = false) {
    this.verificacionEquipoService.getAllDetalle(1, this.dataFR.freidora?.Id ?? 0, id)
      .subscribe(resp => {
        if (resp.success) {

          const variables = resp.data;

          this.dialogService.open(VerificacionEquipoComponent, {
            header: 'Verificación de Equipo previa al arranque',
            width: '95%',
            data: {
              mostrarTipo: false,
              esEditable: false,
              cerrado: true,
              variables
            }
          });
        }
      });
  }

  regresar() {
    this.router.navigate(['/fritura']);
  }

}
