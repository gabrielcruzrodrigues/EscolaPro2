import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminMainSearchUserBoxComponent } from "../../layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { InputErrorMessageComponent } from "../../layout/input-error-message/input-error-message.component";
import { ButtonsFormComponent } from "../buttons-form/buttons-form.component";
import { nameValidators } from '../../../validators/nameValidator';
import { ToastrService } from 'ngx-toastr';
import { phoneValidator } from '../../../validators/phoneValidator';
import { rgValidator } from '../../../validators/rgValidator';
import { cpfValidator } from '../../../validators/cpfValidator';

@Component({
  selector: 'app-family-form',
  imports: [
    CommonModule,
    AdminMainSearchUserBoxComponent,
    InputErrorMessageComponent,
    ButtonsFormComponent,
    ReactiveFormsModule
  ],
  templateUrl: './family-form.component.html',
  styleUrl: './family-form.component.sass'
})
export class FamilyFormComponent implements AfterViewInit {
  form: FormGroup;
  previewUrl: string | ArrayBuffer | null = null;
  titleSearchStudents: string = 'Buscar estudante temp';
  placeholderSearchStudents: string = 'Digite aqui o nome ou email do estudante temp';
  cities: string[] = ['Jequié', 'Jaguaquara'];
  step: number = 1;
  stepName: string = 'Etapa familiar : 1';
  firstStep: boolean = true;
  lastEtep: boolean = false;

  rgFileUploaded: boolean = false;
  financialUploaded: boolean = false;
  cpfUploaded: boolean = false;

  //view childs
  @ViewChild('step1') step1!: ElementRef;
  @ViewChild('step2') step2!: ElementRef;

  //arrays with errors 
  nameErrors: string[] = [];
  emailErros: string[] = [];
  rgErrors: string[] = [];
  rgDispatchedErrors: string[] = [];
  rgDispatchedDateErrors: string[] = [];
  stateErrors: string[] = [];
  cityErrors: string[] = [];
  dateOfBirthErrors: string[] = [];
  rgFileErrors: string[] = [];
  cpfErrors: string[] = [];
  phoneErrors: string[] = [];
  cepErrors: string[] = [];
  addressErrors: string[] = [];
  homeNumberErrors: string[] = [];
  neighborhoodErrors: string[] = [];
  naturalnessErrors: string[] = [];
  nationalityErrors: string[] = [];
  sexErrors: string[] = [];
  typeErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
    this.form = this.fb.group({
      image: [null],
      rgFile: [null],
      financialFile: [null],
      cpfFile: [null],
      name: ['', [Validators.required, nameValidators()]],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      phone: [null, [Validators.required, phoneValidator()]],
      sex: ['', Validators.required],
      rg: [null, [Validators.required, rgValidator()]],
      rgDispatched: ['', Validators.required],
      rgDispatchedDate: ['', Validators.required],
      naturalness: ['', Validators.required],
      nationality: ['', Validators.required],
      cpf: ['', [Validators.required, cpfValidator()]],
      cep: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      address: ['', Validators.required],
      homeNumber: ['', Validators.required],
      neighborhood: ['', Validators.required],
      type: ['', Validators.required]
    });
  }

  sendDataFromFatherComponent(): void {
    this.validateForm();
    if (this.hasAnyErrorInInputs()) {
      this.toastr.info("Existem campos com erro! Vefifique-os antes de continuar.")
    }
    console.log(this.form.value);
  }

  ngAfterViewInit(): void {
    this.updateTemplate();
  }

  validateForm(): void {
    this.getNameErrors();
    this.getRgErrors();
    this.getSexErrors();
    this.getDateOfBirthErrors();
    this.getRgDispatchedErrors();
    this.getRgDispatchedDateErrors();
    this.getRgFileErrors();
    this.getCpfErrors();
    this.getPhoneErrors();
    this.getCepErrors();
    this.getAddressErrors();
    this.getHomeNumberErrors();
    this.getNeighborhoodErrors();
    this.getNaturalnessErrors();
    this.getNationalityErrors();
    this.getEmailErrors();
  }

  hasAnyErrorInInputs(): boolean {
    const allErrors = [
      this.nameErrors,
      this.emailErros,
      this.rgErrors,
      this.rgDispatchedErrors,
      this.rgDispatchedDateErrors,
      this.stateErrors,
      this.cityErrors,
      this.dateOfBirthErrors,
      this.rgFileErrors,
      this.cpfErrors,
      this.phoneErrors,
      this.cepErrors,
      this.addressErrors,
      this.homeNumberErrors,
      this.neighborhoodErrors,
      this.naturalnessErrors,
      this.nationalityErrors,
      this.sexErrors,
      this.typeErrors
    ];

    return allErrors.some(errorArray => errorArray.length > 0);
  }


  onImageSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.form.patchValue({ image: file });
      this.form.get('image')?.updateValueAndValidity();

      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onFileSelected(event: Event, option: string): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (file) {
      switch (option) {
        case 'rg':
          this.form.patchValue({ rgFile: file });
          this.rgFileUploaded = true;
          break;
        case 'financial':
          this.form.patchValue({ financialFile: file });
          this.financialUploaded = true;
          break;
        case 'cpf':
          this.form.patchValue({ cpfFile: file });
          this.cpfUploaded = true;
          break;
      }
    }

    input.value = '';
  }

  clearInputErrors(option: string): void {
    if (option == 'name') {
      this.nameErrors = [];
    }

    if (option == 'email') {
      this.emailErros = [];
    }

    if (option == 'dateOfBirth') {
      this.dateOfBirthErrors = [];
    }

    if (option == 'phone') {
      this.phoneErrors = [];
    }

    if (option == 'sex') {
      this.sexErrors = [];
    }

    if (option == 'rg') {
      this.rgErrors = [];
    }

    if (option == 'rgDispatched') {
      this.rgDispatchedErrors = [];
    }

    if (option == 'rgDispatchedDate') {
      this.rgDispatchedDateErrors = [];
    }

    if (option == 'naturalness') {
      this.naturalnessErrors = [];
    }

    if (option == 'nationality') {
      this.nationalityErrors = [];
    }

    if (option == 'cpf') {
      this.cpfErrors = [];
    }
  }

  updateTemplate(): void {
    switch (this.step) {
      case 1:
        // alert('oi')
        this.step1.nativeElement.classList.remove('disable');
        this.step2.nativeElement.classList.add('disable');
        this.stepName = 'Etapa familiar : 1';
        break;

      case 2:
        this.step1.nativeElement.classList.add('disable');
        this.step2.nativeElement.classList.remove('disable');
        this.stepName = 'Etapa familiar : 2';
        break;
    }
  }

  onStepChange(event: string): void {
    if (event.match('next')) {
      this.step++;

      if (this.step == 1) {
        this.firstStep = true;
      } else {
        this.firstStep = false;
      }

      if (this.step == 2) {
        this.lastEtep = true;
      } else {
        this.lastEtep = false;
      }

      this.updateTemplate();
    } else if (event.match('back')) {
      if (this.step >= 1) {
        this.step--;

        if (this.step == 1) {
          this.firstStep = true;
        } else {
          this.firstStep = false;
        }

        if (this.step == 2) {
          this.lastEtep = true;
        } else {
          this.lastEtep = false;
        }

        this.updateTemplate();
      }
    } else if (event.match('finish')) {
      // alert('cadastrado')
      this.sendDataFromFatherComponent();
    }
  }

  getRgErrors(): void {
    const control = this.form.get('rg');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O rg é obrigatório!');
      this.rgErrors = errors;
      return;
    }

    if (control?.hasError('invalidRg')) {
      errors.push('O rg é inválido!');
      this.rgErrors = errors;
      return;
    }
  }

  getRgDispatchedErrors(): void {
    const control = this.form.get('rgDispatched');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O orgão emissor é obrigatório!');
      this.rgDispatchedErrors = errors;
      return;
    }
  }

  getRgDispatchedDateErrors(): void {
    const control = this.form.get('rgDispatchedDate');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('A data de emissão é obrigatória!');
      this.rgDispatchedDateErrors = errors;
      return;
    }
  }

  findCitiesByState(event: Event): void {

  }

  searchCity(event: Event): void {

  }

  getDateOfBirthErrors(): void {
    const control = this.form.get('dateOfBirth');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('A data de aniversário é obrigatória!');
      this.dateOfBirthErrors = errors;
      return;
    }
  }

  getSexErrors(): void {
    const control = this.form.get('sex');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O sexo é obrigatório!');
      this.sexErrors = errors;
      return;
    }
  }

  getRgFileErrors(): void {

  }

  getCpfErrors(): void {
    const control = this.form.get('cpf');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O CPF é obrigatório!');
      this.cpfErrors = errors;
      return;
    }

    if (control?.hasError('invalidCpf')) {
      errors.push('O CPF é inválido!');
      this.cpfErrors = errors;
      return;
    }
  }

  getPhoneErrors(): void {
    const control = this.form.get('phone');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O numero de telefone é obrigatório!');
      this.phoneErrors = errors;
      return;
    }

    if (control?.hasError('invalidPhone')) {
      errors.push('O numero de telefone é inválido!');
      this.phoneErrors = errors;
      return;
    }
  }

  getCepErrors(): void {

  }

  getAddressErrors(): void {

  }

  getHomeNumberErrors(): void {

  }

  getNeighborhoodErrors(): void {

  }

  getNaturalnessErrors(): void {
    const control = this.form.get('naturalness');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('A naturalidade é obrigatória!');
      this.naturalnessErrors = errors;
      return;
    }
  }

  getNationalityErrors(): void {
    const control = this.form.get('nationality');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('A nacionalidade é obrigatória!');
      this.nationalityErrors = errors;
      return;
    }
  }

  getEmailErrors(): void {
    const control = this.form.get('email');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O email é obrigatório!');
      this.emailErros = errors;
      return;
    }

    if (control?.hasError('email')) {
      errors.push('O email é inválido!');
      this.emailErros = errors;
      return;
    }
  }

  getNameErrors(): void {
    const nameControl = this.form.get('name');
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
    console.log(this.nameErrors);
  }
}
