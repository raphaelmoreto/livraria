import { Component, OnInit } from '@angular/core';
import { IToast } from '../../models/toast.model';
import { ToastService } from '../../services/toast.service';

@Component({
    selector: 'app-toast',
    templateUrl: './toast.component.html',
    styleUrls: ['./toast.component.scss']
})

export class ToastComponent implements OnInit {
    toasts: IToast[] = [];

    constructor (private toastService: ToastService) { }

    ngOnInit(): void {
        this.toastService.getToast()
        .subscribe(toast => {
            this.toasts.push(toast);

            setTimeout(() => {
                this.removeToast(toast.id);
            }, 4000);
        });
    }

    removeToast(id: number): void {
        this.toasts = this.toasts.filter(t => t.id !== id);
    }
}