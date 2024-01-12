import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem, MessageService, SelectItem } from 'primeng/api';
import { DataView } from 'primeng/dataview';
import { CategoryService } from 'src/app/demo/services/category/CategoryServices';
import { Product } from 'src/app/demo/services/interfaces/Product.interface';
import { ProductService } from 'src/app/demo/services/products/ProductServices';
import { generateRandomRating } from './helpers/generateRandomRating';

@Component({
    templateUrl: './products.component.html',
    providers: [MessageService],
})
export class ProductsComponent implements OnInit {
    products: Product[] = [];
    currentProduct: Product | null = null;

    sortOptions: SelectItem[] = [];

    sortOrder: number = 0;

    sortField: string = '';

    breadcrumbItems: MenuItem[] = [];

    productDialog: boolean = false;
    isEdit: boolean = false;

    productForm: FormGroup;

    statuses: any[]; // Opciones para el dropdown
    selectedFile: File;
    categoryOptions: any[] = [];

    deleteProductDialog: boolean = false; // Controla la visibilidad del diálogo de confirmación
    productToDelete: Product | null = null; // Almacena el producto a eliminar

    constructor(
        private productService: ProductService,
        private formBuilder: FormBuilder,
        private categoryService: CategoryService,
        private messageService: MessageService
    ) {}

    ngOnInit() {
        // datos del formulario de agregar y editar product para las validaciones
        this.productForm = this.formBuilder.group({
            name: ['', Validators.required],
            description: ['', Validators.required],
            category: [null, Validators.required],
            status: [null, Validators.required],
            price: [null, [Validators.required, Validators.min(0)]],
            image: [null],
        });

        // mostramos las categorias como opciones al crear un producto
        this.categoryService.getAllCategories().subscribe((categories) => {
            this.categoryOptions = categories.map((category) => ({
                label: category.categoryName,
                value: category.id,
            }));
            const categoryFormControl = this.productForm.get('category');
            categoryFormControl.setValue(this.categoryOptions);
        });

        // mostramos todos los productos
        this.productService.getAllProducts().subscribe((data) => {
            this.products = data.map((product) => ({
                ...product,
                rating: generateRandomRating(),
            }));
        });
        // status default
        this.statuses = [true, false];
        this.breadcrumbItems = [{ label: 'Products' }];
    }

    onSortChange(event: any) {
        const value = event.value;

        if (value.indexOf('!') === 0) {
            this.sortOrder = -1;
            this.sortField = value.substring(1, value.length);
        } else {
            this.sortOrder = 1;
            this.sortField = value;
        }
    }

    onFilter(dv: DataView, event: Event) {
        dv.filter((event.target as HTMLInputElement).value);
    }

    openNew() {
        this.isEdit = false;
        this.productDialog = true;
        this.selectedFile = null;
        this.productForm.reset(); // Opcional, si deseas reiniciar todo el formulario
    }

    hideDialog() {
        this.productDialog = false;
    }

    onFileSelect(event): void {
        if (event.target.files && event.target.files.length > 0) {
            this.selectedFile = event.target.files[0];
        } else {
            this.selectedFile = null;
        }
    }

    openEdit(product: Product) {
        this.isEdit = true;
        this.productDialog = true;
        this.selectedFile = null;

        this.currentProduct = product;

        // Cargar los datos del producto en el formulario
        this.productForm.patchValue({
            name: product.name,
            description: product.description,
            category: product.categoryId,
            status: product.status,
            price: product.price,
        });
    }

    onSubmit(): void {
        Object.keys(this.productForm.controls).forEach((field) => {
            const control = this.productForm.get(field);
            control.markAsTouched({ onlySelf: true });
        });
        if (this.isEdit) {
            const formData = new FormData();
            // Asegúrate de convertir y añadir cada valor al FormData correctamente
            Object.entries(this.productForm.value).forEach(([key, value]) => {
                if (key === 'category') {
                    // Si el campo es 'category', lo cambiamos a 'categoryId'
                    formData.append('categoryId', String(value));
                } else if (value instanceof File) {
                    // Para los archivos usamos el objeto File directamente
                    formData.append(key, value, value.name);
                } else {
                    // Para otros casos, convertimos todo a string para evitar errores
                    formData.append(key, String(value));
                }
            });

            // Añadir el archivo de imagen al FormData solo si ha sido seleccionado
            if (this.selectedFile) {
                formData.append(
                    'image',
                    this.selectedFile,
                    this.selectedFile.name
                );
            }

            // Llamada al servicio de actualización con FormData y el ID del producto
            this.productService
                .updateProduct(formData, this.currentProduct.id)
                .subscribe((success) => {
                    this.loadProducts();

                    this.productDialog = false;
                    this.selectedFile = null;
                    this.productForm.reset();
                    this.showAlert(
                        'success',
                        'successfully',
                        'updated product'
                    );
                });
        } else {
            if (this.productForm.valid && this.selectedFile) {
                // Crear instancia de FormData
                const formData = new FormData();

                // Añadir campos de texto al FormData
                formData.append('Name', this.productForm.value.name);
                formData.append('Price', this.productForm.value.price);
                formData.append('CategoryId', this.productForm.value.category);
                formData.append(
                    'Description',
                    this.productForm.value.description
                );
                formData.append('Status', this.productForm.value.status);

                // Añadir el archivo de imagen al FormData
                formData.append(
                    'Image',
                    this.selectedFile,
                    this.selectedFile.name
                );

                // Utilizar el servicio para enviar los datos
                this.productService.createProduct(formData).subscribe(
                    (response) => {
                        this.loadProducts();

                        this.productDialog = false;
                        this.selectedFile = null;
                        this.productForm.reset();
                        this.showAlert(
                            'success',
                            'successfully',
                            'Product created successfully'
                        );
                    },
                    (error) => {}
                );
            } else {
                console.error('Form invalid');
            }
        }
    }

    // Método para abrir el diálogo de confirmación
    confirmDeleteProduct(product: Product): void {
        this.productToDelete = product;
        this.deleteProductDialog = true;
    }

    // Método para cargar productos nuevamente
    loadProducts(): void {
        this.productService.getAllProducts().subscribe((data) => {
            this.products = data.map((product) => ({
                ...product,
                rating: generateRandomRating(),
            }));
        });
    }

    // Método llamado cuando se confirma la eliminación
    confirmDelete(): void {
        if (this.productToDelete) {
            this.productService
                .deleteProduct(this.productToDelete.id)
                .subscribe((success) => {
                    this.loadProducts();
                    this.showAlert('success', 'succesfully', 'removed product');

                    this.deleteProductDialog = false;
                    this.productToDelete = null;
                });
        }
    }

    // Método privado para mostrar alertas
    private showAlert(
        severity: string,
        summary: string,
        detail: string,
        life: number = 3000
    ): void {
        this.messageService.add({
            severity: severity,
            summary: summary,
            detail: detail,
            life: life,
        });
    }
}
