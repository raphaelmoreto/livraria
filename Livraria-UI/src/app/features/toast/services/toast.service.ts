import { Injectable } from '@angular/core';
import { IToast, ToastType } from '../models/toast.model';
import { Subject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class ToastService {
    // "Sibject" É UM ENTREGADOR DE MENSAGEM, UM OBJETO QUE PERMITE ENVIAR VALORES E PERMITE QUE OUTROS CÓDIGOS RECEBAM ESSES VALORES AUTOMATICAMENTE
    private toastSubject = new Subject<IToast>();
    private counter = 0;

    error(mensagem: string): void {
        this.show(mensagem, 'error');
    }

    getToast(): Observable<IToast> {
        return this.toastSubject.asObservable();
    }

    private show(mensagem: string, type: ToastType): void {
        this.toastSubject.next({
            id: ++this.counter,
            mensagem,
            type
        });
    }

    success(mensagem: string): void {
        this.show(mensagem, 'success');
    }

    warning(mensagem: string): void {
        this.show(mensagem, 'warning');
    }
}
