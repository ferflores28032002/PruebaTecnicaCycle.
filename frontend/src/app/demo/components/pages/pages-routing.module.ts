import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/Guard/AuthGuard';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: 'products',
                data: { breadcrumb: 'products' },
                loadChildren: () =>
                    import('./products/products.module').then(
                        (m) => m.ProductsModule
                    ),
                canActivate: [AuthGuard], // Protege esta ruta
            },
            {
                path: 'category',
                data: { breadcrumb: 'category' },
                loadChildren: () =>
                    import('./category/category.module').then(
                        (m) => m.CategoryModule
                    ),
                canActivate: [AuthGuard], // Protege esta ruta
            },

            { path: '**', redirectTo: '/auth/login' }, // Redirige a login para rutas desconocidas
        ]),
    ],
    exports: [RouterModule],
})
export class UIkitRoutingModule {}
