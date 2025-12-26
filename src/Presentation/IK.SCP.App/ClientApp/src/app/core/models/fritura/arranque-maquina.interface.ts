export interface ArranqueMaquina {
    arranqueMaquinaId: number;
    cerrado:           boolean;
    revisado:          boolean;
    condiciones:       Condiciones[];
    verificaciones:    Verificaciones[];
    observaciones:     any[];
}

export interface Condiciones {
    arranqueMaquinaCondicionPreviaId: number;
    condicionPreviaId:                number;
    nombre:                           string;
    comentario:                       string;
    valor:                            boolean;
    observacion:                      string;
    orden:                            number;
}

export interface Verificaciones {
    arranqueMaquinaVerificacionEquipoCabId: number;
    usuario:                            string;
    fecha:                              Date;
}


export interface VerificacionEquipo {
    categoria:      string;
    verificaciones: VerificacionEquipoDetalle[];
}

export interface VerificacionEquipoDetalle {
    id:                   number;
    verificacionEquipoId: number;
    nombre:               string;
    detalle:              string;
    operativo:            string;
    limpio:               string;
    observacion:          string;
    orden:                number;
}



export interface CondicionPrevia {
    arranqueMaquinaId: number;
    condiciones:       Condicion[];
}

export interface Condicion {
    arranqueMaquinaCondicionPreviaId: number;
    condicionPreviaId:                number;
    valor:                            boolean;
    observacion:                      string;
}