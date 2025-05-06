import { Component, OnInit } from '@angular/core';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../types/User';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { formatDate } from '../../../utils/FormatDate';

@Component({
  selector: 'app-admin-users-details',
  imports: [AdminNavbarComponent, InfoTopComponent],
  templateUrl: './admin-users-details.component.html',
  styleUrl: './admin-users-details.component.sass'
})
export class AdminUsersDetailsComponent implements OnInit {
  profile: string = '/user-default.png';
  name: string = 'Usuário do sistema Escola Pro 2';
  email: string = 'usuario@example.com';
  role: string = 'Cargo teste';
  createdAt: string = '05-05-2025 - 05:22';
  lastAccess: string = '05-05-2025 - 05:22';
  companieName: string = 'Empresa Escola Pro 2';

  isLoading: boolean = false;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('userId') ?? '0';
    this.userService.getById(id).subscribe({
      next: (response: HttpResponse<User>) => {
        if (response.status == 200) {
          this.name = response.body?.name ?? this.name;
          this.email = response.body?.email ?? this.email;
          this.role = this.authService.getRoleName(response.body?.role ?? 1);
          this.createdAt = formatDate(response.body?.createdAt ?? this.createdAt);
          this.lastAccess = formatDate(response.body?.lastAccess ?? this.lastAccess);
          this.companieName = response.body?.companieName ?? this.companieName;
        }
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 404) {
          this.toastr.error("O usuário não foi encontrado, contate o suporte técnico!");
          this.isLoading = false;
          this.router.navigate(['/admin/users-panel']);
        } 
        if (error.status == 500) {
          this.toastr.error("Ocorreu um erro desconhecido, contate o suporte técnico!");
          this.isLoading = false;
          this.router.navigate(['/admin/users-panel']);
        }
      }
    })
  }
}
