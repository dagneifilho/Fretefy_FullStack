import { Component, OnInit } from '@angular/core';
import { RegiaoInsert } from '../models/regiao-insert.model';
import { RegiaoUpdate } from '../models/regiao-update.model';
import { RegiaoService } from '../services/regiao.service';
import { CidadeService } from '../services/cidade.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Cidade } from '../models/cidade.model';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Regiao } from '../models/regiao.model';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-cadastro-regiao',
  templateUrl: './cadastro-regiao.component.html',
  styleUrls: ['./cadastro-regiao.component.scss']
})
export class CadastroRegiaoComponent implements OnInit {

  regiaoCadastro: any = { Id: null, Nome: '', Cidades: [] };
  cidadesDisponiveis : Cidade[] = [];
  id: string;
  form : FormGroup;
  regiao : Regiao;
  routeSub: Subscription;


  
  constructor(private regiaoService : RegiaoService, 
                private cidadeService: CidadeService,
                private toastrService: ToastrService,
                public router: Router,
                private builder: FormBuilder,
                private route: ActivatedRoute
              ) { }

  async ngOnInit(): Promise<void> {
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id']; 
    });

    this.cidadesDisponiveis = await this.cidadeService.getCidadesDisponiveis().toPromise();

    if (this.id) {
      await this.carregarRegiao();
    }

    this.criaFormulario();
  }

  private criarGrupoCidade(cidade?: Cidade): FormGroup {
    return this.builder.group({
      cidadeId: [cidade?.id || '', Validators.required]
    });
  }
  
  criaFormulario(): void {
    const nome = this.regiao ? this.regiao.nome : '';
    if (this.regiao)
    {
      this.regiao.cidades.forEach(cidade => {
        this.cidadesDisponiveis.push(cidade);
      });
    }
    const cidadesArray = this.regiao?.cidades 
      ? this.regiao.cidades.map(cidade => this.builder.group({
          cidadeId: [cidade.id, Validators.required]
        })) 
      : [this.builder.group({ cidadeId: ['', Validators.required] })];
  
    this.form = this.builder.group({
      nome: [nome, Validators.required],
      cidades: this.builder.array(cidadesArray)
    });
  }
  
 

  async carregarRegiao(): Promise<void> {
    this.regiao = await this.regiaoService.getRegiao(this.id).toPromise();
  }


  get cidades(): FormArray {
    return this.form.get('cidades') as FormArray;
  }

  adicionarCidade() {
    this.cidades.push(this.builder.group({
      cidadeId: new FormControl('', [Validators.required]) 
    }));
  }
  
  removerCidade(index: number) {
    this.cidades.removeAt(index);
  }

  get cidadesFormArray() : FormArray{
    return (this.form.get('cidades') as FormArray);
  }
  private tratarErroValidacao(erroResponse: any): void {
    if (erroResponse?.error?.erros) {
      erroResponse.error.erros.forEach((erro: { campo: string, erro: string }) => {
        this.toastrService.error(erro.erro, 'Error', {
          timeOut: 3000,
          progressBar: true,
          positionClass: 'toast-top-right',
          closeButton: true,
        });
      });
    } else {
      this.toastrService.error('Ocorreu um erro inesperado.', 'Error', {
        timeOut: 3000,
        progressBar: true,
        positionClass: 'toast-top-right',
        closeButton: true,
      });
    }
  }


  onSubmit() :void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toastrService.error('Preencha todos os campos obrigatórios.', 'Error', {
        timeOut: 3000,
        progressBar: true,
        positionClass: 'toast-top-right',
        closeButton: true,
      });
      return;
    }

    const {nome, cidades} = this.form.value;
    const cidadesIds = cidades.map(c => c.cidadeId);

    if(this.id)
    {
      const regiaoUpdate : RegiaoUpdate = new RegiaoUpdate(this.id, nome, cidadesIds);
      this.regiaoService.update(regiaoUpdate).subscribe({
        next: (data) => {
          this.toastrService.success('Região atualizada com sucesso!');
          this.router.navigate(['/regiao']);
          return;
        }, 
        error: (err) => {
          this.tratarErroValidacao(err);
        }
      })
    }
    else {
      const regiaoInsert: RegiaoInsert = new RegiaoInsert(nome, cidadesIds);
      this.regiaoService.insert(regiaoInsert).subscribe({
        next: (data) => {
          this.toastrService.success('Região criada com sucesso!');
          this.router.navigate(['/regiao']);
          return;
        }, 
        error: (err) => {
          this.tratarErroValidacao(err);
        }
      });
    }
  }
}

