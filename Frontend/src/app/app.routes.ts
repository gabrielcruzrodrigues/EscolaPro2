import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/admin/master/dashboard/dashboard.component';
import { UsersPanelComponent } from './pages/admin/master/users-panel/users-panel.component';
import { UsersShowComponent } from './pages/admin/master/users-show/users-show.component';
import { LoginComponent } from './pages/login/login.component';
import { UsersCreateComponent } from './pages/admin/master/users-create/users-create.component';
import { UsersSearchEditComponent } from './pages/admin/master/users-search-edit/users-search-edit.component';
import { UsersSearchDeleteComponent } from './pages/admin/master/users-search-delete/users-search-delete.component';
import { UsersEditComponent } from './pages/admin/master/users-edit/users-edit.component';

export const routes: Routes = [
     //admin/master
     {
          path: 'admin/master/dashboard',
          component: DashboardComponent
     },
     {
          path: 'admin/master/users-panel',
          component: UsersPanelComponent
     },
     {
          path: 'admin/master/users-show',
          component: UsersShowComponent
     },
     {
          path: 'admin/master/users-create',
          component: UsersCreateComponent
     },
     {
          path: 'admin/master/users-search-edit',
          component: UsersSearchEditComponent
     },
     {
          path: 'admin/master/users-edit/:userId',
          component: UsersEditComponent
     },
     {
          path: 'admin/master/users-search-delete',
          component: UsersSearchDeleteComponent
     },

     //publico
     {
          path: 'login',
          component: LoginComponent
     }
];
