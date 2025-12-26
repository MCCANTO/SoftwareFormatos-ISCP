import { Component, OnInit } from '@angular/core';
import { DataEnvasadoStorage } from 'src/app/core/models/envasado/envasado-data';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-banner-orden-envasado',
  templateUrl: './banner-orden-envasado.component.html',
  styleUrls: ['./banner-orden-envasado.component.scss']
})
export class BannerOrdenEnvasadoComponent implements OnInit{

  dataEnv!: DataEnvasadoStorage;

  constructor(
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    this.dataEnv = this.storageAppService.DataEnvasado;
  }

}
