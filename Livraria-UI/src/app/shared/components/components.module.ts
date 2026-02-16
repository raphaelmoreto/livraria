import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FeaturesModule } from "src/app/features/features.module";

import { MenuLateralComponent } from "./menu-lateral/menu-lateral.component";
import { MenuPrincipalComponent } from "./menu-principal/menu-principal.component";

@NgModule({
    declarations: [
        MenuLateralComponent,
        MenuPrincipalComponent
    ],
    imports: [
        CommonModule,
        FeaturesModule
    ],
    exports: [
        MenuLateralComponent,
        MenuPrincipalComponent
    ]
})

export class ComponentsModule { }