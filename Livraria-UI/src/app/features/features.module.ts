import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from '../shared/pipes/pipes.module';
import { ReactiveFormsModule } from '@angular/forms';

import { CardLivroComponent } from './livros/components/card-livro/card-livro.component';
import { LoginModalComponent } from './login-modal/login-modal.component';

@NgModule({
    declarations: [
        CardLivroComponent,
        LoginModalComponent
    ],
    imports: [
        CommonModule,
        PipesModule,
        ReactiveFormsModule
    ],
    exports: [
        CardLivroComponent,
        LoginModalComponent
    ]
})

export class FeaturesModule { }