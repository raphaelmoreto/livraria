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
    lstLivros$!: Observable<ILivro[]>;

    constructor (private livroService: LivroService) { }

    buscarLivros(): void {
        this.lstLivros$ = this.livroService.listar();
        // .pipe(
        //     tap(response => {
        //         console.log(response)
        //     })
        // );
    }

    ngOnInit(): void {
        this.buscarLivros();
    }

    trackByLivroId(index: number, livro: ILivro): number {
        return livro.id;
    }
}