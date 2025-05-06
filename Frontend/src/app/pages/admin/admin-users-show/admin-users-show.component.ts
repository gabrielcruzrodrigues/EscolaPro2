import { Component, ElementRef, ViewChild } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminUsersTableComponent } from "../../../components/layout/admin/admin-users-table/admin-users-table.component";
import { UserService } from '../../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { User } from '../../../types/User';
import { HttpResponse } from '@angular/common/http';
import { formatDate } from '../../../utils/FormatDate';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";

@Component({
  selector: 'app-admin-users-show',
  imports: [
    AdminNavbarComponent,
    SpinningComponent,
    AdminUsersTableComponent,
    InfoTopComponent
],
  templateUrl: './admin-users-show.component.html',
  styleUrl: './admin-users-show.component.sass'
})
export class AdminUsersShowComponent {
  isLoading: boolean = true;
  users: User[] = [];
  title: string = 'Users';

  orderNameListAZToggle: boolean = false;
  nameButtonOrderListAZ: string = 'Ordenar Nome A - Z';
  @ViewChild('nameOrderAZ') nameOrderAZ!: ElementRef;

  orderEmailListAZToggle: boolean = false;
  emailButtonOrderListAZ: string = 'Ordenar Email A - Z';
  @ViewChild('emailOrderAZ') emailOrderAZ!: ElementRef;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.userService.getAll().subscribe({
      next: (response: HttpResponse<User[]>) => {
        this.isLoading = false;

        response.body?.forEach(user => {
          user.createdAt = formatDate(user.createdAt);
          this.users.push(user);
        });
      },
      error: (error) => {
        console.log(error);
        this.isLoading = false;

        if (error.status == 500) {
          this.toastr.error("Ocorreu um erro ao tentar buscar os usuários, contate um administrador do sistema!");
          this.router.navigate(['/users']);
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
          this.users.sort((a, b) => a.name.localeCompare(b.name));
        } else {
          this.orderNameListAZToggle = false;
          this.nameButtonOrderListAZ = 'Ordenar Nome A - Z';
          this.users.sort((a, b) => b.name.localeCompare(a.name));
        }
        break;

      case "email":
        if (!this.orderEmailListAZToggle) {
          this.orderEmailListAZToggle = true;
          this.emailButtonOrderListAZ = 'Ordenar Email por Z - A';
          this.users.sort((a, b) => a.email.localeCompare(b.email));
        } else {
          this.orderEmailListAZToggle = false;
          this.emailButtonOrderListAZ = 'Ordenar Email A - Z';
          this.users.sort((a, b) => b.email.localeCompare(a.email));
        }
        break;
    }
  }
}
