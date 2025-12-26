import { Component } from '@angular/core';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-banner-acondicionamiento',
  templateUrl: './banner-acondicionamiento.component.html',
  styleUrls: ['./banner-acondicionamiento.component.scss']
})
export class BannerAcondicionamientoComponent  {

  dataAco!: any;

  constructor( private storageAppService: StorageAppService ){}

  ngOnInit(): void {
    this.dataAco = this.storageAppService.DataAcondicionamiento;
  }
}
