import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from '@shared/pipes/pipes.module';

import { AppRoutingModule } from "src/app/app-routing.module";

import { AjudaComponent } from './ajuda/page/ajuda.component';
import { CardLivroComponent } from './livros/components/card-livro/card-livro.component';
import { ConfiguracoesComponent } from './configuracoes/page/configuracoes.component';
import { ComponentsModule } from '@shared/components/components.module';
import { FeedbackComponent } from './feedback/page/feedback.component';
import { HomeComponent } from './home/page/home.component';
import { LoadingComponent } from './loading/components/loading/loading.component';
import { ToastComponent } from './toast/components/toast/toast.component';

@NgModule({
    declarations: [
        AjudaComponent,
        CardLivroComponent,
        ConfiguracoesComponent,
        FeedbackComponent,
        HomeComponent,
        LoadingComponent,
        ToastComponent
    ],
    imports: [
        CommonModule,
        ComponentsModule,
        PipesModule,
        AppRoutingModule
    ],
    exports: [
        CardLivroComponent,
        HomeComponent,
        LoadingComponent,
        ToastComponent
    ]
})

export class FeaturesModule { }