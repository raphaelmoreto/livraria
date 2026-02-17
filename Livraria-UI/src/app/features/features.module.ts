import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipesModule } from '../shared/pipes/pipes.module';
import { ReactiveFormsModule } from '@angular/forms';

import { CardLivroComponent } from './livros/components/card-livro/card-livro.component';
import { LoginModalComponent } from './login-modal/login-modal.component';
import { ToastComponent } from './toast/components/toast/toast.component';

@NgModule({
    declarations: [
        CardLivroComponent,
        LoginModalComponent,
        ToastComponent
    ],
    imports: [
        CommonModule,
        PipesModule,
        ReactiveFormsModule
    ],
    exports: [
        CardLivroComponent,
        LoginModalComponent,
        ToastComponent
    ]
})

export class FeaturesModule { }