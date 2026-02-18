import { AuthService } from '@core/services/auth.service';
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

//O "HttpInterceptor" NO ANGULAR É UMA FERRAMENTA QUE PERMITE INTERCEPTAR TODAS AS REQUISIÇÕES HTTP QUE O APLICATIVO FAZ ANTES QUE ELAS SAIAM PARA A REDE, E TAMBÉM INTERCEPTAR AS RESPOSTAS QUE CHEGAM DO SERVIDOR. FUNCIONA COMO UM FILTRO CENTRALIZADO PARA REQUISIÇÕES E RESPOSTAS HTTP

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {}
    
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.authService.getToken();
        if (token) {
            const cloned = request.clone({
                headers: request.headers.set('Authorization', `Bearer ${token}`)
            });
            return next.handle(cloned);
        }
        return next.handle(request);
    }
}
