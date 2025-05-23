import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SpinningComponent } from "../spinning/spinning.component";
import { Family } from '../../../types/Family';
import { FamilyService } from '../../../services/family.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-families-main-search-box',
  imports: [
    SpinningComponent,
    CommonModule
  ],
  templateUrl: './families-main-search-box.component.html',
  styleUrl: './families-main-search-box.component.sass'
})
export class FamiliesMainSearchBoxComponent {
  @Input() title: string = 'Buscar familiares';
  @Input() isLoading: boolean = false;
  @Input() dropdown: boolean = false;
  @Output() searchFamily = new EventEmitter<Family[]>();
  families: Family[] = [];
  @Input() placeholder: string = "";

  constructor(
    private familyService: FamilyService,
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService
  ) { }

  search(event: Event): void {
    this.families = [];
    event.preventDefault();
    const inputElement = document.getElementById('searchInput') as HTMLInputElement;

    if (inputElement.value) {
      this.familyService.search(inputElement.value).subscribe({
        next: (response: HttpResponse<Family[]>) => {
          const families = response.body ?? [];
          this.families = families;

          if (families.length == 0) {
            this.toastr.info("Nenhum familiar encontrado!")
          }

          this.searchFamily.emit(families);
        },
        error: (error) => {
          if (error.status == 500) {
            this.isLoading = false;
            this.toastr.error("Ocorreu um erro ao tentar buscar os familiares, contate um administrador do sistema!");
            this.router.navigate(['/admin/families']);
          }
        }
      })
    } else {
      this.isLoading = false;
    }
  }

  openFamiliesDetails(familyId: number): void {
    this.router.navigate([`/admin/families-details/${familyId}`]);
  }
}
