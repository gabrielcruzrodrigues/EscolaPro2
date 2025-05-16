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
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard.component';
import { AdminUsersPanelComponent } from './pages/admin/admin-users-panel/admin-users-panel.component';
import { AdminUsersCreateComponent } from './pages/admin/admin-users-create/admin-users-create.component';
import { AdminUsersShowComponent } from './pages/admin/admin-users-show/admin-users-show.component';
import { AdminUsersSearchEditComponent } from './pages/admin/admin-users-search-edit/admin-users-search-edit.component';
import { AdminUsersSearchDeleteComponent } from './pages/admin/admin-users-search-delete/admin-users-search-delete.component';
import { AdminUsersDetailsComponent } from './pages/admin/admin-users-details/admin-users-details.component';
import { AdminStudentsPanelComponent } from './pages/admin/admin-students-panel/admin-students-panel.component';
import { AdminUsersEditComponent } from './pages/admin/admin-users-edit/admin-users-edit.component';
import { AdminFamiliesPanelComponent } from './pages/admin/admin-families-panel/admin-families-panel.component';
import { AdminFamiliesCreateComponent } from './pages/admin/admin-families-create/admin-families-create.component';
import { AdminFamiliesShowComponent } from './pages/admin/admin-families-show/admin-families-show.component';

export const routes: Routes = [
     // === Public config ===

     {
          path: 'verify',
          component: AuthLoadingComponent,
          pathMatch: 'full'
     },
     {
          path: '',
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

     //admin

     {
          path: 'admin/dashboard',
          component: AdminDashboardComponent,
          canActivate: [masterGuard]
     },

     // === ADMIN - Users ===

     {
          path: 'admin/users-panel',
          component: AdminUsersPanelComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-create',
          component: AdminUsersCreateComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-show',
          component: AdminUsersShowComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-search-edit',
          component: AdminUsersSearchEditComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-edit/:userId',
          component: AdminUsersEditComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-search-delete',
          component: AdminUsersSearchDeleteComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/users-details/:userId',
          component: AdminUsersDetailsComponent,
          canActivate: [masterGuard]
     },

     // === ADMIN - Students ===

     {
          path: 'admin/students-panel',
          component: AdminStudentsPanelComponent,
          canActivate: [masterGuard]
     },

     // === ADMIN - Families ===

     {
          path: 'admin/families-panel',
          component: AdminFamiliesPanelComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/families-create',
          component: AdminFamiliesCreateComponent,
          canActivate: [masterGuard]
     },
     {
          path: 'admin/families-show',
          component: AdminFamiliesShowComponent,
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
