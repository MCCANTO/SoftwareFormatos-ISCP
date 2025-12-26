

export interface Sazonador {
  sazonadorId: number;
  codigo: string;
  nombre: string;
}

export interface DataSazonadoStorage {
  sazonador: Sazonador;
}

export interface BandejaSazonado {
  arranqueId:       number;
  saborizador:      string;
  sabor:            string;
  saborDescripcion: string;
  fecha:            Date;
  linea:            string;
  producto:         string;
}

export interface Arranque {
  arranqueId:       number;
  saborId:          string;
  saborDescripcion: string;
  productos:        Producto[];
  verificaciones:   Verificacion[];
  condiciones:      Condicion[];
  observaciones:    Observacion[];
}

export interface Producto {
  linea:    string;
  orden:    string;
  producto: string;
}

export interface Condicion {
  arranqueCondicionPreviaId: number;
  orden:      number;
  nombre:     string;
  comentario: null;
  valor:      boolean;
}

export interface Verificacion {
  arranqueVerificacionEquipoId: number;
  usuarioCreacion:              string;
  fechaCreacion:                Date;
  cerrado:                      boolean;
}

export interface Observacion {
  usuario:      string;
  fecha:        Date;
  observacion:  string;
}
