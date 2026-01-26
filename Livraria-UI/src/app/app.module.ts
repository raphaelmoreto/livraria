import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentModule } from './shared/components/components.module';
import { CoreModule } from './core/core.module';
import { MenuLateralModule } from "./shared/components/menu-lateral/menu-lateral.module";
import { MenuPrincipalModule } from "./shared/components/menu-principal/menu-principal.module";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ComponentModule,
    CoreModule,
    MenuLateralModule,
    MenuPrincipalModule
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
