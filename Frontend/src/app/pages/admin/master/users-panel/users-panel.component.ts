import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { InfoTopComponent } from "../../../../components/layout/info-top/info-top.component";

@Component({
  selector: 'app-users-panel',
  imports: [
    RouterModule,
    CommonModule,
    AdminMasterNavbarComponent,
    InfoTopComponent
],
  templateUrl: './users-panel.component.html',
  styleUrl: './users-panel.component.sass'
})
export class UsersPanelComponent implements OnInit {
  userRole: number = 1;

  constructor(private authService: AuthService) {}

  async ngOnInit(): Promise<void> {
    this.userRole = await this.authService.getRole();
  }
}
