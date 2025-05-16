import { Component, ElementRef, ViewChild } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { Family } from '../../../types/Family';
import { FamilyService } from '../../../services/family.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { formatDate } from '../../../utils/FormatDate';
import { AdminFamiliesTableComponent } from "../../../components/layout/admin/admin-families-table/admin-families-table.component";

@Component({
  selector: 'app-admin-families-show',
  imports: [AdminNavbarComponent, SpinningComponent, InfoTopComponent, AdminFamiliesTableComponent],
  templateUrl: './admin-families-show.component.html',
  styleUrl: './admin-families-show.component.sass'
})
export class AdminFamiliesShowComponent {
  isLoading: boolean = false;
  families: Family[] = [];
  title: string = 'Families';

  constructor(
    private familyService: FamilyService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  orderNameListAZToggle: boolean = false;
  nameButtonOrderListAZ: string = 'Ordenar Nome A - Z';
  @ViewChild('nameOrderAZ') nameOrderAZ!: ElementRef;

  orderEmailListAZToggle: boolean = false;
  emailButtonOrderListAZ: string = 'Ordenar Email A - Z';
  @ViewChild('emailOrderAZ') emailOrderAZ!: ElementRef;

  ngOnInit(): void {
    this.familyService.getAll().subscribe({
      next: (response: HttpResponse<Family[]>) => {
        this.isLoading = false;

        response.body?.forEach(family => {
          family.createdAt = formatDate(family.createdAt);
          this.families.push(family);
        });
      },
      error: (error) => {
        this.isLoading = false;

        if (error.status == 500) {
          this.toastr.error("Ocorreu um erro ao tentar buscar os familiares, contate o suporte técnico!");
          this.router.navigate(['/admin/families']);
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
          this.families.sort((a, b) => a.name.localeCompare(b.name));
        } else {
          this.orderNameListAZToggle = false;
          this.nameButtonOrderListAZ = 'Ordenar Nome A - Z';
          this.families.sort((a, b) => b.name.localeCompare(a.name));
        }
        break;

      case "email":
        if (!this.orderEmailListAZToggle) {
          this.orderEmailListAZToggle = true;
          this.emailButtonOrderListAZ = 'Ordenar Email por Z - A';
          this.families.sort((a, b) => a.email.localeCompare(b.email));
        } else {
          this.orderEmailListAZToggle = false;
          this.emailButtonOrderListAZ = 'Ordenar Email A - Z';
          this.families.sort((a, b) => b.email.localeCompare(a.email));
        }
        break;
    }
  }
}
