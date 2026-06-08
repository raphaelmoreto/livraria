import { Component, OnInit } from '@angular/core';
import { ILivroAbreviado } from '@features/livros/models/livro.model';
import { LivroService } from '@features/livros/services/livro.service';
import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})

export class HomeComponent implements OnInit {
    //INDICA QUE NÃO HÁ UMA LISTA AGORA, MAS IRÁ RECEBER A LISTA NO FUTURO
    // lstLivros$!: Observable<ILivro[]>;
    carregando: boolean = false;
    lstLivros: ILivroAbreviado[] = [];
    page: number = 1;
    pageSize: number = 10;

    constructor (private livroService: LivroService, private router: Router) { }

    buscaPorPaginacao(): void {
        if (this.carregando)
            return;

        this.carregando = true;

        this.livroService.buscaAbreviadaPorPoginacao(this.page, this.pageSize)
        .subscribe((res) => {
            //O "..." É O "Spread Operator". ELE CRIA UMA NOVA LISTA NA MÉMORIA JUNTANDO AS DUAS INDICADAS
            this.lstLivros = [...this.lstLivros, ...res];
            this.carregando = false;
        });        
    }

    carregarMais(): void {
        this.page++;
        this.buscaPorPaginacao();
    }

    inspecionarLivro(livroId: number): void {
        this.router.navigate(['/livro', livroId]);
    }

    ngOnInit(): void {
        // this.buscarLivros();
        this.buscaPorPaginacao();
    }

    //O "trackBy" SERVE PARA AJUDAR O ANGULAR A IDENTIFICAR CADA ITEM DE UMA LISTA, EVITANDO QUE ELE RECRIE ELEMENTOS HTML DESNECESSARIAMENTE QUANDO A LISTA É ATUALIZADA
    trackByLivroId(index: number, livro: ILivroAbreviado): number {
        return livro.id;
    }
}