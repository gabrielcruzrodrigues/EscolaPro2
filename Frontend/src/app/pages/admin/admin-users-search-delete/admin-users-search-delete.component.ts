import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { AdminMainSearchUserBoxComponent } from "../../../components/layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { AdminUsersTableComponent } from "../../../components/layout/admin/admin-users-table/admin-users-table.component";
import { User } from '../../../types/User';
import { formatDate } from '../../../utils/FormatDate';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-admin-users-search-delete',
  imports: [
    AdminNavbarComponent,
    AdminMainSearchUserBoxComponent,
    AdminUsersTableComponent
  ],
  templateUrl: './admin-users-search-delete.component.html',
  styleUrl: './admin-users-search-delete.component.sass'
})
export class AdminUsersSearchDeleteComponent {
  titleUsersSearchInput: string = 'Buscar usuários para deletar';
  users: User[] = [];
  typeActions: string = 'trash';
  title: string = 'Users';
  usersSearched: boolean = false;

  constructor(private authService: AuthService) {}

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
