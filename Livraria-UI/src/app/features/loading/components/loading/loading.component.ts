import { Component } from '@angular/core';
import { LoadingService } from '@features/loading/services/loading.service';

@Component({
    selector: 'app-loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.scss']
})

export class LoadingComponent {
    loading$;

    constructor(private loadingService: LoadingService) {
        this.loading$ = this.loadingService.loading$;
    }
}