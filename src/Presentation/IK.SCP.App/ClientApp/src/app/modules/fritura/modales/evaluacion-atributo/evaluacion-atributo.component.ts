import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { GranelService } from 'src/app/services/envasado/granel.service';
import { EvaluacionAtributoService } from 'src/app/services/fritura/evaluacion-atributo.service';
import { MaestroService } from 'src/app/services/fritura/maestro.service';

@Component({
  selector: 'app-evaluacion-atributo',
  templateUrl: './evaluacion-atributo.component.html',
  styleUrls: ['./evaluacion-atributo.component.scss']
})
export class EvaluacionAtributoComponent implements OnInit {

  id!: number;
  esLectura!: boolean;

  panelista: any;
  panelistasTotal: any[] = [];
  panelistas: any[] = [];

  panelistasSelected: any[] = [];
  apariencia!: number;
  color!: number;
  olor!: number;
  sabor!: number;
  textura!: number;
  calificacion!: number;
  observacion: string = '';

  esPT: boolean = false;

  calificaciones: any[] = [
    { valor: 1 },
    { valor: 2 },
    { valor: 3 },
    { valor: 4 },
    { valor: 5 },
  ];

  constructor(
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig,
    private maestroService : MaestroService,
    private evaluacionAtributoService: EvaluacionAtributoService,
    private granelService: GranelService,
  ) { 

    this.id = this.config.data.id;
    this.esPT = this.config.data.esPT ?? false;
    this.esLectura = (this.id ? true : false);
  }

  get colorStyle(): string {

    let color = '';

    if ( this.calificacion <=2 )
      color = '#EF7564';
    else if ( this.calificacion <=3 )
      color = '#F5DD29';
    else if ( this.calificacion <= 5)
      color = '#61BD4F';
    else 
      color = '';

    return color;
  }

  ngOnInit(): void {

    this.maestroService.getAllPanelista()
      .subscribe( resp => {
        if( resp.ok ){
          this.panelistas = resp.data;
          this.panelistasTotal = resp.data;
          if(this.id > 0) this.cargarData();
        }
      })

  }

  cargarData() {

    if(this.esPT) {
      this.granelService.getGranelEvaluacionPT(this.id)
        .subscribe((resp: any) => {
          if (resp.success) {
            this.panelistasSelected = resp.data.panelistas;
            this.apariencia = resp.data.aparienciaGeneral;
            this.color = resp.data.color;
            this.olor = resp.data.olor;
            this.sabor = resp.data.sabor;
            this.textura = resp.data.textura;
            this.calificacion = resp.data.calificacionFinal;
            this.observacion = resp.data.observacion;
          }
        });
    } else {

      this.evaluacionAtributoService.getById(this.id)
        .subscribe((resp: any) => {
          if (resp.ok) {
            this.panelistasSelected = resp.data.panelistas;
            this.apariencia = resp.data.aparienciaGeneral;
            this.color = resp.data.color;
            this.olor = resp.data.olor;
            this.sabor = resp.data.sabor;
            this.textura = resp.data.textura;
            this.calificacion = resp.data.calificacionFinal;
            this.observacion = resp.data.observacion;
          }
        });

    }
  }

  agregarPanelista() {
    this.panelistasSelected.push(this.panelista);
    this.panelistas = [...this.panelistas.filter(f => f.panelistaId != this.panelista.panelistaId)]
    this.panelista = null;
  }

  eliminarPanelista( panelista: any ) {
    const fila = this.panelistasSelected.filter(f => f.panelistaId === panelista.panelistaId).length;
    if (fila > 0) {
      this.panelistasSelected = this.panelistasSelected.filter(f => f.panelistaId != panelista.panelistaId);
      this.panelistas.push(panelista);
      this.panelistas = [...this.panelistas.sort((a,b) => (a.nombre > b.nombre) ? 1 : ((b.nombre > a.nombre) ? -1 : 0))];
    }
  }

  calcularCalificacion() {
    const arr = [
      this.apariencia,
      this.color,
      this.olor,
      this.sabor,
      this.textura,
    ];
    this.calificacion = Math.min(...arr);
  }

  cerrar()
  {
    this.ref.close();
  }

  guardar(){

    const ids = this.panelistasSelected.map( m =>  m.panelistaId );
    const ids_p = ids.join(',');
   
    this.ref.close({
      panelistas: ids_p,
      aparienciaGeneral: this.apariencia,
      color: this.color,
      olor: this.olor,
      sabor: this.sabor,
      textura: this.textura,
      calificacionFinal: this.calificacion,
      observacion: this.observacion,
    });
    
  }
}
