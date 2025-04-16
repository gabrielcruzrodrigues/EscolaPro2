import { Component, OnInit } from '@angular/core';
import { InputErrorMessageComponent } from '../../../../components/layout/input-error-message/input-error-message.component';
import { SpinningComponent } from '../../../../components/layout/spinning/spinning.component';
import { AdminMasterNavbarComponent } from '../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Companie, CreateCompanie, ErrorResponseCreateCompanie, UpdateCompanie } from '../../../../types/Companie';
import { HttpResponse } from '@angular/common/http';
import { cnpjValidator } from '../../../../validators/cnpjValidator';
import { CompanieService } from '../../../../services/companie.service';
import { ToastrService } from 'ngx-toastr';
import { nameValidators } from '../../../../validators/nameValidator';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-companies-edit',
  imports: [
    InputErrorMessageComponent,
    SpinningComponent,
    AdminMasterNavbarComponent,
    ReactiveFormsModule
  ],
  templateUrl: './companies-edit.component.html',
  styleUrl: './companies-edit.component.sass'
})
export class CompaniesEditComponent implements OnInit {
  isLoading: boolean = false;
  companieForm: FormGroup;

  nameErrors: string[] = [];
  cnpjErrors: string[] = [];
  connectionStringErrors: string[] = [];

  constructor(
    private companieService: CompanieService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) {
    this.companieForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, nameValidators()]],
      cnpj: ['', [Validators.required, cnpjValidator]],
      connectionString: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('companieId') ?? '0';
    this.companieService.getById(id).pipe(map(response => response.body))
      .subscribe({
        next: (companie: Companie | null) => {
          if (companie) {
            this.companieForm.patchValue({
              id: companie.id,
              name: companie.name,
              cnpj: companie.cnpj,
              connectionString: companie.connectionString
            });
            this.isLoading = false;
          }
        },
        error: (error) => {
          if (error.status === 500) {
            this.toastr.error("Houve um erro ao buscar os dados da empresa, contate um administrador do sistema!");
            this.isLoading = false;
            this.router.navigate(['/users']);
          }
        }
      })
  }

  submit(): void {
    if (this.companieForm.invalid) {
      this.getNameErrors();
      this.getCnpjErrors();
      this.getConnectionErrors();
    } else {
      this.isLoading = true;
      const user: UpdateCompanie = this.companieForm.value as UpdateCompanie;

      this.companieService.update(user).subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status === 204) {
            this.toastr.success(`A empresa foi atualizada com sucesso!`);
            this.router.navigate(['/admin/master/companies-panel']);
            this.isLoading = false;
          } else {
            this.toastr.info("Uma resposta inesperada foi recebida do servidor, contate um administrador do sistema!")
            this.isLoading = false;
          }
        },
        error: (error: ErrorResponseCreateCompanie) => {
          if (error.error.code == 409) {
            this.isLoading = false;
            if (error.error.type == 'name') {
              this.nameErrors = ['Este nome já foi registrado, tente outro nome!'];
            } else {
              this.cnpjErrors = ['Este CNPJ já foi registrado, tente outro CNPJ!'];
            }
          } else {
            this.toastr.error("Houve um erro ao tentar atualizar a empresa, procure o administrador do sistema!");
            console.log(error);
            this.isLoading = false;
            this.router.navigate(['/admin/master/companies-panel'])
            return;
          }
        }
      })
    }
  }

  clearInputErrors(option: string): void {
    if (option == 'name') {
      this.nameErrors = [];
    }

    if (option == 'cnpj') {
      this.cnpjErrors = [];
    }

    if (option == 'connection') {
      this.connectionStringErrors = [];
    }
  }

  getNameErrors(): void {
    const nameControl = this.companieForm.get('name');
    const errors: string[] = [];

    if (nameControl?.hasError('required')) {
      errors.push('O nome é obrigatório.');
      this.nameErrors = errors;
      return;
    }

    if (nameControl?.hasError('minLength')) {
      errors.push('O nome deve conter mais de 2 caracteres.');
      this.nameErrors = errors;
      return;
    }

    if (nameControl?.hasError('missingLetter')) {
      errors.push("O nome deve conter pelo menos uma letra.");
      this.nameErrors = errors;
      return;
    }
  }

  getCnpjErrors(): void {
    const cnpjControl = this.companieForm.get('cnpj');
    const errors: string[] = [];

    if (cnpjControl?.hasError('required')) {
      errors.push('O CNPJ é obrigatório.');
      this.cnpjErrors = errors;
      return;
    }

    if (cnpjControl?.hasError('cnpjInvalid')) {
      errors.push('Este CNPJ é inválido.');
      this.cnpjErrors = errors;
      return;
    }
  }

  getConnectionErrors(): void {
    const connectionControl = this.companieForm.get('connectionString');
    const errors: string[] = [];

    if (connectionControl?.hasError('required')) {
      errors.push('A string de conexão é obrigatória.');
      this.connectionStringErrors = errors;
      return;
    }
  }
}
