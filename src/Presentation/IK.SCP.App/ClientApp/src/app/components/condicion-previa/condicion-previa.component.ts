import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-condicion-previa',
  templateUrl: './condicion-previa.component.html',
  styleUrls: ['./condicion-previa.component.scss']
})
export class CondicionPreviaComponent {
  @Input() condiciones: any;
  @Input() esEditable: boolean = false;
}
