import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-pagina-erro',
    templateUrl: './pagina-erro.component.html',
    styleUrls: ['./pagina-erro.component.scss']
})

export class PaginaErroComponent {
    @Input({ required : true }) codigoErro!: number;
    @Input({ required : true }) mensagemErro!: string;
    @Input({ required : true }) mensagemDescricao!: string;
}