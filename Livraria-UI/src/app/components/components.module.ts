import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { LoginModule } from "./login/login.module";
import { MenuLateralModule } from "./menu-lateral/menu-lateral.module";
import { MenuPrincipalModule } from "./menu-principal/menu-principal.module";

@NgModule({
    imports: [
        CommonModule,
        LoginModule,
        MenuLateralModule,
        MenuPrincipalModule,
    ],
    declarations: [ ],
})

export class ComponentModule { }