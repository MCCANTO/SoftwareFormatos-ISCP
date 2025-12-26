import { Component, OnInit } from '@angular/core';
import { DataEnvasadoGranelStorage } from 'src/app/core/models/envasado/envasado-granel-data';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-banner-orden-granel',
  templateUrl: './banner-orden-granel.component.html',
  styleUrls: ['./banner-orden-granel.component.scss']
})
export class BannerOrdenGranelComponent implements OnInit {

  dataEnvGra!: DataEnvasadoGranelStorage;

  constructor(
    private storageAppService: StorageAppService,
  ) { }

  ngOnInit(): void {
    this.dataEnvGra = this.storageAppService.DataEnvasadoGranel;
  }

}
