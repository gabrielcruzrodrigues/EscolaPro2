import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/admin/master/dashboard/dashboard.component';
import { UsersPanelComponent } from './pages/admin/master/users-panel/users-panel.component';
import { UsersShowComponent } from './pages/admin/master/users-show/users-show.component';
import { LoginComponent } from './pages/login/login.component';
import { UsersCreateComponent } from './pages/admin/master/users-create/users-create.component';
import { UsersSearchEditComponent } from './pages/admin/master/users-search-edit/users-search-edit.component';
import { UsersSearchDeleteComponent } from './pages/admin/master/users-search-delete/users-search-delete.component';
import { UsersEditComponent } from './pages/admin/master/users-edit/users-edit.component';
import { CompaniesPanelComponent } from './pages/admin/master/companies-panel/companies-panel.component';
import { CompaniesCreateComponent } from './pages/admin/master/companies-create/companies-create.component';
import { CompaniesShowComponent } from './pages/admin/master/companies-show/companies-show.component';
import { CompaniesSearchEditComponent } from './pages/admin/master/companies-search-edit/companies-search-edit.component';
import { CompaniesSearchDeleteComponent } from './pages/admin/master/companies-search-delete/companies-search-delete.component';

export const routes: Routes = [
     //admin/master

     {
          path: 'admin/master/dashboard',
          component: DashboardComponent
     },

     // === Users ===

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

     // === Companies ===

     {
          path: 'admin/master/companies-panel',
          component: CompaniesPanelComponent
     },
     {
          path: 'admin/master/companies-create',
          component: CompaniesCreateComponent
     },
     {
          path: 'admin/master/companies-show',
          component: CompaniesShowComponent
     },
     {
          path: 'admin/master/companies-search-edit',
          component: CompaniesSearchEditComponent
     },
     {
          path: 'admin/master/companies-search-delete',
          component: CompaniesSearchDeleteComponent
     },

     // === Puublic ===
     {
          path: 'login',
          component: LoginComponent
     }
];
