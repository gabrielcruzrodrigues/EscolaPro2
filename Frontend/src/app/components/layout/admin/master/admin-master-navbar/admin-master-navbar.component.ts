import { afterNextRender, AfterViewInit, ChangeDetectorRef, Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../../../../services/auth.service';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-master-navbar',
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './admin-master-navbar.component.html',
  styleUrl: './admin-master-navbar.component.sass'
})
export class AdminMasterNavbarComponent implements OnInit, AfterViewInit {
  admin: boolean = false;
  moderador: boolean = false;
  user: boolean = false;
  callsDropdownOpen = false;
  @ViewChild('callsToggle') callsToggleElement!: ElementRef;
  currentRoute: string = '';

  isMenuOpen = false;
  isMobile = false;

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
          case 2:
            this.moderador = true;
            break;
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

  callsToggleDropdown() {
    this.callsDropdownOpen = !this.callsDropdownOpen;
    this.callsToggleElement.nativeElement.classList.toggle('selected-li');
  }




  checkWindowSize() {
    this.isMobile = window.innerWidth <= 600;
    if (!this.isMobile) {
      this.isMenuOpen = true; // manter o menu aberto no desktop
    }
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
