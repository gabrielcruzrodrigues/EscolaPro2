import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { FamiliesMainSearchBoxComponent } from "../../../components/layout/families-main-search-box/families-main-search-box.component";

@Component({
  selector: 'app-admin-families-panel',
  imports: [
    RouterModule,
    InfoTopComponent,
    AdminNavbarComponent,
    FamiliesMainSearchBoxComponent
],
  templateUrl: './admin-families-panel.component.html',
  styleUrl: './admin-families-panel.component.sass'
})
export class AdminFamiliesPanelComponent {
  dropdown: boolean = true;
  placeholder: string = 'Buscar familiares...'
}
