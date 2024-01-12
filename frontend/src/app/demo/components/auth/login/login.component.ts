import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/demo/services/auth/AuthServices';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [
        `
            :host ::ng-deep .pi-eye,
            :host ::ng-deep .pi-eye-slash {
                transform: scale(1.6);
                margin-right: 1rem;
                color: var(--primary-color) !important;
            }
        `,
    ],
})
export class LoginComponent {
    valCheck: string[] = ['remember'];
    password!: string;
    email!: string;

    constructor(
        public layoutService: LayoutService,
        private messageService: MessageService,
        private authService: AuthService,
        private router: Router
    ) {}

    // Método para manejar el inicio de sesión
    onLogin() {
        const loginData = {
            email: this.email,
            password: this.password,
        };

        this.authService.login(loginData.email, loginData.password).subscribe(
            (response) => {
                // Aquí deberías verificar la respuesta para asegurarte de que el inicio de sesión fue exitoso
                if (response && response.accessToken) {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Login Successful',
                        detail: 'You have successfully logged in.',
                    });
                    setTimeout(() => this.router.navigate(['/']), 2000); // wait for 2 seconds

                    console.log(this.authService.getToken());
                } else {
                    // Si la respuesta no contiene un token, considera eso como un inicio de sesión fallido
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Login Failed',
                        detail: 'Invalid email or password.',
                    });
                }
            },
            (error) => {
                // Maneja otros errores que podrían ocurrir durante el inicio de sesión
                this.messageService.add({
                    severity: 'error',
                    summary: 'Login Error',
                    detail: 'An error occurred while attempting to log in.',
                });
            }
        );
    }
}
