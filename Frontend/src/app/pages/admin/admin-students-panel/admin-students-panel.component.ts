import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-students-panel',
  imports: [
    AdminNavbarComponent, 
    InfoTopComponent,
    RouterModule
  ],
  templateUrl: './admin-students-panel.component.html',
  styleUrl: './admin-students-panel.component.sass'
})
export class AdminStudentsPanelComponent {

}
