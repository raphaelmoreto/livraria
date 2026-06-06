import { ApiService } from '@core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILivro, ILivroAbreviado } from '../models/livro.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class LivroService extends ApiService<ILivro> {
    constructor(http: HttpClient) {
        super(http, 'livro');
    }

    buscaAbreviadaPorPoginacao(page: number, pageSize: number = 10): Observable<ILivroAbreviado[]> {
        return this.get<ILivroAbreviado[]>(`busca/abreviada/paginacao?page=${page}&pageSize=${pageSize}`)
    }

    buscaPorPaginacao(page: number, pageSize: number = 10): Observable<ILivro[]> {
        return this.get<ILivro[]>(`busca/paginacao?page=${page}&pageSize=${pageSize}`);
    }

    listar(): Observable<ILivro[]> {
        return this.get<ILivro[]>('');
    }
}
