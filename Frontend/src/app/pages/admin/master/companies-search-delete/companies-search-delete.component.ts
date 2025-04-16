import { Component } from '@angular/core';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { CompaniesMainSearchBoxComponent } from "../../../../components/layout/companies-main-search-box/companies-main-search-box.component";
import { CompaniesTableComponent } from "../../../../components/layout/admin/master/companies-table/companies-table.component";
import { Companie } from '../../../../types/Companie';

@Component({
  selector: 'app-companies-search-delete',
  imports: [
    AdminMasterNavbarComponent, 
    CompaniesMainSearchBoxComponent, 
    CompaniesTableComponent
  ],
  templateUrl: './companies-search-delete.component.html',
  styleUrl: './companies-search-delete.component.sass'
})
export class CompaniesSearchDeleteComponent {
  titleUsersSearchInput: string = 'Buscar empresas para desativar';
  companies: Companie[] = [];
  typeActions: string = 'trash';

  onSearchCompanie(companies: Companie[]): void {
    this.companies = companies;
  }
}
