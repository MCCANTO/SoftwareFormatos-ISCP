import { Component, OnInit } from '@angular/core';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { DataFrituraStorage } from 'src/app/core/models/fritura/data-orden.interface';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-fritura',
  templateUrl: './fritura.component.html',
  styleUrls: ['./fritura.component.scss']
})
export class FrituraComponent implements OnInit {
  dataFr!: DataFrituraStorage;
  
  constructor(
    private storageAppService: StorageAppService
  ) {}

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
    this.dataFr = this.storageAppService.DataFritura;
  }

}
