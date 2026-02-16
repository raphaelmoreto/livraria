import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IUsuarioInput } from '../../core/models/usuario-input.model';

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

    @Output('clickedCadastro') clickedCadastroEmitt = new EventEmitter<IUsuarioInput>();
    @Output() fecharModal = new EventEmitter<void>();

    register(): void {
        this.isActive = true;
    }

    login() {
        this.isActive = false;
    }

    fechar(): void {
        this.fecharModal.emit();
    }

    onClickedCadastro(): void {
        // "getRawValue()" TRANSFORMA O "FormGroup" EM UM OBJETO COMUM
        const usuarioCadastro: IUsuarioInput = this.usuarioCadastro.getRawValue();
        this.clickedCadastroEmitt.emit(usuarioCadastro);
    }
}