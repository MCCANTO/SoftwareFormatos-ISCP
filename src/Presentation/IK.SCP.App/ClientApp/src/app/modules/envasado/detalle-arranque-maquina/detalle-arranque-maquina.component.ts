import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DialogService } from 'primeng/dynamicdialog';
import { CondicionComponent } from 'src/app/components/condicion/condicion.component';
import { VariableBasicaComponent } from 'src/app/components/variable-basica/variable-basica.component';
import { LocalStorageConstant } from 'src/app/core/constants/local-storage.constant';
import { EnvasadoCondicionPrevia } from 'src/app/core/models/envasado/condicion-previa.model';
import { OrdenEnvasado } from 'src/app/core/models/envasado/orden';
import { ArranqueMaquinaService } from 'src/app/services/envasado/arranque-maquina.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-detalle-arranque-maquina',
  templateUrl: './detalle-arranque-maquina.component.html',
  styleUrls: ['./detalle-arranque-maquina.component.scss'],
  providers: [DialogService],
})
export class DetalleArranqueMaquinaComponent implements OnInit {
  id: number = 0;

  esCerrado: boolean = false;

  creador: string = '';

  condiciones: any[] = [];

  variables: any[] = [];

  observaciones: any[] = [];
  observacion = '';

  form!: FormGroup;

  dataEnvasadora!: any;

  dataOrden!: OrdenEnvasado;

  constructor(
    public dialogService: DialogService,
    public jwtTokenService: JwtTokenService,
    private arranqueMaquinaService: ArranqueMaquinaService,
    private router: Router,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private localStorageService: LocalStorageService,
    private activatedRoute: ActivatedRoute
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.createForm();
  }

  get EsMaquinista(): boolean {
    return this.jwtTokenService.esMaquinistaEnv();
  }

  get EsCreador(): boolean {
    return this.jwtTokenService.getUser() === this.creador;
  }

  get PuedeModificar(): boolean {
    if (this.esCerrado) return false;
    else if (this.id === 0 && this.EsMaquinista) return true;
    else if (this.EsMaquinista && this.EsCreador) return true;
    else return false;
  }

  createForm() {
    this.form = this.fb.group({
      pesoSobreProducto1: [{ value: '', disabled: true }],
      pesoSobreProducto2: [{ value: '', disabled: true }],
      pesoSobreProducto3: [{ value: '', disabled: true }],
      pesoSobreProducto4: [{ value: '', disabled: true }],
      pesoSobreProducto5: [{ value: '', disabled: true }],
      pesoSobreProductoProm: [{ value: '', disabled: true }],
      pesoSobreVacio: [{ value: '', disabled: true }],
    });

    this.PesoSobre1?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre2?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre3?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre4?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
    this.PesoSobre5?.valueChanges.subscribe((v) => {
      this.calcularPromedio();
    });
  }

  get PesoSobre1() {
    return this.form.get('pesoSobreProducto1');
  }

  get PesoSobre2() {
    return this.form.get('pesoSobreProducto2');
  }

  get PesoSobre3() {
    return this.form.get('pesoSobreProducto3');
  }

  get PesoSobre4() {
    return this.form.get('pesoSobreProducto4');
  }

  get PesoSobre5() {
    return this.form.get('pesoSobreProducto5');
  }

  get Promedio() {
    return this.form.get('pesoSobreProductoProm');
  }

  calcularPromedio() {
    let contador = 0;
    let total = 0;

    if (this.PesoSobre1?.value > 0) {
      total += this.PesoSobre1?.value;
      contador++;
    }

    if (this.PesoSobre2?.value > 0) {
      total += this.PesoSobre2?.value;
      contador++;
    }

    if (this.PesoSobre3?.value > 0) {
      total += this.PesoSobre3?.value;
      contador++;
    }

    if (this.PesoSobre4?.value > 0) {
      total += this.PesoSobre4?.value;
      contador++;
    }

    if (this.PesoSobre5?.value > 0) {
      total += this.PesoSobre5?.value;
      contador++;
    }

    const promedio = contador > 0 ? total / contador : 0;

    this.Promedio?.setValue(promedio);
  }

  ngOnInit(): void {
    this.cargarDataEnvasado();
    this.cargarData();
  }

  cargarDataEnvasado() {
    const data = JSON.parse(
      this.localStorageService.get(LocalStorageConstant.DATA_ORDEN_ENVASADO) ??
      ''
    );

    this.dataEnvasadora = data.envasadora;
    this.dataOrden = data.orden;
  }

  cargarData() {
    this.spinner.show();
    this.arranqueMaquinaService.getById(this.id).subscribe((resp: any) => {
      if (resp.data) {
        const arranque = resp.data;

        this.id = arranque.arranqueMaquinaId;
        this.creador = arranque.usuarioCreacion;
        this.esCerrado = arranque.cerrado;

        this.form.patchValue({
          pesoSobreProducto1: arranque.pesoSobreProducto1,
          pesoSobreProducto2: arranque.pesoSobreProducto2,
          pesoSobreProducto3: arranque.pesoSobreProducto3,
          pesoSobreProducto4: arranque.pesoSobreProducto4,
          pesoSobreProducto5: arranque.pesoSobreProducto5,
          pesoSobreProductoProm: arranque.pesoSobreProductoProm,
          pesoSobreVacio: arranque.pesoSobreVacio,
        });

        this.observaciones = arranque.observaciones;

        this.condiciones = arranque.condiciones;

        this.variables = arranque.variables;
      }
      this.spinner.hide();
    });
  }

  goTo(index: number) {
    if (index === 0) {
      this.router.navigate(['/envasado']);
    }

    if (index === 1) {
    }

    if (index === 2) {
    }
  }

  cargarCondicionesPrevias(id: number) {
    this.arranqueMaquinaService
      .getAllCondicionesById(id)
      .subscribe((resp: any) => {
        const condicion: EnvasadoCondicionPrevia = {
          editable: false,
          mostrarTipo: true,
          tipo: resp.data.tipoId,
          condiciones: resp.data.items,
          tipos: [],
        };

        const ref = this.dialogService.open(CondicionComponent, {
          header: 'Condiciones Previas',
          width: '85%',
          data: {
            condicion,
          },
        });
      });
  }

  cargarVariablesBasicas(id: number, cerrado: boolean = false) {
    this.arranqueMaquinaService
      .getAllVariablesById(id)
      .subscribe((resp: any) => {
        const variables = resp.data;

        const vb_ref = this.dialogService.open(VariableBasicaComponent, {
          header: 'Variables Básicas',
          width: '95%',
          data: {
            mostrarTipo: false,
            editable: false,
            cerrado: cerrado,
            variables,
          },
        });
      });
  }
}
