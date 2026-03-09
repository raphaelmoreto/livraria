import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AjudaComponent } from '@features/ajuda/page/ajuda.component';
import { authGuard } from '@core/guards/auth.guard';
import { ConfiguracoesComponent } /* ↓ */
    from '@features/configuracoes/page/configuracoes.component';
import { FeedbackComponent } from '@features/feedback/page/feedback.component';
import { HomeComponent } from '@features/home/page/home.component';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    { 
        path: 'ajuda',
        component: AjudaComponent,
        canActivate: [authGuard]
    },
    { 
        path: 'configuracoes',
        component: ConfiguracoesComponent,
        canActivate: [authGuard]
    },
    {
        path: 'feedback',
        component: FeedbackComponent,
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