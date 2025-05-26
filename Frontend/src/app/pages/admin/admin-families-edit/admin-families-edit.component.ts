import { Component, OnInit } from '@angular/core';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { FamilyFormComponent } from "../../../components/forms/family-form/family-form.component";
import { FamilyService } from '../../../services/family.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Family } from '../../../types/Family';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-families-edit',
  imports: [
    SpinningComponent,
    AdminNavbarComponent,
    InfoTopComponent,
    FamilyFormComponent,
    CommonModule
],
  templateUrl: './admin-families-edit.component.html',
  styleUrl: './admin-families-edit.component.sass'
})
export class AdminFamiliesEditComponent implements OnInit {
  isLoading: boolean = false;
  familyForEditData: Family | null = null;

  //duplicate fields - 409
  nameDuplicate: boolean = false;
  emailDuplicate: boolean = false;
  phoneDuplicate: boolean = false;
  rgDuplicate: boolean = false;
  cpfDuplicate: boolean = false;

  constructor(
    private familyService: FamilyService,
    private router: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    const familyId = this.activatedRoute.snapshot.paramMap.get('familyId') ?? '0';
    this.familyService.getById(familyId).pipe(map(response => response.body))
    .subscribe({
      next: (family: Family | null) => {
        this.familyForEditData = family;
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 500) {
          this.toastr.error("Houve um erro ao buscar os dados do familiar, contate um administrador do sistema!");
          this.isLoading = false;
          this.router.navigate(['/admin/families-panel']); 
        } 
      }
    })
  }

  onFamilyData(family: FormData): void {
    this.nameDuplicate = false;
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
              case 'name':
                this.nameDuplicate = true;
                break;
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
