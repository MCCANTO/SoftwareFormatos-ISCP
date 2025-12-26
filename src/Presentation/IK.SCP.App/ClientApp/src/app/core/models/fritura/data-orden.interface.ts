

export interface OrdenFR {
    Orden: string;
    Articulo: string;
    Descripcion: string;
    Unidad: string;
    Cantidad: number;
    FechaEjecucion: Date;
    FechaInicio?: Date;
    UsuarioInicio?: string;
    FechaFin?: Date;
    UsuarioFin: Date;
}

export interface GetAllOrden {
  orden: string;
  lineaProduccionId: number;
  linea: string;
  producto: string;
}

export interface Freidora {
    Id: number;
    Nombre: string;
}

export interface DataFritura {
    freidoraId: number;
    orden: string;
}

export interface DataFrituraStorage {
    freidora?: Freidora;
    orden?: OrdenFR;
}
