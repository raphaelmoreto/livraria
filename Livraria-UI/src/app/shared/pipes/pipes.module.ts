import { DashIfEmptyPipe } from './dash-if-empty.pipe';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [
        DashIfEmptyPipe
    ],
    exports: [
        DashIfEmptyPipe
    ]
})

export class PipesModule { }