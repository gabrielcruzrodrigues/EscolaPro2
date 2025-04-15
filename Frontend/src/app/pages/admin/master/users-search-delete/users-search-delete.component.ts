import { Component } from '@angular/core';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { UsersMainSearchBoxComponent } from "../../../../components/layout/users-main-search-box/users-main-search-box.component";
import { UsersTableComponent } from "../../../../components/layout/admin/master/users-table/users-table.component";
import { User } from '../../../../types/User';

@Component({
  selector: 'app-users-search-delete',
  imports: [AdminMasterNavbarComponent, UsersMainSearchBoxComponent, UsersTableComponent],
  templateUrl: './users-search-delete.component.html',
  styleUrl: './users-search-delete.component.sass'
})
export class UsersSearchDeleteComponent {
  titleUsersSearchInput: string = 'Buscar usuários para desativar';
  users: User[] = [];
  typeActions: string = 'trash';
  title: string = 'Users';

  onSearchUser(users: User[]): void {
    this.users = users;
  }
}
