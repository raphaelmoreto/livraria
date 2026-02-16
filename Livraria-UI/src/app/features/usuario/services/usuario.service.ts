import { ApiService } from 'src/app/core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from 'src/app/core/models/apiResponse.model';
import { IUsuarioInput } from 'src/app/core/models/usuario-input.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class UsuarioService extends ApiService<IApiResponse> {
    constructor(http: HttpClient) {
        super (http, 'usuario');
    }

    criarConta(usuarioCadastro: IUsuarioInput): Observable<IApiResponse> {
        return this.post<IApiResponse>('', usuarioCadastro);
    }
}
