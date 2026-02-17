import { ApiService } from 'src/app/core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from 'src/app/core/models/apiResponse.model';
import { IUsuarioCadastro } from 'src/app/features/usuario/models/usuario-cadastro.model';
import { IUsuarioLogin } from 'src/app/features/usuario/models/usuario-login.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class UsuarioService extends ApiService<IApiResponse> {
    constructor(http: HttpClient) {
        super (http, 'usuario');
    }

    criarConta(usuarioCadastro: IUsuarioCadastro): Observable<IApiResponse> {
        return this.post<IApiResponse>('', usuarioCadastro);
    }
}
