import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { DataAcondicionamientoStorage } from 'src/app/core/models/acondicionamiento/acondicionamiento-data';
import { AcondicionamientoService } from 'src/app/services/acondicionamiento/acondicionamiento.service';
import { StorageAppService } from 'src/app/services/storage-app.service';
import { NuevaOrdenComponent } from './modales/nueva-orden/nueva-orden.component';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';

@Component({
  selector: 'app-bandeja-orden',
  templateUrl: './bandeja-orden.component.html',
  styleUrls: ['./bandeja-orden.component.scss'],
})
export class BandejaOrdenComponent {
  ordenes: any[] = [];

  materiasPrima: any[] = [];

  ordenId = '';
  fechaInicio!: Date;
  fechaFin!: Date;
  materiaPrimaId: number = 0;

  first = 0;
  rows = 10;
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private storageAppService: StorageAppService,
    private acondicionamientoService: AcondicionamientoService,
    private datePipe: DatePipe,
    private localStorageService: LocalStorageService,
  ) {}

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
    this.listarMateriaPrima();
    this.listarOrdenes();

    this.getFilterDate();
  }

  getFilterDate() {
    const filtros = this.localStorageService.get(LocalStorageConstant.FILTRO_BANDEJA_ACONDICIONAMIENTO);

    if (filtros) {
      const data = JSON.parse(filtros);

      this.ordenId = data.ordenId;
      this.fechaInicio = new Date(data.fechaInicio);
      this.fechaFin = new Date(data.fechaFin);
      this.materiaPrimaId = data.materiaPrimaId;
      this.listarOrdenes();
    } else {
      this.fechaInicio = new Date();
      this.fechaFin = new Date();
    }
  }

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.ordenes ? this.first === this.ordenes.length - this.rows : true;
  }

  isFirstPage(): boolean {
    return this.ordenes ? this.first === 0 : true;
  }

  verOrden(orden: any) {
    const data: DataAcondicionamientoStorage = {
      orden: orden.OrdenId,
      fecha: orden.FechaEjecucion,
      materiaPrima: {
        id: orden.MateriaPrimaId,
        nombre: orden.MateriaPrima,
      },
    };

    this.storageAppService.DataAcondicionamiento = data;

    this.router.navigate(['/acondicionamiento/panel-control']);
  }

  nuevaOrden() {
    const vb_ref = this.dialogService.open(NuevaOrdenComponent, {
      header: 'Nueva Orden de Acondicionamiento',
      width: '45%',
      position: 'top',
      data: {
        materiasPrima: this.materiasPrima,
      },
    });

    vb_ref.onClose.subscribe((result) => {
      if (result) {
        this.acondicionamientoService.insertOrden(result).subscribe((resp) => {
          this.listarOrdenes();
        });
      }
    });
  }

  listarOrdenes() {
    this.acondicionamientoService
      .getAllOrdenes(
        this.ordenId ?? '',
        this.datePipe.transform(this.fechaInicio, 'yyyy-MM-dd') ?? '',
        this.datePipe.transform(this.fechaFin, 'yyyy-MM-dd') ?? '',
        this.materiaPrimaId ?? 0
      )
      .subscribe((resp) => {
        if (resp.success){
          this.ordenes = resp.data;
          this.setFilterDate();
        }
      });
  }

  setFilterDate() {
    const filtros = {
      ordenId: this.ordenId,
      fechaInicio: this.fechaInicio,
      fechaFin: this.fechaFin,
      materiaPrimaId: this.materiaPrimaId,
    }
    this.localStorageService.set(LocalStorageConstant.FILTRO_BANDEJA_ACONDICIONAMIENTO, JSON.stringify(filtros));
  }

  listarMateriaPrima() {
    this.acondicionamientoService.getAllMateriaPrima().subscribe((resp) => {
      if (resp.success) this.materiasPrima = resp.data;
    });
  }
  obtenerSeveridad(cerrado: boolean) {
    if (cerrado) return 'success';
    else return 'warning';
  }

}
