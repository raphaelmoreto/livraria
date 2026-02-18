import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentsModule } from '@shared/components/components.module';
import { CoreModule } from '@core/core.module';
import { FeaturesModule } from "@features/features.module";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ComponentsModule,
    CoreModule,
    FeaturesModule
],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }