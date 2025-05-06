import { Component, EventEmitter, input, Input, Output } from '@angular/core';
import { SpinningComponent } from "../../spinning/spinning.component";
import { User } from '../../../../types/User';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../../services/user.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../services/auth.service';
import { formatDate } from '../../../../utils/FormatDate';

@Component({
  selector: 'app-admin-main-search-user-box',
  imports: [
    SpinningComponent,
    CommonModule
  ],
  templateUrl: './admin-main-search-user-box.component.html',
  styleUrl: './admin-main-search-user-box.component.sass'
})
export class AdminMainSearchUserBoxComponent {
  @Input() title: string = 'Buscar usuários';
  @Input() isLoading: boolean = false;
  @Input() dropdown: boolean = false;
  @Output() searchUser = new EventEmitter<User[]>();
  users: User[] = [];

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService
  ) { }

  search(event: Event): void {
    this.users = [];
    event.preventDefault();
    const inputElement = document.getElementById('searchInput') as HTMLInputElement;
    
    if (inputElement.value) {
      this.userService.search(inputElement.value).subscribe({
        next: (response: HttpResponse<User[]>) => {
          const users = response.body ?? [];
          
          users.forEach(user => {
            user.roleName = this.authService.getRoleName(user.role);
            user.lastAccess = formatDate(user.lastAccess);
          })
          this.users = users;
          
          if (users.length == 0) {
            this.toastr.info("Nenhum usuário encontrado!")
          }
          
          this.searchUser.emit(users);
        },
        error: (error) => {
          if (error.status == 500) {
            this.isLoading = false;
            this.toastr.error("Ocorreu um erro ao tentar buscar os usuários, contate um administrador do sistema!");
            this.router.navigate(['/users']);
          }
        }
      })
    } else {
      this.isLoading = false;
    }
  }

  openUserDetails(userId: number): void {
    this.router.navigate([`/admin/users-details/${userId}`]);
  }
}
