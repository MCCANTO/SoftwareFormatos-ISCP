import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NuevoControlComponent } from './modales/nuevo-control/nuevo-control.component';
import { ControlRayosXService } from 'src/app/services/acondicionamiento/control-rayos-x.service';
import { PermisoAccion } from 'src/app/core/models/security/security.interface';
import { SecurityService } from 'src/app/services/security.service';
import { ACCION_ACO_CONTROL_RAYOS_X } from 'src/app/core/constants/accion.constant';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-control-rayos-x',
  templateUrl: './control-rayos-x.component.html',
  styleUrls: ['./control-rayos-x.component.scss'],
})
export class ControlRayosXComponent implements OnInit {
  PERMISOS_CTRL_RAYOS_X: PermisoAccion = {
    LECTURA: false,
    ESCRITURA: false,
    REVISION: false,
  };

  controles: any[] = [];
  selectedControles: any[] = [];
  periodo: Date = new Date();

  constructor(
    private service: ControlRayosXService,
    private dialogService: DialogService,
    private datePipe: DatePipe,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private securityService: SecurityService
  ) {
    this.securityService
      .validarAcciones([ACCION_ACO_CONTROL_RAYOS_X])
      .then((resp) => {
        this.PERMISOS_CTRL_RAYOS_X = resp[0];
      });
  }

  ngOnInit(): void {
    this.buscar();
  }

  buscar() {
    const periodo = this.datePipe.transform(this.periodo, 'yyyyMM') ?? '';
    this.service.getAll(periodo).subscribe((resp) => {
      if (resp.success) {
        this.controles = resp.data;
      }
    });
  }

  agregar() {
    const vb_ref = this.dialogService.open(NuevoControlComponent, {
      header: 'Nuevo Control',
      width: '450px',
    });

    vb_ref.onClose.subscribe((result) => {
      if (result) {
        this.guardarNuevoControl(result);
      }
    });
  }

  guardarNuevoControl(dataNuevoContol: any) {
    const data = {
      ...dataNuevoContol,
    };
    this.service.insert(data).subscribe((_) => {
      this.buscar();
    });
  }

  revisar() {
    if (this.selectedControles.length === 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Advertencia',
        detail: 'Seleccione al menos un control',
      });

      return;
    }

    this.confirmationService.confirm({
      message:
        'Se marcará(n) el(los) registro(s) seleccionados como revisados. ¿Está seguro de continuar?',
      accept: () => {
        this.service.review(this.selectedControles).subscribe((_) => {
          this.buscar();
        });
      },
    });
  }

  validarControlesPorRevisar() {
    return this.controles.some((x) => !x.Revisado);
  }

  printControlMonitoreoRayosX() {
    const periodo = this.datePipe.transform(this.periodo, 'yyyyMM') ?? '';
    this.service.printControlMonitoreoRayosX(periodo).subscribe(
      (response:Blob) => {
        const fileName = 'archivo-modificado.pdf';
        saveAs(response, fileName);
      }, error => {
        console.error(error);
      }
    )
  }

}
