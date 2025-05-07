import { Component } from '@angular/core';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InputErrorMessageComponent } from "../../../components/layout/input-error-message/input-error-message.component";
import { CreateUser, ErrorResponseCreateUser, ResponseCreateUser } from '../../../types/User';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Companie } from '../../../types/Companie';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { nameValidators } from '../../../validators/nameValidator';
import { passwordValidator } from '../../../validators/passwordValidator';
import { CompanieService } from '../../../services/companie.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { AuthService } from '../../../services/auth.service';
import { Role } from '../../../types/Role';
import { filterRoles } from '../../../utils/FilterRoles';

@Component({
  selector: 'app-admin-users-create',
  imports: [
  SpinningComponent,
    AdminNavbarComponent,
    InputErrorMessageComponent,
    ReactiveFormsModule,
    InfoTopComponent
],
  templateUrl: './admin-users-create.component.html',
  styleUrl: './admin-users-create.component.sass'
})
export class AdminUsersCreateComponent {
  userForm: FormGroup;
  title: string = 'Users';
  roles: Role[] = [];

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
    private authService: AuthService,
    private toastr: ToastrService,
    private companieService: CompanieService
  ) {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, nameValidators()]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, passwordValidator()]],
      passwordVerify: ['', Validators.required],
      role: ['', Validators.required],
      companieId: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    const companieId = this.authService.getCompanieId();
    this.userForm.patchValue({
      companieId: companieId
    });
    
    this.userService.getAllRoles().subscribe({
      next: (response: HttpResponse<Role[]>) => {
        if (response.status == 200) {
          this.roles = filterRoles(response.body ?? []);
        }
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 500) {
          this.toastr.error("Houve um erro desconhecido, contate o suporte técnico!");
          this.router.navigate(['/admin/users-panel']);
        }
      }
    })
  }

  Submit(): void {
    if (this.userForm.invalid || !this.verifyPassword()) {
      this.getNameErrors();
      this.getEmailErrors();
      this.getPasswordErrors();
      this.getPasswordVerifyErrors();
    } else {
      this.isLoading = true;
      const user: CreateUser = this.userForm.value as CreateUser;
      user.role = Number(user.role);

      this.userService.create(user).subscribe({
        next: (response: ResponseCreateUser) => {
          this.toastr.success(`O usuário ${response.name} foi criado com sucesso!`);
          this.router.navigate(['/admin/users-panel']);
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
            this.router.navigate(['/admin/users'])
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
