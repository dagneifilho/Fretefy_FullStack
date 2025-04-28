import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as bootstrap from 'bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Cidade } from 'src/app/models/cidade.model';
import { RegiaoInsert } from 'src/app/models/regiao-insert.model';
import { RegiaoListed } from 'src/app/models/regiao-listed.model';
import { RegiaoUpdate } from 'src/app/models/regiao-update.model';
import { Regiao } from 'src/app/models/regiao.model';
import { CidadeService } from 'src/app/services/cidade.service';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-regiao',
  templateUrl: './regiao.component.html',
  styleUrls: ['./regiao.component.scss']
})
export class RegiaoComponent implements OnInit {
  regioes$: Observable<RegiaoListed[]>;
  loading : boolean = true;
  regiaoVisualizada : Regiao|null = null;

  constructor(private regiaoService : RegiaoService, 
              private cidadeService: CidadeService,
              private toastrService: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadRegioes();
  }

  loadRegioes() : void{
    this.regioes$ =  this.regiaoService.getRegioes();
  }
  visualizar(id: string): void
  {
    this.regiaoService.getRegiao(id).subscribe({
      next:(data) => {
        this.regiaoVisualizada = data;
        const modalElement = document.getElementById('visualizarModal');
        const modal = new bootstrap.Modal(modalElement);

        modal.show();
      },
      error: (err) => {
        console.error(err)
      }
    })
  }
  

  cadastrarRegiao():void {
    this.router.navigate(['/cadastrar']);
  }
  editarRegiao(id: string){
    this.router.navigate([`editar/${id}`])
  }

  alternarAtivacao(id: string) : void{
    this.regiaoService.mudarStatus(id).subscribe({
      next: (data)=> {
        this.loadRegioes();
        this.toastrService.success('Status da região alterado com sucesso!');
      }, 
      error: (err) => {
        console.error(err);
      }
    });
  }

}
