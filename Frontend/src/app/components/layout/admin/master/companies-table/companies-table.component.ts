import { Component, Input } from '@angular/core';
import { Companie } from '../../../../../types/Companie';
import { CompanieService } from '../../../../../services/companie.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ModalConfirmComponent } from "../../../modal-confirm/modal-confirm.component";
import { SpinningComponent } from "../../../spinning/spinning.component";

@Component({
  selector: 'app-companies-table',
  imports: [CommonModule, ModalConfirmComponent, SpinningComponent],
  templateUrl: './companies-table.component.html',
  styleUrl: './companies-table.component.sass'
})
export class CompaniesTableComponent {
  @Input() companies: Companie[] = [];
  @Input() typeActions: string = 'default';
  modalDeleteMessage: string = "Tem certeza de que deseja desativar essa empresa?";
  companieId: number = 0;
  isLoading: boolean = false;

  isModalOpen: boolean = false;

  constructor (
    private companieService: CompanieService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  openModal(companieId: number): void {
    this.isModalOpen = true;
    this.companieId = companieId;
  }

  onConfirmDelete(confirm: boolean): void {
    this.isLoading = true;
    this.isModalOpen = false;
    if (confirm) {
      this.companieService.delete(this.companieId).subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status == 204) {
            this.toastr.success("Usuário desabilitado com sucesso!");
            this.isLoading = false
            this.companies = [...this.companies.filter(companie => companie.id !== this.companieId)];
          } else {
            this.toastr.info("Uma resposta inesperada foi retornada pelo sistema, contate um administrador do sistema!");
            this.isLoading = false;
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.isLoading = false;
          this.toastr.error("Houve um erro ao tentar desabilitar uma empresa, contate um administrador do sistema!");
        }
      })
    }
    this.isLoading = false;
  }

  goToEditPage(companieId: number): void {
    this.router.navigate([`/admin/master/companies-edit/${companieId}`]);
  }
}
