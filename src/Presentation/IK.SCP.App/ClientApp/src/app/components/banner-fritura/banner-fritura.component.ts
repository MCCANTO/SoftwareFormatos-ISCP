import { Component } from '@angular/core';
import { DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-banner-fritura',
  templateUrl: './banner-fritura.component.html',
  styleUrls: ['./banner-fritura.component.scss']
})
export class BannerFrituraComponent {
  dataFr!: DataFrituraStorage;

  constructor(
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    this.dataFr = this.storageAppService.DataFritura;
  }
}
