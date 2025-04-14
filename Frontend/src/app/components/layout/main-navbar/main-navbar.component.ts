import { afterNextRender, Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-main-navbar',
  standalone: true,
  imports: [],
  templateUrl: './main-navbar.component.html',
  styleUrl: './main-navbar.component.sass'
})
export class MainNavbarComponent {
  admin: boolean = false;
  moderador: boolean = false;
  user: boolean = false;
  callsDropdownOpen = false;
  @ViewChild('callsToggle') callsToggleElement!: ElementRef;

  constructor(
    private authService: AuthService
  ) {
    afterNextRender(() => {
      const userRole = this.authService.getRole();

      try {
        switch (userRole) {
          case 0:
            this.admin = true;
            break;
          case 1:
            this.user = true;
            break;
          case 2:
            this.moderador = true;
            break;
        }
      } catch (error) {
        console.error("Erro ao obter a role do usuário:", error);
      }
    })
  }

  ngOnInit(): void {
    
  }

  logout(): void {
    this.authService.logout();
  }

  callsToggleDropdown() {
    this.callsDropdownOpen = !this.callsDropdownOpen;
    this.callsToggleElement.nativeElement.classList.toggle('selected-li');
  }
}
