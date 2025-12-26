import { OrdenEnvasado } from "./orden";

export interface DataEnvasado {
    envasadoraId: number;
    orden: string;
}

export interface DataEnvasadoStorage {
    envasadora: Envasadora,
    orden: OrdenEnvasado;
} 

export interface Envasadora {
    Id: number;
    Nombre: string;
}