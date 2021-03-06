import { ForbiddenComponent } from './../../components/forbidden/forbidden.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PieChartComponent } from 'src/app/components/pieChart/pieChart.component';
import { StatsCardComponent } from 'src/app/components/statsCard/statsCard.component';
import { RoundProgressModule } from 'angular-svg-round-progressbar';
import { ChartsModule } from 'ng2-charts';
import { DashboardRoutingModule } from './dashboard.routing';



@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RoundProgressModule,
    ChartsModule
  ],
  declarations: [
    DashboardComponent,
    PieChartComponent,
    StatsCardComponent,
    ForbiddenComponent,
  ],
  providers: [

  ]
})
export class DashboardModule { }
