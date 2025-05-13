import { Component } from '@angular/core';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { FamilyFormComponent } from "../../../components/forms/family-form/family-form.component";
import { Family } from '../../../types/Family';

@Component({
  selector: 'app-admin-families-create',
  imports: [SpinningComponent, AdminNavbarComponent, InfoTopComponent, FamilyFormComponent],
  templateUrl: './admin-families-create.component.html',
  styleUrl: './admin-families-create.component.sass'
})
export class AdminFamiliesCreateComponent {
  isLoading: boolean = false;

  onFamilyData(family: Family): void {
    console.log(family);
  }
}
