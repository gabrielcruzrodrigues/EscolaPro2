import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SpinningComponent } from '../../../../components/layout/spinning/spinning.component';
import { AdminMasterNavbarComponent } from "../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component";
import { UserService } from '../../../../services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CreateUser, ErrorResponseCreateUser, ResponseCreateUser } from '../../../../types/User';
import { InputErrorMessageComponent } from "../../../../components/layout/input-error-message/input-error-message.component";
import { nameValidators } from '../../../../validators/nameValidator';
import { passwordValidator } from '../../../../validators/passwordValidator';
import { Companie } from '../../../../types/Companie';
import { CompanieService } from '../../../../services/companie.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-users-create',
  imports: [
    // InputErrorMessageComponent,
    ReactiveFormsModule,
    SpinningComponent,
    AdminMasterNavbarComponent,
    InputErrorMessageComponent
],
  templateUrl: './users-create.component.html',
  styleUrl: './users-create.component.sass'
})
export class UsersCreateComponent implements OnInit {
  userForm: FormGroup;
  title: string = 'Users';
  companies: Companie[] = [];

  //variable for control spinning
  isLoading: boolean = false;

  //variables for change the visibility of the password and passwordVarify inputs
  passwordVisible: boolean = false;

  //variable for validation password
  wrongPassword: boolean = false;

  //arrays with errors 
  nameErrors: string[] = [];
  emailErrros: string[] = [];
  passwordErrros: string[] = [];
  passwordVerifyErrors: string[] = [];
  companiesErrors: string[] = [];

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private companieService: CompanieService
  ) {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, nameValidators()]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, passwordValidator()]],
      passwordVerify: ['', Validators.required],
      role: [2, Validators.required],
      companieId: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.companieService.getAll().subscribe({
      next: (response: HttpResponse<Companie[]>) => {
        if (response.status === 200) {
          response.body?.forEach(companie => {
            this.companies.push(companie);
          })
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 500) {
          this.toastr.error("Houve um erro ao tentar buscar as empresas, procure o administrador do sistema!");
          this.isLoading = false;
          this.router.navigate(['/admin/master/users-panel'])
          return;
        }
      }
    })
  }

  Submit(): void {
    console.log(this.userForm.get('companieId')?.value)
    if (this.userForm.invalid || !this.verifyPassword()) {
      this.getNameErrors();
      this.getEmailErrors();
      this.getPasswordErrors();
      this.getPasswordVerifyErrors();
      this.getCompaniesErrors();
    } else {
      this.isLoading = true;
      const user: CreateUser = this.userForm.value as CreateUser;
      user.role = Number(user.role);

      this.userService.create(user).subscribe({
        next: (response: ResponseCreateUser) => {
          this.toastr.success(`O usuário ${response.name} foi criado com sucesso!`);
          this.router.navigate(['/admin/master/users-panel']);
          this.isLoading = false;
        },
        error: (error: ErrorResponseCreateUser) => {
          console.log(error);
          if (error.error.code == 409) {
            this.isLoading = false;
            if (error.error.type == 'name') {
              this.nameErrors = ['Este nome já foi registrado, tente outro nome!'];
            } else {
              this.emailErrros = ['Este email já foi registrado, tente outro email!'];
            }
          } else {
            this.toastr.error("Houve um erro ao tentar criar um novo usuário, procure o administrador do sistema!");
            console.log(error);
            this.isLoading = false;
            this.router.navigate(['/users'])
            return;
          }
        }
      })
    }
  }

  verifyPassword(): boolean {
    const password = this.userForm.get('password')?.value;
    const verifyPassword = this.userForm.get('passwordVerify')?.value;

    if (password !== verifyPassword) {
      this.wrongPassword = true;
      return false;
    }

    return true;
  }

  clearInputErrors(option: string): void {
    if (option == 'name') {
      this.nameErrors = [];
    }

    if (option == 'email') {
      this.emailErrros = [];
    }

    if (option == 'password') {
      this.passwordErrros = [];
    }

    if (option == 'passwordVerify') {
      this.wrongPassword = false;
      this.passwordVerifyErrors = [];
    }
  }

  getNameErrors(): void {
    const nameControl = this.userForm.get('name');
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

  getCompaniesErrors(): void {
    const companieControl = this.userForm.get('companieId');
    const errors: string[] = [];

    if (companieControl && companieControl.value === '') {
      errors.push('A empresa é obrigatória.');
      this.companiesErrors = errors;
      return;
    }
  }

  getEmailErrors(): void {
    const emailControl = this.userForm.get('email');
    const errors: string[] = [];

    if (emailControl?.hasError('required')) {
      errors.push('O email é obrigatório.');
      this.emailErrros = errors;
      return;
    }

    if (emailControl?.hasError('email')) {
      errors.push('Por favor, insira um email válido. Sem conter espaços antes ou depois!');
      this.emailErrros = errors;
      return;
    }
  }

  getPasswordErrors(): void {
    const passwordControl = this.userForm.get('password');
    const passwordVerifyControl = this.userForm.get('passwordVerify');
    const errors: string[] = [];

    if (passwordControl?.hasError('required')) {
      errors.push('A senha é obrigatório.');
      this.passwordErrros = errors;
      return;
    }

    if (passwordControl?.hasError('passwordTooShort')) {
      errors.push('A senha deve conter pelo menos 8 caracteres.');
      this.passwordErrros = errors;
      return;
    }

    if (passwordControl?.hasError('missingLetter')) {
      errors.push('A senha deve conter pelo menos 1 letra.');
      this.passwordErrros = errors;
      return;
    }

    if (passwordControl?.hasError('missingNumber')) {
      errors.push('A senha deve conter pelo menos 1 número.');
      this.passwordErrros = errors;
      return;
    }
  }

  getPasswordVerifyErrors(): void {
    const passwordVerifyControl = this.userForm.get('passwordVerify');
    const errors: string[] = [];

    if (passwordVerifyControl?.hasError('required')) {
      errors.push('A verificação de senha é obrigatória!');
      this.passwordVerifyErrors = errors;
      return;
    }

    this.verifyPassword();

    if (this.wrongPassword) {
      errors.push('As senhas não são iguais!');
      this.passwordVerifyErrors = errors;
      return;
    }
  }
}
