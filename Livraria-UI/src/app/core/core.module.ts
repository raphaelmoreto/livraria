import { AuthInterceptor } from './interceptors/auth.interceptor';

//"Optional" e "SkipSelf" SÃO USADOS PARA GARANTIR QUE O "CoreModule" NÃO SEJA IMPORTADO MAIS DE UMA VEZ
import { NgModule, Optional, SkipSelf } from '@angular/core';

//HABILITA REQUISIÇÕES HTTP NA APLICAÇÃO
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
    imports: [
        HttpClientModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ]
})

export class CoreModule {
    constructor (@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule deve ser importado apenas no AppModule');
        }
    }
}