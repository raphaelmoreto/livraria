import { ApiService } from '@core/services/api.service';
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

    buscaPorPaginacao(page: number, pageSize: number = 10): Observable<ILivro[]> {
        return this.get<ILivro[]>(`busca-por-paginacao?page=${page}&pageSize=${pageSize}`);
    }

    listar(): Observable<ILivro[]> {
        return this.get<ILivro[]>('');
    }
}
