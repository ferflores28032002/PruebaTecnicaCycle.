import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUploadModule } from 'primeng/fileupload';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { OrderListModule } from 'primeng/orderlist';
import { PickListModule } from 'primeng/picklist';

import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { ToastModule } from 'primeng/toast';
import { ListDemoRoutingModule } from './products-routing.module';
import { ProductsComponent } from './products.component';

@NgModule({
    imports: [
        CommonModule,
        ToastModule,
        InputTextareaModule,
        ReactiveFormsModule,
        FormsModule,
        InputNumberModule,
        DialogModule,
        RadioButtonModule,
        FileUploadModule,
        ListDemoRoutingModule,
        DataViewModule,
        PickListModule,
        OrderListModule,
        InputTextModule,
        RatingModule,
        ButtonModule,
        BreadcrumbModule,
        DropdownModule,
    ],
    declarations: [ProductsComponent],
})
export class ProductsModule {}
