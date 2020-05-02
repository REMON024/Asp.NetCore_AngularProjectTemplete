import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewactionComponent } from './viewaction/viewaction.component';
import { EditactionComponent } from './editaction/editaction.component';
import { PaginationModule } from '../../pagination/pagination.module';
import { MatDatepickerModule, MatNativeDateModule, DateAdapter, MatButtonModule } from '@angular/material';
import { ShareModule } from '../../Share/share.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Routes, RouterModule } from '@angular/router';
import { EditcontrollerComponent } from '../controller/editcontroller/editcontroller.component';

const routes: Routes = [
    {
        path: "View-Action",
        component: ViewactionComponent,
        data: {
            titile2: 'Action List'
        }
    },
    {
        path: "Edit-Action",
        component: EditactionComponent,
        data: {
            titile2: 'Edit Action'
        }
    },
    {
        path: '',
        redirectTo: 'View-Action',
        pathMatch: 'full'
    }
];
@NgModule({
  declarations: [ViewactionComponent, EditactionComponent],
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
export class ActionModule { }
