import { CommonModule } from '@angular/common';
import { afterNextRender, AfterViewInit, ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-admin-navbar',
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './admin-navbar.component.html',
  styleUrl: './admin-navbar.component.sass'
})
export class AdminNavbarComponent implements OnInit, AfterViewInit {
  // Responsividade da navbar
  isMobile = false;
  isMenuOpen = false;

  currentRoute: string = '';

  //roles para liberar funções
  admin: boolean = false;


  constructor(
    private authService: AuthService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private ngZone: NgZone
  ) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.url;
      }
    });

    afterNextRender(() => {
      // const userRole = this.authService.getRole();
      const userRole: number = 0;

      try {
        switch (userRole) {
          case 0:
            this.admin = true;
            break;
          // case 2:
          //   this.moderador = true;
          //   break;
        }
        this.cdr.detectChanges();
      } catch (error) {
        console.error("Erro ao obter a role do usuário:", error);
      }
    })
  }

  ngOnInit(): void {
    this.checkWindowSize();
  }

  ngAfterViewInit() {
    window.addEventListener('resize', () => {
      this.ngZone.run(() => this.checkWindowSize());
    });
  }

  logout(): void {
    this.authService.logout();
  }

  // callsToggleDropdown() {
  //   this.callsDropdownOpen = !this.callsDropdownOpen;
  //   this.callsToggleElement.nativeElement.classList.toggle('selected-li');
  // }

  checkWindowSize() {
    this.isMobile = window.innerWidth <= 1024;
    if (!this.isMobile) {
      this.isMenuOpen = true; // manter o menu aberto no desktop
    }
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
