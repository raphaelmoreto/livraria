import { Component, Input } from '@angular/core';
import { ILivro } from 'src/app/features/livros/models/livro.model';
import { LivroService } from './../../../features/livros/services/livro.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})

export class CardComponent {
    @Input({ required : true }) livro!: ILivro;
}