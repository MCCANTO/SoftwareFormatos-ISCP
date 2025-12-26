import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { DataEnvasadoStorage } from 'src/app/core/models/envasado/envasado-data';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-envasado',
  templateUrl: './envasado.component.html',
  styleUrls: ['./envasado.component.scss'],
  providers: [MessageService, DialogService, ConfirmationService]
})
export class EnvasadoComponent implements OnInit {

  dataEnv!: DataEnvasadoStorage;

  constructor(
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
    this.dataEnv = this.storageAppService.DataEnvasado;
  }

}
