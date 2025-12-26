import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { CondicionComponent } from 'src/app/components/condicion/condicion.component';
import { VariableBasicaComponent } from 'src/app/components/variable-basica/variable-basica.component';
import { EnvasadoCondicionPrevia } from 'src/app/core/models/envasado/condicion-previa.model';
import { GranelService } from 'src/app/services/envasado/granel.service';
import { OrdenService } from 'src/app/services/fritura/orden.service';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-detalle-control-granel-checklist',
  templateUrl: './detalle-control-granel-checklist.component.html',
  styleUrls: ['./detalle-control-granel-checklist.component.scss'],
  providers: [DialogService, DatePipe],
})
export class DetalleControlGranelChecklistComponent implements OnInit {
  id: number = 0;

  condiciones = [];
  condicionesProceso = [];

  observaciones = [];
  observacion = '';

  especificaciones: any[] = [];

  tiposGranel = [
    { id: 'E', nombre: 'Exportación' },
    { id: 'L', nombre: 'Local' },
    { id: 'T', nombre: 'Tránsito' },
  ];

  tipoGranel = '';

  lineasFritura: any[] = [];
  lineaFritura = 0;

  fechaVctoLote = new Date();
  maquinista = '';
  selladora = '';

  personalPesa: string[] = [];
  personalSella: string[] = [];

  revisiones: string[] = [];
  revisado = false;

  constructor(
    private dialogService: DialogService,
    private granelService: GranelService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private storageAppService: StorageAppService,
    private frituraService: OrdenService,
    private datePipe: DatePipe,
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.cargarFreidoras();
    this.cargarData();
  }

  cargarFreidoras() {
    this.frituraService.getAllFreidora().subscribe((resp) => {
      if (resp.ok) {
        this.lineasFritura = resp.data;
      }
    });
  }

  cargarData() {

    this.granelService
      .getChecklistById(this.id)
      .subscribe((resp) => {
        if (resp.success) {
          const data = resp.data;

          this.id = data.arranqueGranelId;

          this.listarEspecificaciones();
          this.tipoGranel = data.tipoId;
          this.fechaVctoLote = data.fechaVencimiento == null ? new Date() : new Date(data.fechaVencimiento);
          this.lineaFritura = data.lineaFrituraId;
          this.maquinista = data.maquinista;
          this.selladora = data.selladora;
          this.personalPesa = data.personalPesa?.split(',');
          this.personalSella = data.personalSella?.split(',');
          this.condiciones = data.condicionesOperativas;
          this.condicionesProceso = data.condicionesProceso;
          this.observaciones = data.observaciones;

          data.revisiones.forEach((rev: any) => {
            this.revisiones.push(
              `${rev.usuario} - ${this.datePipe.transform(
                rev.fecha,
                'dd/MM/yyyy HH:mm'
              )}`
            );
          });
        }
      });
  }

  listarEspecificaciones() {
    this.granelService
      .getChecklistEspecificaciones(this.id)
      .subscribe((resp) => {
        if (resp.success) {
          this.especificaciones = resp.data;
        }
      });
  }

  listarCondicionesProcesoDetalle(id: number, cerrado: boolean = false) {
    this.granelService.getCondicionesProcesoDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const condicion = {
          variables: resp.data,
          mostrarTipo: false,
          editable: false,
          cerrado: true,
        };

        this.dialogService.open(VariableBasicaComponent, {
          header: 'Condiciones de Proceso',
          width: '95%',
          data: condicion,
        });
      }
    });
  }

  listarCondicionesOperativas(id: number) {
    this.granelService.getCondicionesOperativasDetalle(id).subscribe((resp) => {
      if (resp.success) {
        const condicion: EnvasadoCondicionPrevia = {
          editable: false,
          mostrarTipo: true,
          tipo: resp.data.tipoId,
          condiciones: resp.data.detalle,
          tipos: [],
        };

        this.dialogService.open(CondicionComponent, {
          header: 'Condiciones Operativas',
          width: '85%',
          data: {
            condicion,
          },
        });
      }
    });
  }

  esOtro(esp: any): boolean {
    const result: boolean = esp.valores.some(
      (v: any) => v.nombre === 'OTRO' && v.id == esp.valor
    );
    if (!result) esp.otro = '';
    return result;
  }

  regresar() {
    this.router.navigate(['/envasado-granel']);
  }
}
