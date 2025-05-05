import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { HttpResponse } from '@angular/common/http';
import { User } from '../../../types/User';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-info-top',
  imports: [],
  templateUrl: './info-top.component.html',
  styleUrl: './info-top.component.sass'
})
export class InfoTopComponent implements OnInit {
  username: string = 'User';
  role: string = 'role';
  profile: string = "/user.png";

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  async ngOnInit(): Promise<void> {
    const userId =  await this.authService.getId();
    this.userService.getById(userId).subscribe({
      next: (response: HttpResponse<User>) => {
        if (response.status == 200) {
          this.username = response.body?.name ?? "Undefined";
          const role = response.body?.role ?? 1;
          this.role = this.authService.getRoleName(role);
        }
      }, 
      error: (error) => {
        this.toastr.error("Houve um problema ao buscar os seus dados, faça login novamente!");
        this.router.navigate(["/login"]);
      }
    })
  }
}
