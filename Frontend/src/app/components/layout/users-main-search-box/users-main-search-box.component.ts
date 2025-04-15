import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SpinningComponent } from "../spinning/spinning.component";
import { HttpResponse } from '@angular/common/http';
import { User } from '../../../types/User';
import { UserService } from '../../../services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users-main-search-box',
  imports: [SpinningComponent],
  templateUrl: './users-main-search-box.component.html',
  styleUrl: './users-main-search-box.component.sass'
})
export class UsersMainSearchBoxComponent {
  @Input() title: string = 'Buscar usuários';
  @Input() isLoading: boolean = false;
  @Output() searchUser = new EventEmitter<User[]>();

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  search(event: Event): void {
    event.preventDefault();
    const inputElement = document.getElementById('searchInput') as HTMLInputElement;

    if (inputElement.value) {
      this.isLoading = true;
      this.userService.search(inputElement.value).subscribe({
        next: (response: HttpResponse<User[]>) => {
          const users = response.body ?? [];

          if (users.length == 0) {
            this.toastr.info("Nenhum usuário encontrado!")
          }
          
          this.searchUser.emit(users);
          // this.isLoading = false;
        },
        error: (error) => {
          if (error.status == 500) {
            this.isLoading = false;
            this.toastr.error("Ocorreu um erro ao tentar buscar os usuários, contate um administrador do sistema!");
            this.router.navigate(['/admin/master/users-panel']);
          }
        }
      })
    }
  }
}
