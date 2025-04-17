import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-auth-loading',
  imports: [],
  templateUrl: './auth-loading.component.html',
  styleUrl: './auth-loading.component.sass'
})
export class AuthLoadingComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const role = this.authService.getRole();
    const next = this.route.snapshot.queryParamMap.get('next') || '/login';

    switch (role) {
      case 0: //admin
        this.authService.tokenVerify().subscribe({
          next: () => {
            setTimeout(() => {
              this.router.navigate([next]);
            }, 1000);
          },
          error: () => {
            setTimeout(() => {
              this.router.navigate(['/login'])
            }, 1000);
          }
        });
        break;
      default:
        this.router.navigate(['/login'])
    }
  }
}
