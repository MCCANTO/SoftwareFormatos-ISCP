export interface ArranqueMaquinaVerificacionEquipo {
    arranqueMaquinaVerificacionEquipoCabId: number;
    arranqueMaquinaId:                      number;
    verificaciones:                         VerificacionEquipo[];
}

export interface VerificacionEquipo {
    arranqueMaquinaVerificacionEquipoId: number;
    verificacionEquipoId:                number;
    operativo:                           string;
    limpio:                              string;
    observacion:                         string;
}
