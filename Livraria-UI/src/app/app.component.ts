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
    modalLogin: boolean = false;

    constructor (private livroService: LivroService) { }

    abrirModalLogin() {
        this.modalLogin = true;
    }

    buscarLivros(): void {
        this.lstLivros$ = this.livroService.listar()
        // .pipe(
        //     tap(response => {
        //         console.log(response)
        //     })
        // );
    }

    fecharModalLogin() {
        this.modalLogin = false;
    }

    ngOnInit(): void {
        this.buscarLivros();
    }

    trackByLivroId(index: number, livro: ILivro): number {
        return livro.id;
    }
}