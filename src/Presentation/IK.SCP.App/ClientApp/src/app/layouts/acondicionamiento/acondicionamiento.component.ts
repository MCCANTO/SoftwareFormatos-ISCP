import { Component, OnInit } from '@angular/core';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';

@Component({
  selector: 'app-acondicionamiento',
  templateUrl: './acondicionamiento.component.html',
  styleUrls: ['./acondicionamiento.component.scss']
})
export class AcondicionamientoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
  }

}
