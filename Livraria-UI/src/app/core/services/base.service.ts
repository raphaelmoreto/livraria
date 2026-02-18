import { environment } from '@environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export abstract class BaseService {
    protected baseUrl: string;

    constructor(protected http: HttpClient, route: string) {
        this.baseUrl = `${environment.apiUrl}/${route}`;
    }
}