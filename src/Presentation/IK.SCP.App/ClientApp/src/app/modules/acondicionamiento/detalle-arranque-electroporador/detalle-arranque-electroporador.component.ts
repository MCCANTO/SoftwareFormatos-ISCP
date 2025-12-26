import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { VerificacionEquipoComponent } from 'src/app/components/verificacion-equipo/verificacion-equipo.component';
import { ArranqueElectroporadorService } from 'src/app/services/acondicionamiento/arranque-electroporador.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-detalle-arranque-electroporador',
  templateUrl: './detalle-arranque-electroporador.component.html',
  styleUrls: ['./detalle-arranque-electroporador.component.scss'],
})
export class DetalleArranqueElectroporadorComponent implements OnInit {
  id: number = 0;
  arranque!: any;

  constructor(
    private router: Router,
    private service: ArranqueElectroporadorService,
    private dialogService: DialogService,
    private activatedRoute: ActivatedRoute
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.cargarDatosChecklist();
  }

  cargarDatosChecklist() {
    this.service.getById(this.id).subscribe((resp) => {
      if (resp.success) {
        this.arranque = resp.data;
      }
    });
  }

  verVerificacion(row: any) {
    this.cargarDetalleVerficiacion(row.id, true);
  }

  cargarDetalleVerficiacion(id: number, cerrado: boolean = false) {
    this.service.getAllVerificacionDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const variables = resp.data;

        this.dialogService.open(VerificacionEquipoComponent, {
          header: 'Verificación de Equipo previa al arranque',
          width: '95%',
          data: {
            mostrarTipo: false,
            esEditable: !cerrado,
            cerrado: cerrado,
            variables,
          },
        });
      }
    });
  }
  regresar() {
    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  printCheckListArranqueElectroporador(){
    this.service.printCheckListArranqueElectroporador(this.id).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }
}
