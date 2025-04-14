import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/admin/master/dashboard/dashboard.component';
import { UsersPanelComponent } from './pages/admin/master/users-panel/users-panel.component';

export const routes: Routes = [
     //admin/master
     {
          path: 'admin/master/dashboard',
          component: DashboardComponent
     },
     {
          path: 'admin/master/users-panel',
          component: UsersPanelComponent
     }
];
