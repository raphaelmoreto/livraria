import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUsuarioLogin } from '@features/usuario/models/usuario-login.model';
import { ILoginResponse } from '../models/auth.model';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ApiService } from './api.service';

@Injectable({
    providedIn: 'root'
})

export class AuthService extends ApiService<ILoginResponse> {
    //"BehaviorSubject" SEMPRE É COMO SE FOSSE UMA CAIXA QUE GUARDA A ÚLTIMA COISA QUE VOCÊ COLOCOU DENTRO DELA. SEMPRE QUE QUISER VER, ELE MOSTRA ESSA ÚLTIMA COISA
    //ILoginResponse['usuario'] SIGNIFICA O TIPO. PODE ARMAZENAR UM USUÁRIO OU PODE ESTAR VÁZIA
    private usuarioSubject = new BehaviorSubject<ILoginResponse['usuario'] | null>(null);

    //É COMO SE FOSSE UM "readonly" PARA OBSERVAR O QUE ESTÁ NA CAIXA NO MOMENTO ATUAL
    usuario$ = this.usuarioSubject.asObservable();

    constructor(http: HttpClient) {
        super (http, 'auth');

        const storedUser = localStorage.getItem('usuario');
        if (storedUser) {
            this.usuarioSubject.next(JSON.parse(storedUser));
        }
    }

    login(usuarioLogin: IUsuarioLogin): Observable<ILoginResponse> {
        return this.http.post<ILoginResponse>(`${this.baseUrl}/login`, usuarioLogin)
        .pipe(
            tap( (res) => {
                localStorage.setItem('token', res.token); //GUARDA O TOKEN DE LOGIN NO NAVEGADOR

                //GUARDA OS DADOS DO USUÁRIO (TRANSFORMANDOS EM STRING) NO NAVEGADOR
                localStorage.setItem('usuario', JSON.stringify(res.usuario));
                
                //AVISA PARA QUEM ESTÁ OLHANDO QUE ALGUÉM ESTÁ LOGADO
                this.usuarioSubject.next(res.usuario);
            })
        );
    }

    logout(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('usuario');
        this.usuarioSubject.next(null);
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }

    isLoggedIn(): boolean {
        //O "!!" JEITO RÁPIDO DE CONVERTER QUALQUER VALOR EM "true" OU "false"
        return !!this.getToken();
    }
}