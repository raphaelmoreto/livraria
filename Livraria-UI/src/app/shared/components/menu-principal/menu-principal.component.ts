import { ApiService } from '../../../core/services/api.service';
import { Component } from '@angular/core';
import { IUsuarioInput } from 'src/app/core/models/usuario-input.model';

@Component({
  selector: 'app-menu-principal',
  templateUrl: './menu-principal.component.html',
  styleUrls: ['./menu-principal.component.scss']
})

export class MenuPrincipalComponent {
    modalLogin: boolean = false;

    constructor () { }

    abrirModalLogin(): void {
        this.modalLogin = true;
    }

    fecharModalLogin(): void {
        this.modalLogin = false;
    }

    cadastro(usuario: IUsuarioInput): void {
        // this.apiService.post<IUsuarioInput>('/usuario', usuario);
    }
}