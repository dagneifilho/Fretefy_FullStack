import { RouterModule, Routes } from '@angular/router';
import { RegiaoComponent } from './regiao.component';
import { CadastroRegiaoComponent } from 'src/app/cadastro-regiao/cadastro-regiao.component';

const routes: Routes = [
  { 
    path: '',
    component: RegiaoComponent
  },
  {
    path:'editar/:id', 
    component: CadastroRegiaoComponent
  },
  {
    path:'cadastrar',
    component:CadastroRegiaoComponent
  }
];

export const  RegiaoRoutingModule = RouterModule.forChild(routes);