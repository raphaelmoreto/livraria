import { AuthService } from './../../../core/services/auth.service';
import { Component, ViewChild } from '@angular/core';
import { IUsuarioCadastro } from 'src/app/features/usuario/models/usuario-cadastro.model';
import { IUsuarioLogin } from 'src/app/features/usuario/models/usuario-login.model';
import { LoginModalComponent } from '../../../features/login-modal/login-modal.component';
import { UsuarioService } from 'src/app/features/usuario/services/usuario.service';
import { ToastService } from 'src/app/features/toast/services/toast.service';

@Component({
  selector: 'app-menu-principal',
  templateUrl: './menu-principal.component.html',
  styleUrls: ['./menu-principal.component.scss']
})

export class MenuPrincipalComponent {
    modalLogin: boolean = false;

    @ViewChild(LoginModalComponent) loginModal!: LoginModalComponent;

    constructor (
        private authService: AuthService,
        private toast: ToastService,
        private usuarioService: UsuarioService
    ) { }

    abrirModalLogin(): void {
        this.modalLogin = true;
    }

    cadastro(usuarioCadastro: IUsuarioCadastro): void {
        this.usuarioService.criarConta(usuarioCadastro)
        .subscribe({
            next: (res) => {
                this.toast.success(res.mensagem);
                this.loginModal.login();
            },
            error: (err) => {
                if (err.error.tipo = 'warning') {
                    this.toast.warning(err.error.mensagem);
                } else {
                    this.toast.error(err.error.mensagem);
                }
            }
        });
    }

    fecharModalLogin(): void {
        this.modalLogin = false;
    }

    login(usuarioLogin: IUsuarioLogin): void {
        this.authService.login(usuarioLogin)
        .subscribe({
            next: () => {
                this.toast.success('LOGIN EFETUADO COM SUCESSO');
                this.fecharModalLogin();
            },
            error: (err) => {
                console.log(err);
                this.toast.error(err.error.erro);
            }
        });
    }
}