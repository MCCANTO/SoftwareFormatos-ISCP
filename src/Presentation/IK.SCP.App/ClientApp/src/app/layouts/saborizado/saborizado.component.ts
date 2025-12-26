import { Component, OnInit } from '@angular/core';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { DataSazonadoStorage } from 'src/app/core/models/sazonado/sazonador.interface';
import { StorageAppService } from 'src/app/services/storage-app.service';

@Component({
  selector: 'app-saborizado',
  templateUrl: './saborizado.component.html',
  styleUrls: ['./saborizado.component.scss']
})
export class SaborizadoComponent implements OnInit {

  sazonadorData!: DataSazonadoStorage;

  constructor(
    private storageService: StorageAppService,
  ) {}

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
    this.sazonadorData = this.storageService.DataSazonado;
  }

}
