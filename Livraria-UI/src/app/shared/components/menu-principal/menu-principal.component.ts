import { AuthService } from '@core/services/auth.service';
import { Component, ViewChild } from '@angular/core';
import { IUsuarioCadastro } from '@features/usuario/models/usuario-cadastro.model';
import { IUsuarioLogin } from '@features/usuario/models/usuario-login.model';
import { LoginModalComponent } from '../login-modal/login-modal.component';
import { Router } from '@angular/router';
import { ToastService } from '@features/toast/services/toast.service';
import { UsuarioService } from '@features/usuario/services/usuario.service';

@Component({
  selector: 'app-menu-principal',
  templateUrl: './menu-principal.component.html',
  styleUrls: ['./menu-principal.component.scss']
})

export class MenuPrincipalComponent {
    dropdownAberto: boolean = false;
    modalAberto: boolean = false;

    @ViewChild(LoginModalComponent) loginModal!: LoginModalComponent;

    usuarioLogado$ = this.authService.usuario$;

    constructor (
        private authService: AuthService,
        private router: Router,
        private toast: ToastService,
        private usuarioService: UsuarioService
    ) { }

    abrirModalLogin(): void {
        this.modalAberto = true;
    }

    cadastro(usuarioCadastro: IUsuarioCadastro): void {
        this.usuarioService.criarConta(usuarioCadastro)
        .subscribe({
            next: (res) => {
                this.toast.success(res.mensagem);
                this.loginModal.usuarioCadastro.reset();
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

    fecharDropdown(): void {
        this.dropdownAberto = false;
    }

    fecharModalLogin(): void {
        this.modalAberto = false;
    }

    get isHome(): boolean {
        return this.router.url === '/';
    }

    login(usuarioLogin: IUsuarioLogin): void {
        this.authService.login(usuarioLogin)
        .subscribe({
            next: () => {
                console.log(this.authService.role$);
                this.toast.success('LOGIN EFETUADO COM SUCESSO');
                this.loginModal.usuarioLogin.reset();
                this.fecharModalLogin();
            },
            error: (err) => {
                this.toast.error(err.error.erro);
            }
        });
    }

    logout(): void {
        this.authService.logout();
        this.fecharDropdown();
        this.router.navigate(['/']);
    }

    toggleDropdown(): void {
        this.dropdownAberto = !this.dropdownAberto;
    }
}