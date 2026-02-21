import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from '@shared/pipes/pipes.module';

import { CardLivroComponent } from './livros/components/card-livro/card-livro.component';
import { ConfiguracoesComponent } from './configuracoes/page/configuracoes.component';
import { ComponentsModule } from '@shared/components/components.module';
import { HomeComponent } from './home/page/home.component';
import { ToastComponent } from './toast/components/toast/toast.component';

@NgModule({
    declarations: [
        CardLivroComponent,
        ConfiguracoesComponent,
        HomeComponent,
        ToastComponent,
    ],
    imports: [
        CommonModule,
        ComponentsModule,
        PipesModule,
    ],
    exports: [
        CardLivroComponent,
        HomeComponent,
        ToastComponent
    ]
})

export class FeaturesModule { }