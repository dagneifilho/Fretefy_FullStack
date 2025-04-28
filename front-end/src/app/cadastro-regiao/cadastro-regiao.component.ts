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
                private router: Router,
                private builder: FormBuilder,
                private route: ActivatedRoute
              ) { }

  ngOnInit(): void {
    console.log(this.route.snapshot.params);
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id'];  // Pegando o parâmetro 'id'
    });
    console.log(this.id);

    if(this.id)
    {
      this.carregarRegiao();
    }

    this.cidadeService.getCidadesDisponiveis().subscribe({
      next: (data) => {
        this.cidadesDisponiveis = data;
      },
      error: (err) => {
        console.error(err);
      }
    });

    let formArray :FormArray;
    console.log(this.regiao);

    if(this.regiao)
    {
      formArray = this.builder.array(this.regiao.cidades.map((element) => {
        return this.builder.group({
          cidadeId: new FormControl(element.id, [Validators.required]),
          nomeCidade: new FormControl(element.nome),
          ufCidade: new FormControl(element.uf)
        });
      }));
      console.log(formArray);
    } else {
      formArray = this.builder.array([
        this.builder.group({
          cidadeId: new FormControl('', [Validators.required]),
          nomeCidade: new FormControl(''),
          ufCidade: new FormControl('')
        })
      ]);
      console.log('veio aqui pra baixo');
    }
    this.form = this.builder.group({
      nome: new FormControl(this.regiao.nome ? this.regiao.nome : '', [Validators.required]),
      cidades: formArray
    });
    console.log(this.form)
  }


  carregarRegiao(): void {
    
    
  }
  
  // get cidadesFormArray() : FormArray{
  //   return (this.regiaoForm.get('Cidades') as FormArray);
  // }

  // adicionarCidade(cidade = null): void {
  //   const cidadeFormGroup = this.builder.group({
  //     cidadeId: [cidade ? cidade.id : null, Validators.required]
  //   });
  //   this.cidadesFormArray.push(cidadeFormGroup);
  // }
  // removerCidade(index: number): void {
  //   this.cidadesFormArray.removeAt(index);
  // }

  // salvar(): void {

  //   if (this.regiaoForm.valid) {
  //     const regiaoData = this.regiaoForm.value;
  //     console.log(regiaoData);
  //     return;
  //   }
  //   // }
    // if(id){ 
    //   const regiaoUpdate: RegiaoUpdate = new RegiaoUpdate(id, nome, selecionados);
    //   this.regiaoService.update(regiaoUpdate).subscribe({
    //     next: (data) => {
    //       this.toastrService.success('Região atualizada com sucesso!');
    //       this.router.navigate(['/regiao']);
    //     },
    //     error: (err) => {
    //       console.error(err);
    //       this.tratarErroValidacao(err);
    //     }
    //   });
    //   return;
    // }
    // const regiaoInsert: RegiaoInsert = new RegiaoInsert(nome, selecionados);
    // this.regiaoService.insert(regiaoInsert).subscribe({
    //   next: (data) => {
        
    //     this.toastrService.success('Região criada com sucesso!');
    //     this.router.navigate(['/regiao']);
    //   },
    //   error: (err) => {
    //     console.error(err);
    //     this.tratarErroValidacao(err);
    //   }
    // })

    // console.log(regiaoInsert);
  // }
  // sair():void{
  //   this.router.navigate(['/regiao']);
  // }

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


}
