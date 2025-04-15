import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SpinningComponent } from '../../../../components/layout/spinning/spinning.component';
import { AdminMasterNavbarComponent } from '../../../../components/layout/admin/master/admin-master-navbar/admin-master-navbar.component';
import { InputErrorMessageComponent } from '../../../../components/layout/input-error-message/input-error-message.component';
import { Companie } from '../../../../types/Companie';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { CompanieService } from '../../../../services/companie.service';
import { ActivatedRoute, Router } from '@angular/router';
import { nameValidators } from '../../../../validators/nameValidator';
import { passwordValidator } from '../../../../validators/passwordValidator';
import { HttpResponse } from '@angular/common/http';
import { ErrorResponseCreateUser, UpdateUser, User } from '../../../../types/User';
import { map } from 'rxjs';

@Component({
  selector: 'app-users-edit',
  imports: [
    ReactiveFormsModule,
    SpinningComponent,
    AdminMasterNavbarComponent,
    InputErrorMessageComponent
  ],
  templateUrl: './users-edit.component.html',
  styleUrl: './users-edit.component.sass'
})
export class UsersEditComponent implements OnInit {
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
    private companieService: CompanieService,
    private activatedRoute: ActivatedRoute
  ) {
    this.userForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, nameValidators()]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', passwordValidator()],
      passwordVerify: [''],
      role: [2, Validators.required],
      companieId: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('userId') ?? '0';
    this.userService.getById(id).pipe(map(response => response.body))
    .subscribe({
      next: (user: User | null) => {
        if (user) {
          this.userForm.patchValue({
            id: user.id,
            name: user.name,
            email: user.email,
            role: user.role,
            companieId: user.companieId
          });
          this.isLoading = false;
        }
      },
      error: (error) => {
        if (error.status === 500) {
          this.toastr.error("Houve um erro ao buscar os dados do usuário, contate um administrador do sistema!");
          this.isLoading = false;
          this.router.navigate(['/users']); 
        } 
      }
    })

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
    console.log(this.userForm.value)
    if (this.userForm.invalid || !this.verifyPassword()) {
      this.getNameErrors();
      this.getEmailErrors();
      this.getPasswordErrors();
      this.getPasswordVerifyErrors();
      this.getCompaniesErrors();
    } else {
      alert('oi')
      this.isLoading = true;
      const user: UpdateUser = this.userForm.value as UpdateUser;

      this.userService.update(user).subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status === 204) {
            this.toastr.success(`O usuário foi atualizado com sucesso!`);
            this.router.navigate(['/admin/master/users-panel']);
            this.isLoading = false;
          } else {
            this.toastr.info("Uma resposta inesperada foi recebida do servidor, contate um administrador do sistema!")
          }
        },
        error: (error: ErrorResponseCreateUser) => {
          console.log(error);
          if (error.error.code == 409) {
            this.isLoading = false;
            if (error.error.type == 'name') {
              this.nameErrors = ['Este nome já foi registrado, tente outro nome!'];
            } else {
              this.emailErrros = ['Este CNPJ já foi registrado, tente outro CNPJ!'];
            }
          } else {
            this.toastr.error("Houve um erro ao tentar atualizar o usuário, procure o administrador do sistema!");
            console.log(error);
            this.isLoading = false;
            this.router.navigate(['/admin/master/user-panel'])
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
