import { SazonadorService } from './../../../services/sazonado/sazonador.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { NuevoArranqueComponent } from '../modales/nuevo-arranque/nuevo-arranque.component';
import { DataSazonadoStorage } from 'src/app/core/models/sazonado/sazonador.interface';
import { BandejaSazonado } from '../../../core/models/sazonado/sazonador.interface';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [ DialogService ]
})
export class HomeComponent implements OnInit {

  fecha: Date | null = null
  linea!: number;
  producto!: string;
  sabor!: string;

  sazonadorData!: DataSazonadoStorage;
  lineasFR: any[] = [];

  arranques: BandejaSazonado[] = [];

  constructor(
    private router: Router,
    private dialogService: DialogService,
    private sazonadorService: SazonadorService,
    private storageAppService: StorageAppService,
  ) { }

  ngOnInit(): void {
    this.cargarDataSazonador();
    this.cargarLineasFR();
    this.cargarGrilla();
  }

  cargarGrilla() {
    this.sazonadorService.getAllArranque(
      this.sazonadorData.sazonador.sazonadorId,
      this.fecha,
      this.linea,
      this.producto,
      this.sabor
    ).subscribe( resp => {
        if( resp.success ) this.arranques = resp.data;
      });
  }

  cargarDataSazonador() {
    this.sazonadorData = this.storageAppService.DataSazonado;
  }

  cargarLineasFR() {
    this.sazonadorService.getAllSazonadorFritura(this.sazonadorData.sazonador.sazonadorId)
      .subscribe(resp => {
        this.lineasFR = resp.data;
      });
  }

  agregar() {

    const vb_ref = this.dialogService.open(NuevoArranqueComponent, {
      header: 'Nuevo Checklist de Arranque de Saborizado',
      width: '90%',
      data: {
        sazonadorId: this.sazonadorData.sazonador.sazonadorId,
      }
    });

    vb_ref.onClose.subscribe(() => {
      this.cargarGrilla();
    });

  }

  regresar() {
    this.router.navigate(['/']);
  }


  verArranque( arranqueId: number ) {
    this.router.navigate(['/sazonado/arranque/' + arranqueId]);
  }

  printTemplateArranqueSazonado(arranqueId: number) {
    this.sazonadorService.printTemplateArranqueSazonado(arranqueId)
      .subscribe((response: Blob) => {

        const fileName = 'archivo-modificado.pdf'; // Nombre del archivo a descargar

        // Utiliza la función saveAs del módulo FileSaver para descargar el archivo
        saveAs(response, fileName);
      }, error => {
        console.log('Error al descargar el archivo PDF', error);
      });
  }
}
