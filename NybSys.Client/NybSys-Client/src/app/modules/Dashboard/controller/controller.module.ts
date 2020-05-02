import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewcontrollerComponent } from './viewcontroller/viewcontroller.component';
import { EditcontrollerComponent } from './editcontroller/editcontroller.component';
import { PaginationModule } from '../../pagination/pagination.module';
import { MatDatepickerModule, MatNativeDateModule, DateAdapter, MatButtonModule } from '@angular/material';
import { ShareModule } from '../../Share/share.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path: "View-Controller",
        component: ViewcontrollerComponent,
        data: {
            titile2: 'Controller List'
        }
    },
    {
        path: "Edit-Controller",
        component: EditcontrollerComponent,
        data: {
            titile2: 'Edit Controller'
        }
    },
    {
        path: '',
        redirectTo: 'View-Controller',
        pathMatch: 'full'
    }
];
@NgModule({
  declarations: [ViewcontrollerComponent, EditcontrollerComponent],
  imports: [
    CommonModule,
        RouterModule.forChild(routes),
        PaginationModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ShareModule,
        MatButtonModule
  ]
})
export class ControllerModule { }
