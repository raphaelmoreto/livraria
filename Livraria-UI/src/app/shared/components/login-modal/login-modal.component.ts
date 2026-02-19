import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUsuarioCadastro } from '@features/usuario/models/usuario-cadastro.model';
import { IUsuarioLogin } from '@features/usuario/models/usuario-login.model';
import { ToastService } from '@features/toast/services/toast.service';

@Component({
    selector: 'app-login-modal',
    templateUrl: './login-modal.component.html',
    styleUrls: ['./login-modal.component.scss']
})

export class LoginModalComponent {
    @Input() aberto: boolean = false;

    isActive: boolean = false;

    usuarioCadastro = new FormGroup({
        nome: new FormControl<string>('', Validators.required),
        usuario: new FormControl<string>('', Validators.required),
        email: new FormControl<string>('', Validators.required),
        senha: new FormControl<string>('', Validators.required),
        fk_perfil: new FormControl<number>(2, Validators.required) // 2 = USUÁRIO COMUM NO SISTEMA
    });

    usuarioLogin = new FormGroup({
        usuario: new FormControl<string>('', Validators.required),
        senha: new FormControl<string>('', Validators.required)
    });

    constructor (private toast: ToastService) { }

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
        if (this.usuarioCadastro.invalid) {
            this.usuarioCadastro.markAllAsTouched();
            this.toast.warning('POR FAVOR, PREENCHER TODOS OS CAMPOS');
        } else {
            // "getRawValue()" TRANSFORMA O "FormGroup" EM UM OBJETO COMUM
            const usuarioCadastro: IUsuarioCadastro = this.usuarioCadastro.getRawValue();
            this.clickedCadastroEmitt.emit(usuarioCadastro);
        }
    }

    onClickedLogin(): void {
        if (this.usuarioLogin.invalid) {
            this.usuarioLogin.markAsTouched();
            this.toast.warning('POR FAVOR, PREENCHER TODOS OS CAMPOS');
        } else {
            const usuarioLogin: IUsuarioLogin = this.usuarioLogin.getRawValue();
            this.clickedLoginEmitt.emit(usuarioLogin);
        }
    }
}