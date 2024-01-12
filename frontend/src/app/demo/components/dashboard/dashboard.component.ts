import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { CategoryService } from '../../services/category/CategoryServices';
import { Category } from '../../services/interfaces/Category.interface';
import { Product } from '../../services/interfaces/Product.interface';
import { ProductService } from '../../services/products/ProductServices'; // Asegúrate de que las rutas sean correctas

@Component({
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit, OnDestroy {
    items!: MenuItem[];
    products!: Product[];
    categories!: Category[];
    chartData: any;
    chartOptions: any;
    subscription: Subscription = new Subscription();

    constructor(
        private productService: ProductService,
        private categoryService: CategoryService
    ) {}

    ngOnInit(): void {
        this.subscription.add(
            this.productService.getAllProducts().subscribe((data) => {
                this.products = data;
            })
        );

        this.subscription.add(
            this.categoryService.getAllCategories().subscribe((data) => {
                this.categories = data;
            })
        );
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
