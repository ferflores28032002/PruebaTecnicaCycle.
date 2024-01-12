import { Component, OnInit } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/demo/services/category/CategoryServices';
import { Category } from 'src/app/demo/services/interfaces/Category.interface';

@Component({
    templateUrl: './category.component.html',
    providers: [MessageService],
})
export class CategoryComponent implements OnInit {
    categories: Category[] = [];
    category: Category = { categoryName: '', id: '' };
    categoryDialog: boolean = false;
    cols: any[] = [];
    deleteCategoryDialog: boolean = false;
    globalFilterFields: [string, string];
    isEdit: boolean = false;
    selectedCategories: Category[] = [];
    submitted: boolean = false;
    breadcrumbItems: MenuItem[] = [];

    constructor(
        private categoryService: CategoryService,
        private messageService: MessageService
    ) {}

    ngOnInit() {
        this.categoryService.getAllCategories().subscribe(
            (data) => (this.categories = data),
            (error) => console.error('Error al obtener categorías:', error)
        );
        this.cols = [
            { field: 'id', header: 'ID' },
            { field: 'categoryName', header: 'Category Name' },
        ];
        this.globalFilterFields = ['id', 'categoryName'];
        this.breadcrumbItems = [{ label: 'Categories' }];
    }
    editCategory(category: Category) {
        this.category = { ...category };
        this.isEdit = true;
        this.categoryDialog = true;
    }

    openNew() {
        this.category = { categoryName: '', id: '' };
        this.isEdit = false;
        this.categoryDialog = true;
    }

    deleteCategory(category: Category) {
        this.category = category; // Establecer la categoría seleccionada para eliminar
        this.deleteCategoryDialog = true; // Mostrar el diálogo de confirmación
    }
    confirmDelete() {
        if (this.category && this.category.id) {
            this.categoryService
                .deleteCategory(this.category.id)
                .subscribe((response) => {
                    this.loadCategories();
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Éxito',
                        detail: 'Categoría Eliminada',
                        life: 3000,
                    });
                });
        }
        this.deleteCategoryDialog = false;
        this.category = { categoryName: '', id: '' };
    }

    hideDialog() {
        this.categoryDialog = false;
        this.submitted = false;
    }

    createOrUpdateCategory() {
        this.submitted = true;

        if (this.category.categoryName?.trim()) {
            if (this.isEdit) {
                // Lógica para actualizar/editar la categoría
                this.categoryService
                    .updateCategory(this.category)
                    .subscribe((response) => {
                        this.loadCategories();
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: 'Categoría actualizada',
                            life: 3000,
                        });
                    });
            } else {
                // Lógica para crear una nueva categoría
                this.categoryService
                    .createCategory(this.category)
                    .subscribe((response) => {
                        this.loadCategories();
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: 'Categoría creada',
                            life: 3000,
                        });
                    });
            }

            this.categoryDialog = false;
            this.category = { categoryName: '', id: '' };
            this.isEdit = false; // Resetear la bandera isEdit
        }
    }

    loadCategories() {
        this.categoryService
            .getAllCategories()
            .subscribe((data) => (this.categories = data));
    }
}
