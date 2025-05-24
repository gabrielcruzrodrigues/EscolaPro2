import { Component } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { FamiliesMainSearchBoxComponent } from "../../../components/layout/families-main-search-box/families-main-search-box.component";
import { AdminFamiliesTableComponent } from "../../../components/layout/admin/admin-families-table/admin-families-table.component";
import { Family } from '../../../types/Family';
import { AuthService } from '../../../services/auth.service';
import { formatDate } from '../../../utils/FormatDate';

@Component({
  selector: 'app-admin-families-search-edit',
  imports: [
    AdminNavbarComponent,
    InfoTopComponent,
    FamiliesMainSearchBoxComponent,
    AdminFamiliesTableComponent
  ],
  templateUrl: './admin-families-search-edit.component.html',
  styleUrl: './admin-families-search-edit.component.sass'
})
export class AdminFamiliesSearchEditComponent {
  titleUsersSearchInput: string = 'Buscar familiar para edição';
  families: Family[] = [];
  typeActions: string = 'edit';
  title: string = 'families';
  familiesSearched: boolean = false;
  placeholder: string = 'Buscar familiares...';

  constructor(
    private authService: AuthService
  ) { }

  onSearchFamily(families: Family[]): void {
    if (families.length != 0) {
      this.familiesSearched = true;
    }

    families.forEach(family => {
      family.createdAt = formatDate(family.createdAt);
    })

    this.families = families;
  }
}
