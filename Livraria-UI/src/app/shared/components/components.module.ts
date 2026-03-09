import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from "src/app/app-routing.module";
import { ButtonComponent } from './button/button.component';
import { LoginModalComponent } from "./login-modal/login-modal.component";
import { MenuLateralComponent } from "./menu-lateral/menu-lateral.component";
import { MenuPrincipalComponent } from "./menu-principal/menu-principal.component";
import { PaginaErroComponent } from './pagina-erro/pagina-erro.component';

@NgModule({
    declarations: [
        ButtonComponent,
        LoginModalComponent,
        MenuLateralComponent,
        MenuPrincipalComponent,
        PaginaErroComponent
    ],
    imports: [
        AppRoutingModule,
        CommonModule,
        ReactiveFormsModule
    ],
    exports: [
        ButtonComponent,
        LoginModalComponent,
        MenuLateralComponent,
        MenuPrincipalComponent,
        PaginaErroComponent
    ]
})

export class ComponentsModule { }