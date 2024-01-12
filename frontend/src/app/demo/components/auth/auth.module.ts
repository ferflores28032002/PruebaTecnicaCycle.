import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
    imports: [CommonModule, AuthRoutingModule, ToastModule],
    providers: [MessageService],
})
export class AuthModule {}
