import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AdminMasterNavbarComponent } from '../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component';

@Component({
  selector: 'app-companies-panel',
  imports: [
    RouterModule,
    CommonModule,
    AdminMasterNavbarComponent
  ],
  templateUrl: './companies-panel.component.html',
  styleUrl: './companies-panel.component.sass'
})
export class CompaniesPanelComponent {
  userRole: number = 1;

  constructor(private authService: AuthService) {}

  async ngOnInit(): Promise<void> {
    this.userRole = await this.authService.getRole();
  }
}
