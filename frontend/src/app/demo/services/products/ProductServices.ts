import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/AuthServices';
import { Product } from '../interfaces/Product.interface';

@Injectable({
    providedIn: 'root',
})
export class ProductService {
    private apiUrl = environment.apiUrl;

    private productUrl = `${this.apiUrl}/Product`;

    constructor(private http: HttpClient, private authService: AuthService) {}

    private getHeaders(): HttpHeaders {
        const token = this.authService.getToken();
        return new HttpHeaders().set('Authorization', `Bearer ${token}`);
    }

    // Obtener todos los productos
    getAllProducts(): Observable<Product[]> {
        return this.http
            .get<Product[]>(this.productUrl, { headers: this.getHeaders() })
            .pipe(
                catchError((error) => {
                    console.error('Error al obtener productos:', error);
                    throw error;
                })
            );
    }

    // Crear un nuevo producto
    createProduct(formData: FormData): Observable<any> {
        return this.http
            .post(this.productUrl, formData, {
                headers: this.getHeaders().delete('Content-Type'), // Angular establecerá el tipo de contenido correcto para FormData
            })
            .pipe(
                catchError((error) => {
                    throw error;
                })
            );
    }

    // Actualizar un producto
    // Actualizar un producto con FormData
    updateProduct(formData: FormData, productId: string): Observable<boolean> {
        return this.http
            .put(`${this.productUrl}/${productId}`, formData, {
                headers: this.getHeaders(),
            })
            .pipe(
                map((response) => true),
                catchError((error) => {
                    console.error('Error al actualizar el producto:', error);
                    return of(false);
                })
            );
    }

    // Eliminar un producto
    deleteProduct(id: string): Observable<boolean> {
        return this.http
            .delete(`${this.productUrl}/${id}`, {
                headers: this.getHeaders(),
                observe: 'response',
            })
            .pipe(
                map((response) => {
                    // Verificar si la respuesta del servidor tiene un código de estado 200 (OK)
                    if (response.status === 200) {
                        return true; // Eliminación exitosa
                    }
                    return false; // Eliminación fallida
                }),
                catchError((error) => {
                    console.error('Error al eliminar el producto:', error);
                    return of(false);
                })
            );
    }
}
