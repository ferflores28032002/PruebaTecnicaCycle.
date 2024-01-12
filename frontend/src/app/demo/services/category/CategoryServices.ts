import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/AuthServices';
import { Category } from '../interfaces/Category.interface';

@Injectable({
    providedIn: 'root',
})
export class CategoryService {
    private apiUrl = environment.apiUrl;

    private categoryUrl = `${this.apiUrl}/Category`;

    constructor(private http: HttpClient, private authService: AuthService) {}

    // Método para obtener las cabeceras con el token
    private getHeaders(): HttpHeaders {
        const token = this.authService.getToken();
        return new HttpHeaders().set('Authorization', `Bearer ${token}`);
    }

    // Obtener todas las categorías
    getAllCategories(): Observable<Category[]> {
        return this.http
            .get<Category[]>(this.categoryUrl, { headers: this.getHeaders() })
            .pipe(
                catchError((error) => {
                    console.error('Error al obtener categorías:', error);
                    throw error;
                })
            );
    }

    // Crear una nueva categoría
    createCategory(category: Category): Observable<Category> {
        return this.http
            .post<Category>(this.categoryUrl, category, {
                headers: this.getHeaders(),
            })
            .pipe(
                catchError((error) => {
                    throw error;
                })
            );
    }

    // Actualizar una categoría
    updateCategory(category: Category): Observable<boolean> {
        return this.http
            .put<Category>(`${this.categoryUrl}/${category.id}`, category, {
                headers: this.getHeaders(),
            })
            .pipe(
                map((response) => true), // Si la petición es exitosa, devuelve true
                catchError((error) => {
                    return of(false); // En caso de error, devuelve false
                })
            );
    }

    deleteCategory(id: string): Observable<boolean> {
        return this.http
            .delete(`${this.categoryUrl}/${id}`, { headers: this.getHeaders() })
            .pipe(
                map((response) => true), // Si la petición es exitosa, devuelve true
                catchError((error) => {
                    return of(false); // En caso de error, devuelve false
                })
            );
    }
}
