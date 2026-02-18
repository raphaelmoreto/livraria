import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from '@shared/pipes/pipes.module';

import { CardLivroComponent } from './livros/components/card-livro/card-livro.component';
import { ToastComponent } from './toast/components/toast/toast.component';

@NgModule({
    declarations: [
        CardLivroComponent,
        ToastComponent
    ],
    imports: [
        CommonModule,
        PipesModule
    ],
    exports: [
        CardLivroComponent,
        ToastComponent
    ]
})

export class FeaturesModule { }