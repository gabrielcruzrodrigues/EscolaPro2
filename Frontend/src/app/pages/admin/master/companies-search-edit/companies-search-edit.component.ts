import { Component } from '@angular/core';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { CompaniesMainSearchBoxComponent } from "../../../../components/layout/companies-main-search-box/companies-main-search-box.component";
import { CompaniesTableComponent } from "../../../../components/layout/admin/master/companies-table/companies-table.component";
import { Companie } from '../../../../types/Companie';

@Component({
  selector: 'app-companies-search-edit',
  imports: [AdminMasterNavbarComponent, CompaniesMainSearchBoxComponent, CompaniesTableComponent],
  templateUrl: './companies-search-edit.component.html',
  styleUrl: './companies-search-edit.component.sass'
})
export class CompaniesSearchEditComponent {
  titleUsersSearchInput: string = 'Empresas para edição';
  companies: Companie[] = [];
  typeActions: string = 'edit';

  onSearchCompanie(companies: Companie[]): void {
    this.companies = companies;
  }
}
