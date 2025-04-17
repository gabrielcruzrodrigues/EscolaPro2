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
import { CompaniesEditComponent } from './pages/admin/master/companies-edit/companies-edit.component';
import { masterGuard } from './guards/master.guard';
import { AuthLoadingComponent } from './components/layout/auth-loading/auth-loading.component';

export const routes: Routes = [
     // === Public config ===

     {
          path: 'verify',
          component: AuthLoadingComponent,
          pathMatch: 'full'
     },

     //admin/master

     {
          path: 'admin/master/dashboard',
          component: DashboardComponent,
          canActivate: [masterGuard]
     },

     // === MASTER - Users ===

     {
          path: 'admin/master/users-panel',
          component: UsersPanelComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/users-show',
          component: UsersShowComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/users-create',
          component: UsersCreateComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/users-search-edit',
          component: UsersSearchEditComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/users-edit/:userId',
          component: UsersEditComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/users-search-delete',
          component: UsersSearchDeleteComponent,
          canActivate: [masterGuard]
     },

     // === MASTER - Companies ===

     {
          path: 'admin/master/companies-panel',
          component: CompaniesPanelComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/companies-create',
          component: CompaniesCreateComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/companies-show',
          component: CompaniesShowComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/companies-search-edit',
          component: CompaniesSearchEditComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/companies-search-delete',
          component: CompaniesSearchDeleteComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/master/companies-edit/:companieId',
          component: CompaniesEditComponent,
          canActivate: [masterGuard]
     },

     // === Public ===
     {
          path: 'login',
          component: LoginComponent
     },

     // === Public config ===

     // {
     //      path: '**',
     //      component: AuthLoadingComponent
     // },
];
