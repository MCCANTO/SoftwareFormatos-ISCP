
export interface ArranqueMaquina {
    id: number;
    data: any;
    condiciones: any[];
    variables: any[];
}

export interface PostArranqueMaquinaRequest {
    arranqueMaquinaId:     number;
    envasadoraId:          number;
    ordenId:               string;
    pesoSobreProducto1:    number | null;
    pesoSobreProducto2:    number | null;
    pesoSobreProducto3:    number | null;
    pesoSobreProducto4:    number | null;
    pesoSobreProducto5:    number | null;
    pesoSobreProductoProm: number | null;
    pesoSobreVacio:        number | null;
    observacion:           string;
}