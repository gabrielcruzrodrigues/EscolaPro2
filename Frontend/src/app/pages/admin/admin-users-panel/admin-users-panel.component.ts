import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { RouterModule } from '@angular/router';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { AdminLastActiveUsersComponent } from "../../../components/layout/admin/admin-last-active-users/admin-last-active-users.component";

@Component({
  selector: 'app-admin-users-panel',
  imports: [AdminNavbarComponent, RouterModule, InfoTopComponent, AdminLastActiveUsersComponent],
  templateUrl: './admin-users-panel.component.html',
  styleUrl: './admin-users-panel.component.sass'
})
export class AdminUsersPanelComponent {

}
