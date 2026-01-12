import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentModule } from './components/components.module';
import { MenuLateralModule } from "./components/menu-lateral/menu-lateral.module";
import { MenuPrincipalModule } from "./components/menu-principal/menu-principal.module";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ComponentModule,
    MenuLateralModule,
    MenuPrincipalModule
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
