

export interface EnvasadoCondicionPrevia {
  editable    : boolean;
  mostrarTipo : boolean;
  tipos       : EnvasadoTipoCondicionPrevia[];
  tipo        : number | null;
  condiciones : EnvasadoCondicionPreviaDetalle[];
}

export interface EnvasadoTipoCondicionPrevia {
  parametroGeneralId: number;
  nombre            : string;
}


export interface EnvasadoCondicionPreviaDetalle {
  id                : number;
  condicionPreviaId : number;
  nombre            : string;
  comentario        : string;
  valor             : string | boolean;
  observacion       : string;
}
