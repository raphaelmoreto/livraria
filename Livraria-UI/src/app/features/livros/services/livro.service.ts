import { ApiService } from 'src/app/core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILivro } from '../models/livro.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class LivroService extends ApiService<ILivro> {
    constructor(http: HttpClient) {
        super(http, 'livro');
    }

    listar(): Observable<ILivro[]> {
        return this.get<ILivro[]>('');
    }
}
