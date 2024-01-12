import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DocumentationRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';

@NgModule({
    imports: [CommonModule, DocumentationRoutingModule],
    declarations: [ProfileComponent],
})
export class DocumentationModule {}
