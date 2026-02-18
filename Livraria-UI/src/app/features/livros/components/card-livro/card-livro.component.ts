import { Component, Input } from '@angular/core';
import { ILivro } from '@features/livros/models/livro.model';

@Component({
  selector: 'app-card-livro',
  templateUrl: './card-livro.component.html',
  styleUrls: ['./card-livro.component.scss']
})

export class CardLivroComponent {
    @Input({ required : true }) livro!: ILivro;
}