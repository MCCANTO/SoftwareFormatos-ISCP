import { Envasadora } from "./envasado-data";
import { OrdenEnvasado } from "./orden";

export interface DataEnvasadoGranel {
    envasadoraId: number;
    orden: string;
}

export interface DataEnvasadoGranelStorage {
    envasadora: Envasadora;
    orden: OrdenEnvasado;
}