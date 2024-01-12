import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginResponse } from '../interfaces/LoginResponse.interface';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private apiUrl = environment.apiUrl;

    private authUrl = this.apiUrl;
    private token: string | null = null;

    constructor(private http: HttpClient) {
        this.token = localStorage.getItem('authToken');
    }

    login(email: string, password: string): Observable<LoginResponse> {
        return this.http
            .post<LoginResponse>(`${this.authUrl}/login`, { email, password })
            .pipe(
                tap((response) => {
                    if (response && response.accessToken) {
                        this.token = response.accessToken;
                        localStorage.setItem('authToken', response.accessToken);
                    }
                }),
                catchError((error) => {
                    throw error;
                })
            );
    }

    register(email: string, password: string): Observable<any> {
        return this.http
            .post<any>(`${this.authUrl}/register`, { email, password })
            .pipe(
                catchError((error) => {
                    console.error('Error en el registro:', error);
                    throw error;
                })
            );
    }

    getToken(): string | null {
        return this.token;
    }
}
