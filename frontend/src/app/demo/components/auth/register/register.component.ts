import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/demo/services/auth/AuthServices';
import { passwordStrengthValidator } from '../helpers/passwordStrengthValidator';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
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
export class RegisterComponent {
    RegisterForm: FormGroup;

    constructor(
        private messageService: MessageService,
        private authService: AuthService,
        private router: Router,
        private formBuilder: FormBuilder
    ) {
        this.RegisterForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: [
                '',
                [
                    Validators.required,
                    Validators.minLength(6),
                    passwordStrengthValidator,
                ],
            ],
        });
    }

    onRegister() {
        if (this.RegisterForm.valid) {
            const { email, password } = this.RegisterForm.value;
            this.authService.register(email, password).subscribe(
                (response) => {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Registration Successful',
                        detail: 'You can now log in with your new account.',
                    });
                    // Redirects the user to /auth/login after showing the message
                    setTimeout(
                        () => this.router.navigate(['/auth/login']),
                        2000
                    ); // wait for 2 seconds
                },
                (error) => {
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Registration Error',
                        detail: 'The registration could not be completed.',
                    });
                }
            );
        } else {
            this.messageService.add({
                severity: 'error',
                summary: 'Invalid Data',
                detail: 'Please complete the form correctly.',
            });
        }
    }
}
