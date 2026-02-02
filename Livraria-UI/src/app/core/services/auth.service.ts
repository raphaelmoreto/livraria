import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginResponse } from '../models/auth.model';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})

export class AuthService extends BaseService {
    //"BehaviorSubject" SEMPRE É COMO SE FOSSE UMA CAIXA QUE GUARDA A ÚLTIMA COISA QUE VOCÊ COLOCOU DENTRO DELA. SEMPRE QUE QUISER VER, ELE MOSTRA ESSA ÚLTIMA COISA
    //LoginResponse['usuario'] SIGNIFICA O TIPO. PODE ARMAZENAR UM USUÁRIO OU PODE ESTAR VÁZIA
    private usuarioSubject = new BehaviorSubject<LoginResponse['usuario'] | null>(null);

    //É COMO SE FOSSE UM "readonly" PARA OBSERVAR O QUE ESTÁ NA CAIXA NO MOMENTO ATUAL
    usuario$ = this.usuarioSubject.asObservable();

    constructor(http: HttpClient) {
        super (http, 'auth');

        const storedUser = localStorage.getItem('usuario');
        if (storedUser) {
            this.usuarioSubject.next(JSON.parse(storedUser));
        }
    }

    login(email: string, senha: string): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(`/login`, { email, senha })
            .pipe(
                tap(res => {
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