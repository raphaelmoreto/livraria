import { ApiService } from '@core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from '@core/models/apiResponse.model';
import { IUsuarioCadastro } from '../models/usuario-cadastro.model';
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
