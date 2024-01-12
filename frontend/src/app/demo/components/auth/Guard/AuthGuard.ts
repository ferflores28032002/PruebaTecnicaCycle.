import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from 'src/app/demo/services/auth/AuthServices';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}

    canActivate(): boolean {
        const token = this.authService.getToken();
        if (!token) {
            this.router.navigate(['/auth/login']); // Redirige al login si no hay token
            return false;
        }
        return true; // Permite el acceso a la ruta si hay token
    }
}
