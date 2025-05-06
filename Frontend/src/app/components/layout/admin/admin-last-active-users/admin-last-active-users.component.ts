import { Component, Input, OnInit } from '@angular/core';
import { User } from '../../../../types/User';
import { UserService } from '../../../../services/user.service';
import { response } from 'express';
import { HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../services/auth.service';
import { formatDate } from '../../../../utils/FormatDate';
import { SpinningComponent } from "../../spinning/spinning.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-last-active-users',
  imports: [SpinningComponent],
  templateUrl: './admin-last-active-users.component.html',
  styleUrl: './admin-last-active-users.component.sass'
})
export class AdminLastActiveUsersComponent implements OnInit {
  users: User[] = [];
  isLoading: boolean = true;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.lastActiveUsers().subscribe({
      next: (response: HttpResponse<User[]>) => {
        if (response.status == 200) {
          response.body?.forEach(user => {
            user.roleName = this.authService.getRoleName(user.role);
            user.lastAccess = formatDate(user.lastAccess);
          })

          this.users = response.body ?? [];
          this.isLoading = false;
        }
      },
      error: (error) => {
        this.toastr.error("Houve um erro ao tentar buscar o histórico de acesso de usuários, contate um administrador do sistema!");
        this.isLoading = false;
      }
    })
  }

  showUsersDetails(userId: number): void {
    this.router.navigate([`/admin/users-details/${userId}`]);
  }
}
