import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IUsuarioCadastro } from '@features/usuario/models/usuario-cadastro.model';
import { IUsuarioLogin } from '@features/usuario/models/usuario-login.model';

@Component({
    selector: 'app-login-modal',
    templateUrl: './login-modal.component.html',
    styleUrls: ['./login-modal.component.scss']
})

export class LoginModalComponent {
    @Input() aberto: boolean = false;

    isActive: boolean = false;

    usuarioCadastro = new FormGroup({
        nome: new FormControl<string>(''),
        usuario: new FormControl<string>(''),
        email: new FormControl<string>(''),
        senha: new FormControl<string>(''),
        fk_perfil: new FormControl<number>(2) // 2 = USUÁRIO COMUM NO SISTEMA
    });

    usuarioLogin = new FormGroup({
        usuario: new FormControl<string>(''),
        senha: new FormControl<string>('')
    });

    @Output('clickedCadastro') clickedCadastroEmitt = new EventEmitter<IUsuarioCadastro>();
    @Output('clickedLogin') clickedLoginEmitt = new EventEmitter<IUsuarioLogin>();
    @Output() fecharModal = new EventEmitter<void>();

    register(): void {
        this.isActive = true;
    }

    login(): void {
        this.isActive = false;
    }

    fechar(): void {
        this.fecharModal.emit();
    }

    onClickedCadastro(): void {
        // "getRawValue()" TRANSFORMA O "FormGroup" EM UM OBJETO COMUM
        const usuarioCadastro: IUsuarioCadastro = this.usuarioCadastro.getRawValue();
        this.clickedCadastroEmitt.emit(usuarioCadastro);
    }

    onClickedLogin(): void {
        const usuarioLogin: IUsuarioLogin = this.usuarioLogin.getRawValue();
        this.clickedLoginEmitt.emit(usuarioLogin);
    }
}