import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class LoadingService {
    private loading = new BehaviorSubject<boolean>(false);

    loading$ = this.loading.asObservable();

    mostrar(): void {
        this.loading.next(true);
    }

    esconder(): void {
        this.loading.next(false);
    }
}