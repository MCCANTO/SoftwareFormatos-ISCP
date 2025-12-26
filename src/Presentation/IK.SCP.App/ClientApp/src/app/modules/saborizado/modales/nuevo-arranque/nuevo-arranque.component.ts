import { SazonadorService } from './../../../../services/sazonado/sazonador.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { GetAllOrden } from 'src/app/core/models/fritura/data-orden.interface';
import { OrdenService } from 'src/app/services/fritura/orden.service';

@Component({
  selector: 'app-nuevo-arranque',
  templateUrl: './nuevo-arranque.component.html',
  styleUrls: ['./nuevo-arranque.component.scss']
})
export class NuevoArranqueComponent implements OnInit {

  sazonadorId: number = 0;

  form!: FormGroup;

  lineas: any[] = [];
  linea = null;
  productos = [];
  sabores: any[] = [];

  // sabor = null;
  // otro = '';

  // ordenes = [];
  // producto = null;

  esOtro = false;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private router: Router,
    private sazonadorService: SazonadorService,
    private ordenService: OrdenService,
    private fb: FormBuilder,
  ) {

    this.crearForm();

  }

  crearForm() {
    this.form = this.fb.group({
      ordenes: this.fb.array([], [Validators.required]),
      sabor: [{ value: null }],
      otro: ''
    });

    this.form.get('sabor')?.valueChanges.subscribe(val => {
      this.form.get('otro')?.setValue('');
      if (val != 'OTRO') {
        this.esOtro = false;
      } else {
        this.esOtro = true;
      }
    });
  }

  ngOnInit(): void {
    
    this.sazonadorId = this.config.data.sazonadorId;

    this.cargarLineasFR();

  }

  get ordenes() {
    return this.form.controls['ordenes'] as FormArray<FormGroup>;
  }

  public ordenField(data: GetAllOrden): string {
    return data.orden + ' | ' + data.producto;
  }

  agregarOrden() {

    if (!this.linea) return;

    const ordenForm = this.fb.group({
      linea: [{ value: this.linea, disabled: true }, Validators.required],
      orden: [null, Validators.required],
      producto: [{ value: null, disabled: true }],
    });

    this.ordenes.push(ordenForm);
    this.linea = null;
  }

  eliminarOrden(index: number) {
    this.ordenes.removeAt(index);
    this.obtenerSabores();
  }

  cargarLineasFR() {
    this.sazonadorService.getAllSazonadorFritura(this.sazonadorId)
      .subscribe(resp => {
        this.lineas = resp.data;
      });
  }

  buscarOrden(event: any, linea: number) {

    const valor = event.query;

    if (valor.length > 5) {

      this.ordenService.getAllOrden(linea, valor)
        .subscribe(resp => {
          this.productos = resp.data;
        });

    }

  }

  mostrarProducto(orden: any, ordenForm: FormGroup) {
    ordenForm.get('producto')?.setValue(orden.producto);
    this.obtenerSabores();
  }


  obtenerSabores() {

    this.sabores = [];
    let listaOrdenes: string[] = [];

    this.ordenes.controls.forEach(ctrl => {
      const ordenObj = ctrl.get('orden')?.value;
      listaOrdenes.push(ordenObj.orden);
    });

    if (listaOrdenes.length > 0) {
      const cadenaOrdenes = listaOrdenes.join(',');
      
      this.ordenService.getAllOrdenConsumo(cadenaOrdenes, 'SABOR')
        .subscribe(resp => {

          const sabores = [
            { articulo: 'OTRO', descripcion: 'OTRO', nombre: 'OTRO' },
          ];

          resp.data.forEach( (p: any) => {
            sabores.push({ articulo: p.articulo, descripcion: p.descripcion, nombre: `${p.articulo} - ${p.descripcion}` });
          });
          
          this.sabores = [...sabores];

        });
    }


  }

  cerrar() {
    this.ref.close();
  }

  guardar() {

    if (!this.form.valid) {
      return;
    }

    const data = this.form.value;

    let ordenes : any[] = [];

    data.ordenes.forEach(( item: any ) => {
      const { orden, lineaProduccionId, producto } = item.orden;
      ordenes.push({ orden, lineaProduccionId, producto });
    });

    const saborSeleccionado = this.sabores.filter(f => f.articulo == data.sabor)[0]

    const dataInsert = {
      sazonadorId: this.sazonadorId,
      sabor: saborSeleccionado.articulo,
      otro: saborSeleccionado.articulo == 'OTRO' ? data.otro : saborSeleccionado.descripcion,
      ordenes
    }

    this.sazonadorService.insertArranque(dataInsert)
      .subscribe(resp => {

        if ( resp.success ) {
          this.ref.close();
        }

      });

  }

}
