export class RegiaoInsert {
  
    constructor(nome :string, cidadesIds:string[]) {
        this.cidadesIds = cidadesIds;
        this.nome = nome;
    }
    nome: string;
    cidadesIds: string[];
}