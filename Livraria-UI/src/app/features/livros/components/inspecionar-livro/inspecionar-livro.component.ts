import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ILivro } from '@features/livros/models/livro.model';
import { LivroService } from '@features/livros/services/livro.service';

@Component({
  selector: 'app-inspecionar-livro',
  templateUrl: './inspecionar-livro.component.html',
  styleUrls: ['./inspecionar-livro.component.scss']
})

export class InspecionarLivroComponent implements OnInit {
    livro: ILivro | null = null;

    //O "ActivatedRoute" SERVE PARA ACESSAR INFORMAÇÕES DA ROTA ATUAL. É COMO UM OBJETO QUE SABE TUDO SOBRE A URL
    constructor(private livroService: LivroService, private route: ActivatedRoute) {}

    buscarLivro(idLivro: number): void {
        this.livroService.buscarLivro(idLivro)
        .subscribe((res) => {
            this.livro = res;
        });
    }

    ngOnInit(): void {
        this.route.paramMap.subscribe(paramMap => {
            const idLivro = Number(paramMap.get('id'));
            this.buscarLivro(idLivro);
        });
    }
}