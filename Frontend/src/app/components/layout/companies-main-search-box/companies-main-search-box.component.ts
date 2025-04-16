import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SpinningComponent } from "../spinning/spinning.component";
import { Companie } from '../../../types/Companie';
import { CompanieService } from '../../../services/companie.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-companies-main-search-box',
  imports: [SpinningComponent],
  templateUrl: './companies-main-search-box.component.html',
  styleUrl: './companies-main-search-box.component.sass'
})
export class CompaniesMainSearchBoxComponent {
  @Input() title: string = 'Buscar usuários';
  @Input() isLoading: boolean = false;
  @Output() searchCompanie = new EventEmitter<Companie[]>();

  constructor(
    private companieService: CompanieService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  search(event: Event): void {
    event.preventDefault();
    const inputElement = document.getElementById('searchInput') as HTMLInputElement;

    if (inputElement.value) {
      this.isLoading = true;
      this.companieService.search(inputElement.value).subscribe({
        next: (response: HttpResponse<Companie[]>) => {
          const companies = response.body ?? [];

          if (companies.length == 0) {
            this.toastr.info("Nenhum usuário encontrado!")
          }
          
          this.searchCompanie.emit(companies);
          this.isLoading = false;
        },
        error: (error) => {
          if (error.status == 500) {
            this.isLoading = false;
            this.toastr.error("Ocorreu um erro ao tentar buscar os empresas, contate um administrador do sistema!");
            this.router.navigate(['/admin/master/companies-panel']);
          }
        }
      })
    }
  }
}
