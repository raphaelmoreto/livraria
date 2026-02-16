import { Component } from '@angular/core';
import { IUsuarioInput } from 'src/app/core/models/usuario-input.model';
import { UsuarioService } from 'src/app/features/usuario/services/usuario.service';

@Component({
  selector: 'app-menu-principal',
  templateUrl: './menu-principal.component.html',
  styleUrls: ['./menu-principal.component.scss']
})

export class MenuPrincipalComponent {
    modalLogin: boolean = false;

    constructor (private usuarioService: UsuarioService) { }

    abrirModalLogin(): void {
        this.modalLogin = true;
    }

    fecharModalLogin(): void {
        this.modalLogin = false;
    }

    cadastro(usuarioCadastro: IUsuarioInput): void {
        this.usuarioService.criarConta(usuarioCadastro)
        .subscribe({
            next: (res) => {
                console.log(res);
            },
            error: (err) => {
                console.log(err.error.success, err.error.menssagem);
            }
        });
    }
}