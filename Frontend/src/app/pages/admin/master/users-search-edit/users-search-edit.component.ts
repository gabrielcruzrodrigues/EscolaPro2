import { Component } from '@angular/core';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { UsersMainSearchBoxComponent } from "../../../../components/layout/users-main-search-box/users-main-search-box.component";
import { UsersTableComponent } from "../../../../components/layout/admin/master/users-table/users-table.component";
import { User } from '../../../../types/User';

@Component({
  selector: 'app-users-search-edit',
  imports: [
    AdminMasterNavbarComponent, 
    UsersMainSearchBoxComponent, 
    UsersTableComponent
  ],
  templateUrl: './users-search-edit.component.html',
  styleUrl: './users-search-edit.component.sass'
})
export class UsersSearchEditComponent {
  titleUsersSearchInput: string = 'Buscar usuários para a edição';
  users: User[] = [];
  typeActions: string = 'edit';
  title: string = 'Users';

  onSearchUser(users: User[]): void {
    this.users = users;
  }
}
