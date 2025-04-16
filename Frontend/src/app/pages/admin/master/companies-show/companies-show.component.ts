import { Component, ElementRef, ViewChild } from '@angular/core';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { SpinningComponent } from "../../../../components/layout/spinning/spinning.component";
import { CompaniesTableComponent } from "../../../../components/layout/admin/master/companies-table/companies-table.component";
import { Companie } from '../../../../types/Companie';
import { CompanieService } from '../../../../services/companie.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpResponse } from '@angular/common/http';
import { formatDate } from '../../../../utils/FormatDate';

@Component({
  selector: 'app-companies-show',
  imports: [AdminMasterNavbarComponent, SpinningComponent, CompaniesTableComponent],
templateUrl: './companies-show.component.html',
  styleUrl: './companies-show.component.sass'
})
export class CompaniesShowComponent {
  isLoading: boolean = true;
  companies: Companie[] = [];
  title: string = 'Users';

  orderNameListAZToggle: boolean = true;
  nameButtonOrderListAZ: string = 'Ordenar Nome por A - Z';
  @ViewChild('nameOrderAZ') nameOrderAZ!: ElementRef;

  // orderRoleListAZToggle: boolean = false;
  // @ViewChild('roleOrderAZ') roleOrderAZ!: ElementRef;

  constructor(
    private companieService: CompanieService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.companieService.getAll().subscribe({
      next: (response: HttpResponse<Companie[]>) => {
        this.isLoading = false;

        response.body?.forEach(user => {
          user.createdAt = formatDate(user.createdAt);
          this.companies.push(user);
        });
      },
      error: (error) => {
        console.log(error);
        this.isLoading = false;

        if (error.status == 500) {
          this.toastr.error("Ocorreu um erro ao tentar buscar os empresas, contate um administrador do sistema!");
          this.router.navigate(['/admin/master/companies-show']);
        }
      }
    })
  }

  orderListAZ(option: string): void {

    switch (option) {
      case "name":
        if (!this.orderNameListAZToggle) {
          this.orderNameListAZToggle = true;
          this.nameButtonOrderListAZ = 'Ordenar Nome por Z - A';
          this.companies.sort((a, b) => a.name.localeCompare(b.name));
        } else {
          this.orderNameListAZToggle = false;
          this.nameButtonOrderListAZ = 'Ordenar Nome por A - Z';
          this.companies.sort((a, b) => b.name.localeCompare(a.name));
        }
        break;
    }
  }
}
