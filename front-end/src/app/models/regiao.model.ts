import { Cidade } from "./cidade.model";

export class Regiao {
    id: string;
    nome: string;
    cidades: Cidade[]; 
    ativa: boolean;
}