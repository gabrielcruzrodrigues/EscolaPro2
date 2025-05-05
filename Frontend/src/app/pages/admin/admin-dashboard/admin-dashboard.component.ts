import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";

@Component({
  selector: 'app-admin-dashboard',
  imports: [AdminNavbarComponent],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.sass'
})
export class AdminDashboardComponent {

}
