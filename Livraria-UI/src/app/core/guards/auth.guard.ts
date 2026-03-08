import { AuthService } from '@core/services/auth.service';
import { CanActivate, CanActivateFn, Router } from '@angular/router';
// import { Injectable } from '@angular/core';
import { inject } from '@angular/core';

//MODO ANTIGO ↓
// @Injectable({
//     providedIn: 'root'
// })

// export class AuthGuard implements CanActivate {
//     constructor (private authService: AuthService, private router: Router) { }

//     canActivate(): boolean {
//         if (this.authService.isLoggedIn()) {
//             return true;
//         }
//         this.router.navigate(['/']);
//         return false;
//     }
// };

export const authGuard: CanActivateFn = () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.isLoggedIn()) {
        return true;
    }

    return router.createUrlTree(['/']);

    // router.navigate(['/']);
    // return false;
};