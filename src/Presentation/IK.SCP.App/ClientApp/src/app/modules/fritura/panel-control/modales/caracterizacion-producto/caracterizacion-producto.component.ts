import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { OrdenService } from 'src/app/services/fritura/orden.service';

@Component({
  selector: 'app-caracterizacion-producto',
  templateUrl: './caracterizacion-producto.component.html',
  styleUrls: ['./caracterizacion-producto.component.scss'],
})
export class CaracterizacionProductoComponent implements OnInit {
  form: FormGroup = this.fb.group({
    peso: [null, [Validators.required]],
    etapa: [null, [Validators.required]],
    observacion: [null],
    inspector: [null, [Validators.required]],
    defectos: this.fb.array([]),
  });

  constructor(
    private service: OrdenService,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.cargarData();
  }

  cargarData() {
    const articulo = this.config.data.articulo;
    this.service.getAllDefectoCaracterizacion(articulo).subscribe((resp) => {
      if (resp.success) {
        resp.data.forEach((defecto: any) => {
          const defectoForm = this.fb.group({
            id: [defecto.id],
            nombre: [defecto.nombre, [Validators.required]],
            valor: [{ value: defecto.valor ?? 0, disabled: defecto.esFormula }, [Validators.required]],
            porcentaje: [{ value: 0, disabled: true }, [Validators.required]],
          });
          defectoForm.get('valor')?.valueChanges.subscribe((valor) => {
            const peso = this.form.get('peso')?.value ?? 0;
            const porcentaje = (valor * 100) / peso;
            defectoForm.patchValue({ porcentaje });
          });
          this.defectos.push(defectoForm);
        });
      }
    });
  }

  get defectos() {
    return this.form.controls['defectos'] as FormArray;
  }

  cerrar() {
    this.ref.close();
  }

  guardar() {
    if (!this.form.valid) return;
    const data = this.form.getRawValue();
    this.ref.close(data);
  }
}
