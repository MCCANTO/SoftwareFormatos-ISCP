import { Component, OnInit } from '@angular/core';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { DataEnvasadoGranelStorage } from 'src/app/core/models/envasado/envasado-granel-data';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-envasado-granel',
  templateUrl: './envasado-granel.component.html',
  styleUrls: ['./envasado-granel.component.scss']
})
export class EnvasadoGranelComponent implements OnInit {

  dataEnv: DataEnvasadoGranelStorage | undefined;

  constructor(
    private storageService: StorageAppService,
  ) { }

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
    this.dataEnv = this.storageService.DataEnvasadoGranel;
    
  }

}
