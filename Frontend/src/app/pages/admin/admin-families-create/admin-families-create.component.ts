import { Component } from '@angular/core';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { FamilyFormComponent } from "../../../components/forms/family-form/family-form.component";
import { CreateFamily, Family } from '../../../types/Family';
import { FamilyService } from '../../../services/family.service';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-families-create',
  imports: [SpinningComponent, AdminNavbarComponent, InfoTopComponent, FamilyFormComponent],
  templateUrl: './admin-families-create.component.html',
  styleUrl: './admin-families-create.component.sass'
})
export class AdminFamiliesCreateComponent {
  isLoading: boolean = false;

  //duplicate fields - 409
  emailDuplicate: boolean = false;
  phoneDuplicate: boolean = false;
  rgDuplicate: boolean = false;
  cpfDuplicate: boolean = false;

  constructor(
    private familyService: FamilyService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  onFamilyData(family: FormData): void {
    this.emailDuplicate = false;
    this.phoneDuplicate = false;
    this.rgDuplicate = false;
    this.cpfDuplicate = false;

    this.isLoading = true;
    this.familyService.create(family).subscribe({
      next: (response: HttpResponse<Family>) => {
        this.isLoading = false;
        if (response.status === 201) {
          this.toastr.success("Familiar criado com sucesso!");
          this.router.navigate(['/admin/families-panel']);
          return;
        }

        this.toastr.info("Uma responsta inesperada foi enviada pelo servidor, contate o suporte técnico!");
        return;
      },
      error: (error: HttpErrorResponse) => {
        this.isLoading = false;
        console.log(error);

        if (error.status === 500) {
          this.toastr.error("Um erro desconhecido aconteceu, contate o suporte técnico!");
          this.router.navigate(['/admin/families-panel']);
          return;
        }

        if (error.status === 409) {
          (error.error.fields as string[]).forEach((field: string) => {
            switch (field) {
              case 'email':
                this.emailDuplicate = true;
                break;
              case 'phone':
                this.phoneDuplicate = true;
                break;
              case 'rg':
                this.rgDuplicate = true;
                break;
              case 'cpf':
                this.cpfDuplicate = true;
                break;
            }
          })
        }
      }
    })
  }
}

