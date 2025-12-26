import { Component, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-card-button',
  templateUrl: './card-button.component.html',
  styleUrls: ['./card-button.component.scss']
})
export class CardButtonComponent implements OnInit {

  @Input() icono: string = '';
  @Input() texto: string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
