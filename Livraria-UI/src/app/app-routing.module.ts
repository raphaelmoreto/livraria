import { authGuard } from '@core/guards/auth.guard';
import { ConfiguracoesComponent } /* ↓ */
    from '@features/configuracoes/components/configuracoes/configuracoes.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from '@features/home/components/home/home.component';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    { 
        path: 'configuracoes',
        component: ConfiguracoesComponent,
        canActivate: [authGuard]
    },
    {
        path: '**', redirectTo: ''
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }