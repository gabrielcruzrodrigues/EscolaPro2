import { afterNextRender, Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../../../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-master-navbar',
  imports: [
    CommonModule
  ],
  templateUrl: './admin-master-navbar.component.html',
  styleUrl: './admin-master-navbar.component.sass'
})
export class AdminMasterNavbarComponent {
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

  logout(): void {
    this.authService.logout();
  }

  callsToggleDropdown() {
    this.callsDropdownOpen = !this.callsDropdownOpen;
    this.callsToggleElement.nativeElement.classList.toggle('selected-li');
  }
}
