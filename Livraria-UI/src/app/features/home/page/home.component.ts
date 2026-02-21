import { Component, OnInit } from '@angular/core';
import { ILivro } from '@features/livros/models/livro.model';
import { LivroService } from '@features/livros/services/livro.service';
import { Observable, tap } from 'rxjs';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})

export class HomeComponent implements OnInit {
    //INDICA QUE NÃO HÁ UMA LISTA AGORA, MAS IRÁ RECEBER A LISTA NO FUTURO
    carregando: boolean = false;
    // lstLivros$!: Observable<ILivro[]>;
    lstLivros: ILivro[] = [];
    page: number = 1;
    pageSize: number = 10;

    constructor (private livroService: LivroService) { }

    // buscarLivros(): void {
    //     this.lstLivros$ = this.livroService.listar();
    //     .pipe(
    //         tap(response => {
    //             console.log(response)
    //         })
    //     );
    // }

    buscaPorPaginacao(): void {
        if (this.carregando)
            return;

        this.carregando = true;

        this.livroService.buscaPorPaginacao(this.page, this.pageSize)
        .subscribe((res) => {
            this.lstLivros = [...this.lstLivros, ...res];
            this.carregando = false;
        });
    }

    carregarMais(): void {
        this.page++;
        this.buscaPorPaginacao();
    }

    ngOnInit(): void {
        // this.buscarLivros();
        this.buscaPorPaginacao();
    }

    trackByLivroId(index: number, livro: ILivro): number {
        return livro.id;
    }
}