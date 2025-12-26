import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-listado-condicion',
  templateUrl: './listado-condicion.component.html',
  styleUrls: ['./listado-condicion.component.scss']
})
export class ListadoCondicionComponent implements OnInit {

  @Input() condiciones: any[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
