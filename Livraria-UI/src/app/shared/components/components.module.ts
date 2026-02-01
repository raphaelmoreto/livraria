import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { CardComponent } from './card/card.component';
import { LoginComponent } from "./login/login.component";
import { MenuLateralComponent } from "./menu-lateral/menu-lateral.component";
import { MenuPrincipalComponent } from "./menu-principal/menu-principal.component";
import { PipesModule } from "../pipes/pipes.module";

@NgModule({
    declarations: [
        CardComponent,
        LoginComponent,
        MenuLateralComponent,
        MenuPrincipalComponent
    ],
    imports: [
        CommonModule,
        PipesModule
    ],
    exports: [
        CardComponent,
        LoginComponent,
        MenuLateralComponent,
        MenuPrincipalComponent
    ]
})

export class ComponentsModule { }