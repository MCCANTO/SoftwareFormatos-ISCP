

export enum eMateriaPrima {
    MAIZ_GIGANTE = 1,
    PLATANO = 2,
    ZANAHORIA = 3,
    CAMOTE = 4,
    HABAS = 5,
    PAPA =  6,
}

export interface DataAcondicionamientoStorage {
    orden: string;
    fecha: Date;
    materiaPrima: MateriaPrima;
}

interface MateriaPrima {
    id: number;
    nombre: string;
}