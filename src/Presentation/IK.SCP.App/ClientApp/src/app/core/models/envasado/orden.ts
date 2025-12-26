export interface OrdenEnvasado {
    Orden:          string;
    FechaEjecucion: Date;
    FechaInicio:    Date;
    UsuarioInicio:  string;
    FechaFin:       Date;
    UsuarioFin:     string;
    Articulo:       string;
    Descripcion:    string;
    Unidad:         string;
    Cantidad:       number;
    CreadoPor:      string;
    FechaCreacion:  Date;
    Estado:         number;
}
