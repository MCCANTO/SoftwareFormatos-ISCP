import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';


interface IPersona {
  cargoId: string;
  nombre: string;
}


@Component({
  selector: 'app-personal',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss']
})
export class PersonalComponent implements OnInit {

  cargo = '';
  nombre = '';

  cargos = [
    { id: 1 , nombre : 'EMPACADOR'},
    { id: 2 , nombre : 'PALETIZADOR'}
  ]

  personal: IPersona[] = [];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
  }

  agregar() {
    
    if (this.cargo && this.nombre) {      

      const persona: IPersona = {
        cargoId: this.cargo,
        nombre: this.nombre
      }

      this.personal.push(persona)
      this.cargo = '';
      this.nombre = '';
    }


  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    
    const data = {
      personal: this.personal
    }

    this.ref.close(data);
  }
}
