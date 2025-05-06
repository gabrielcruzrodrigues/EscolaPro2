import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { AdminMainSearchUserBoxComponent } from "../../../components/layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { AdminUsersTableComponent } from "../../../components/layout/admin/admin-users-table/admin-users-table.component";
import { User } from '../../../types/User';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { AuthService } from '../../../services/auth.service';
import { formatDate } from '../../../utils/FormatDate';

@Component({
  selector: 'app-admin-users-search-edit',
  imports: [
    AdminNavbarComponent,
    AdminMainSearchUserBoxComponent,
    AdminUsersTableComponent,
    InfoTopComponent
  ],
  templateUrl: './admin-users-search-edit.component.html',
  styleUrl: './admin-users-search-edit.component.sass'
})
export class AdminUsersSearchEditComponent {
  titleUsersSearchInput: string = 'Buscar usuários para edição';
  users: User[] = [];
  typeActions: string = 'edit';
  title: string = 'Users';
  usersSearched: boolean = false;
  
  constructor(
    private authService: AuthService
  ) { }

  onSearchUser(users: User[]): void {
    if (users.length != 0) {
      this.usersSearched = true;
    }

    users.forEach(user => {
      user.roleName = this.authService.getRoleName(user.role);
      user.createdAt = formatDate(user.createdAt);
    })
    this.users = users;
  }
}
