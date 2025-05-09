import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";

@Component({
  selector: 'app-admin-families-panel',
  imports: [
    RouterModule,
    InfoTopComponent,
    AdminNavbarComponent
],
  templateUrl: './admin-families-panel.component.html',
  styleUrl: './admin-families-panel.component.sass'
})
export class AdminFamiliesPanelComponent {

}
