import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FeaturesModule } from "src/app/features/features.module";
import { ReactiveFormsModule } from '@angular/forms';

import { LoginModalComponent } from "./login-modal/login-modal.component";
import { MenuLateralComponent } from "./menu-lateral/menu-lateral.component";
import { MenuPrincipalComponent } from "./menu-principal/menu-principal.component";

@NgModule({
    declarations: [
        LoginModalComponent,
        MenuLateralComponent,
        MenuPrincipalComponent
    ],
    imports: [
        CommonModule,
        FeaturesModule,
        ReactiveFormsModule
    ],
    exports: [
        LoginModalComponent,
        MenuLateralComponent,
        MenuPrincipalComponent
    ]
})

export class ComponentsModule { }