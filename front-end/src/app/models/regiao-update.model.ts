export class RegiaoUpdate {
    constructor(id: string, nome :string, cidadesIds:string[]) {
        this.id = id;
        this.cidadesIds = cidadesIds;
        this.nome = nome;
    }
    id: string;
    nome: string;
    cidadesIds: string[];
}