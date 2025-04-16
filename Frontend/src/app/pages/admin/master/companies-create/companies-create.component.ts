import { Component } from '@angular/core';
import { InputErrorMessageComponent } from "../../../../components/layout/input-error-message/input-error-message.component";
import { SpinningComponent } from "../../../../components/layout/spinning/spinning.component";
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanieService } from '../../../../services/companie.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { nameValidators } from '../../../../validators/nameValidator';
import { cnpjValidator } from '../../../../validators/cnpjValidator';
import { Companie, CreateCompanie, ErrorResponseCreateCompanie } from '../../../../types/Companie';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-companies-create',
  imports: [
  InputErrorMessageComponent,
  SpinningComponent,
  AdminMasterNavbarComponent,
  ReactiveFormsModule
  ],
  templateUrl: './companies-create.component.html',
  styleUrl: './companies-create.component.sass'
})
export class CompaniesCreateComponent {
  isLoading: boolean = false;
  companieForm: FormGroup;

  nameErrors: string[] = [];
  cnpjErrors: string[] = [];
  connectionStringErrors: string[] = [];

  constructor(
    private companieService: CompanieService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.companieForm = this.fb.group({
      name: ['', [Validators.required, nameValidators()]],
      cnpj: ['', [Validators.required, cnpjValidator]],
      connectionString: ['', Validators.required]
    })
  }

  submit(): void {
    if (this.companieForm.invalid) {
      this.getNameErrors();
      this.getCnpjErrors();
      this.getConnectionErrors();
    } else {
      this.isLoading = true;
      const companie: CreateCompanie = this.companieForm.value as CreateCompanie;

      this.companieService.create(companie).subscribe({
        next: (response: HttpResponse<Companie>) => {
          this.toastr.success(`A empresa ${response.body?.name} foi criada com sucesso!`);
          this.router.navigate(['/admin/master/companies-panel']);
          this.isLoading = false;
        },
        error: (error: ErrorResponseCreateCompanie) => {
          console.log(error);
          if (error.error.code == 409) {
            this.isLoading = false;
            if (error.error.type == 'name') {
              this.nameErrors = ['Este nome já foi registrado, tente outro nome!'];
            } else {
              this.cnpjErrors = ['Este CNPJ já foi registrado, tente outro CNPJ!'];
            }
          } else {
            this.toastr.error("Houve um erro ao tentar criar uma nova empresa, procure o administrador do sistema!");
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
