import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueLavadoTuberculoService } from 'src/app/services/acondicionamiento/arranque-lavado-tuberculo.service';

@Component({
  selector: 'app-detalle-arranque-lavado-tuberculo',
  templateUrl: './detalle-arranque-lavado-tuberculo.component.html',
  styleUrls: ['./detalle-arranque-lavado-tuberculo.component.scss'],
})
export class DetalleArranqueLavadoTuberculoComponent implements OnInit {
  id = 0;
  arranque!: any;

  constructor(
    private service: ArranqueLavadoTuberculoService,
    private router: Router,
    private dialogService: DialogService,
    private activatedRoute: ActivatedRoute
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.cargarData();
  }

  cargarData() {
    this.service.getByIdArranqueLavadoTuberculo(this.id).subscribe((resp) => {
      if (resp.success) {
        this.arranque = resp.data;
      }
    });
  }

  verVerificacion(verificacion: any) {
    this.cargarDetalleVerficiacion(verificacion.id, verificacion.cerrado);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.service.getAllVerificacionEquipoDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const verificaciones = resp.data;

        this.dialogService.open(VerificacionEquipoComponent, {
          header: 'Verificación de Equipo previa al arranque',
          width: '95%',
          data: {
            mostrarTipo: false,
            esEditable: false,
            cerrado: true,
            variables: verificaciones,
          },
        });
      }
    });
  }

  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }
}
