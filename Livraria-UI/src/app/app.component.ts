import { Component, OnInit } from '@angular/core';
import { ILivro } from './features/livros/models/livro.model';
import { LivroService } from './features/livros/services/livro.service';
import { Observable, tap } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
    //INDICA QUE NÃO HÁ UMA LISTA AGORA, MAS IRÁ RECEBER A LISTA NO FUTURO
    lstLivros$!: Observable<ILivro[]>;

    constructor (private livroService: LivroService) { }

    ngOnInit(): void {
        this.buscarLivros();
    }

    buscarLivros(): void {
        this.lstLivros$ = this.livroService.listar()
        // .pipe(
        //     tap(response => {
        //         console.log(response)
        //     })
        // );
    }

    trackByLivroId(index: number, livro: ILivro): number {
        return livro.id;
    }
}