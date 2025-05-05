import { Component, Input, OnInit } from '@angular/core';
import { User } from '../../../../types/User';
import { UserService } from '../../../../services/user.service';
import { response } from 'express';
import { HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../services/auth.service';
import { formatDate } from '../../../../utils/FormatDate';

@Component({
  selector: 'app-admin-last-active-users',
  imports: [],
  templateUrl: './admin-last-active-users.component.html',
  styleUrl: './admin-last-active-users.component.sass'
})
export class AdminLastActiveUsersComponent implements OnInit {
  users: User[] = [];

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private authService: AuthService
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
        }
      },
      error: (error) => {
        this.toastr.error("Houve um erro ao tentar buscar o histórico de acesso de usuários, contate um administrador do sistema!");
      }
    })
  }
}
