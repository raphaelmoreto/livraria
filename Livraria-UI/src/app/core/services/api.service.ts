import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root' //O SERVIÇO FICA DISPONÍVEL NA APLICAÇÃO INTEIRA
})

/*
"pipe", "map" e "subscribe" FAZEM PARTE DO FLUXO DE TRATAMENTO DE DADOS ASSÍNCRONOS RETORNADOS PELA API (Observables):
    • pipe: É COMO SE FOSSE UM TUBO ONDE SE PASSA OS DADOS. SERVE PARA VOCÊ MODIFICAR OS DADOS ANTES DE RECEBER.
    • map: FICA DENTRO DO "pipe". É ELE QUEM ALTERA/MODIFICA OS DADOS
    • subscribe: É QUANDO VOCÊ RECEBE O RETORNO
*/

export abstract class ApiService<T> extends BaseService {
    constructor (http: HttpClient, route: string) {
        super (http, route);
    }

    delete<T> (endpoint: string) {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
    }

    get<T> (endpoint: string) {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
    }

    post<T> (endpoint: string, body:any) {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
    }

    put<T> (endpoint: string, body: any) {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body);
    }
}