import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ModalConfirmComponent } from '../../modal-confirm/modal-confirm.component';
import { SpinningComponent } from '../../spinning/spinning.component';
import { Family } from '../../../../types/Family';
import { FamilyService } from '../../../../services/family.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin-families-table',
  imports: [
  CommonModule,
    ModalConfirmComponent,
    SpinningComponent
  ],
  templateUrl: './admin-families-table.component.html',
  styleUrl: './admin-families-table.component.sass'
})
export class AdminFamiliesTableComponent {
  isLoading: boolean = false;
  @Input() families: Family[] = [];
  @Input() typeActions: string = 'default';
  modalDeleteMessage: string = "Tem certeza de que deseja desativar este familiar?";
  imageProfile: string = '/perfil-photo.png';
  familyId: number = 0;

  isModalOpen: boolean = false;

  constructor(
    private FamilyService: FamilyService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  openModal(userId: number): void {
    this.isModalOpen = true;
    this.familyId = userId;
  }

  onConfirmDelete(confirm: boolean): void {
    this.isLoading = true;
    this.isModalOpen = false;
    if (confirm) {
      this.FamilyService.delete(this.familyId).subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status == 204) {
            this.toastr.success("Familiar desabilitado com sucesso!");
            this.families = [...this.families.filter(family => family.id !== this.familyId)];
          } else {
            this.toastr.info("Uma resposta inesperada foi retornada pelo sistema, contate o suporte técnico!");
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.isLoading = false;
          this.toastr.error("Houve um erro ao tentar desabilitar um usuário, contate o suporte técnico!");
        }
      })
    }
    this.isLoading = false;
  }

  goToEditPage(userId: number): void {
    this.router.navigate([`/admin/families-edit/${userId}`]);
  }

  openUserDetails(userId: number): void {
    this.router.navigate([`/admin/families-details/${userId}`]);
  }
}
