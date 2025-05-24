import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminMainSearchUserBoxComponent } from "../../layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { InputErrorMessageComponent } from "../../layout/input-error-message/input-error-message.component";
import { ButtonsFormComponent } from "../buttons-form/buttons-form.component";
import { nameValidators } from '../../../validators/nameValidator';
import { ToastrService } from 'ngx-toastr';
import { phoneValidator } from '../../../validators/phoneValidator';
import { rgValidator } from '../../../validators/rgValidator';
import { cpfValidator } from '../../../validators/cpfValidator';
import { CepService } from '../../../services/cep.service';
import { Cep } from '../../../types/Cep';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { SpinningComponent } from "../../layout/spinning/spinning.component";
import { Family } from '../../../types/Family';
import { formatDate } from '../../../utils/FormatDate';

@Component({
  selector: 'app-family-form',
  imports: [
    CommonModule,
    AdminMainSearchUserBoxComponent,
    InputErrorMessageComponent,
    ButtonsFormComponent,
    ReactiveFormsModule,
    SpinningComponent
  ],
  templateUrl: './family-form.component.html',
  styleUrl: './family-form.component.sass'
})
export class FamilyFormComponent implements AfterViewInit, OnChanges {
  form: FormGroup;
  previewUrl: string | ArrayBuffer | null = null;
  titleSearchStudents: string = 'Buscar estudante temp';
  placeholderSearchStudents: string = 'Digite aqui o nome ou email do estudante temp';
  cities: string[] = ['Jequié', 'Jaguaquara'];
  step: number = 1;
  stepName: string = 'Etapa familiar : 1';
  firstStep: boolean = true;
  lastEtep: boolean = false;
  cep: Cep | null = null;
  isLoading: boolean = false;
  @Output() familyData = new EventEmitter<FormData>();

  rgFileUploaded: boolean = false;
  financialUploaded: boolean = false;
  cpfUploaded: boolean = false;

  //view childs
  @ViewChild('step1') step1!: ElementRef;
  @ViewChild('step2') step2!: ElementRef;

  //config for form to edit
  @Input() forEdit: boolean = false;
  @Input() familyForEditData: Family | null = null;

  //409 duplicates fields
  @Input() nameDuplicate: boolean = false;
  @Input() emailDuplicate: boolean = false;
  @Input() phoneDuplicate: boolean = false;
  @Input() rgDuplicate: boolean = false;
  @Input() cpfDuplicate: boolean = false;
  showDuplicatedErrorMessage: boolean = false;

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
    private toastr: ToastrService,
    private cepService: CepService
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
      type: ['', Validators.required],
      role: [100]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['nameDuplicate']) {
      this.duplicateFields('name');
    }

    if (changes['emailDuplicate']) {
      this.duplicateFields('email');
      // alert('email')
    }

    if (changes['phoneDuplicate']) {
      this.duplicateFields('phone');
      // alert('phone')
    }

    if (changes['rgDuplicate']) {
      this.duplicateFields('rg');
      // alert('rg')
    }

    if (changes['cpfDuplicate']) {
      this.duplicateFields('cpf');
      // alert('cpf')
    }

    if (this.forEdit && changes['familyForEditData'] && changes['familyForEditData'].currentValue) {
      const data = changes['familyForEditData'].currentValue as Family;
      console.log(data);
      this.form.patchValue({
        image: data.image,
        rgFile: data.rgFile,
        financialFile: data.financialFile,
        cpfFile: data.cpfFile,
        name: data.name,
        email: data.email,
        dateOfBirth: data.dateOfBirth,
        phone: data.phone,
        sex: data.sex,
        rg: data.rg,
        rgDispatched: data.rgDispatched,
        rgDispatchedDate: data.rgDispatchedDate,
        naturalness: data.naturalness,
        nationality: data.nationality,
        cpf: data.cpf,
        cep: data.cep,
        state: data.state,
        city: data.city,
        address: data.address,
        homeNumber: data.homeNumber,
        neighborhood: data.neighborhood,
        type: data.type,
      });
    }
  }

  duplicateFields(option: string): void {
    switch (option) {
      case 'name':
        if (this.nameDuplicate) {
          this.nameErrors.push('Esse nome já foi cadastrado!');
          this.nameDuplicate = false;
        }
        break;
      case 'email':
        if (this.emailDuplicate) {
          this.emailErros.push('Esse email já foi cadastrado!');
          this.emailDuplicate = false;
        }
        break;
      case 'phone':
        if (this.phoneDuplicate) {
          this.phoneErrors.push('Esse telefone já foi cadastrado!');
          this.phoneDuplicate = false;
        }
        break;
      case 'rg':
        if (this.rgDuplicate) {
          this.rgErrors.push('Esse RG já foi cadastrado!');
          this.rgDuplicate = false;
        }
        break;
      case 'cpf':
        if (this.cpfDuplicate) {
          this.cpfErrors.push('Esse CPF já foi cadastrado!');
          this.cpfDuplicate = false;
        }
        break;
    }
  }

  sendDataFromFatherComponent(): void {
    this.validateForm();
    if (this.hasAnyErrorInInputs()) {
      this.toastr.info("Existem campos com erro! Vefifique-os antes de continuar.");
      return;
    }

    const formData = new FormData();

    formData.append('Name', this.form.get('name')?.value);
    formData.append('Email', this.form.get('email')?.value);
    formData.append('Cpf', this.form.get('cpf')?.value);
    formData.append('Rg', this.form.get('rg')?.value);
    formData.append('RgDispatched', this.form.get('rgDispatched')?.value);
    formData.append('RgDispatchedDate', new Date(this.form.get('rgDispatchedDate')?.value).toISOString());
    formData.append('DateOfBirth', new Date(this.form.get('dateOfBirth')?.value).toISOString());
    formData.append('Nationality', this.form.get('nationality')?.value);
    formData.append('Naturalness', this.form.get('naturalness')?.value);
    formData.append('Sex', this.form.get('sex')?.value.toString()); // enum
    formData.append('HomeNumber', this.form.get('homeNumber')?.value);
    formData.append('Phone', this.form.get('phone')?.value);
    formData.append('City', this.form.get('city')?.value);
    formData.append('State', this.form.get('state')?.value);
    formData.append('Type', this.form.get('type')?.value.toString()); // enum
    formData.append('Role', this.form.get('role')?.value.toString()); // enum
    formData.append('Cep', this.form.get('cep')?.value.toString());
    formData.append('Address', this.form.get('address')?.value.toString());
    formData.append('Neighborhood', this.form.get('neighborhood')?.value.toString());

    if (this.form.get('image')?.value) formData.append('Image', this.form.get('image')?.value);
    if (this.form.get('rgFile')?.value) formData.append('RgFile', this.form.get('rgFile')?.value);
    if (this.form.get('cpfFile')?.value) formData.append('CpfFile', this.form.get('cpfFile')?.value);
    if (this.form.get('financialFile')?.value) formData.append('ProofOfResidenceFile', this.form.get('financialFile')?.value);

    this.familyData.emit(formData);
  }

  getCep(): void {
    const cep = this.form.get('cep')?.value;
    if (cep && cep.length == 8) {
      this.isLoading = true;
      this.cepService.getCep(cep).subscribe({
        next: (response: HttpResponse<Cep>) => {
          this.form.patchValue({
            address: response.body?.street,
            city: response.body?.city,
            neighborhood: response.body?.neighborhood,
            state: response.body?.state
          })
          this.getStateErrors();
          this.getCityErrors();
          this.getAddressErrors();
          this.getNeighborhoodErrors();
          this.isLoading = false;
        },
        error: (response: HttpErrorResponse) => {
          this.isLoading = false;
          this.toastr.info("Este cep não foi encontrado ou é inválido!")

        }
      })
    } if (cep.length > 8) {
      this.toastr.info("O cep deve conter exatamente 8 números!");
    }
  }

  ngAfterViewInit(): void {
    this.updateTemplate();
  }

  validateForm(): void {
    this.getNameErrors();
    this.getRgErrors();
    this.getSexErrors();
    this.getCityErrors();
    this.getStateErrors();
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
    this.getTypeErrors();
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

    if (option == 'cep') {
      this.cepErrors = [];
    }

    if (option == 'state') {
      this.stateErrors = [];
    }

    if (option == 'city') {
      this.cityErrors = [];
    }

    if (option == 'address') {
      this.addressErrors = [];
    }

    if (option == 'type') {
      this.typeErrors = [];
    }

    if (option == 'neighborhood') {
      this.neighborhoodErrors = [];
    }

    if (option == 'homeNumber') {
      this.homeNumberErrors = [];
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

      (this.step == 1) ? this.firstStep = true : this.firstStep = false;
      (this.step == 2) ? this.lastEtep = true : this.lastEtep = false;

      this.updateTemplate();
    } else if (event.match('back')) {
      if (this.step >= 1) {
        this.step--;

        (this.step == 1) ? this.firstStep = true : this.firstStep = false;
        (this.step == 2) ? this.lastEtep = true : this.lastEtep = false;

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

  getTypeErrors(): void {
    const control = this.form.get('type');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('Obrigatório!');
      this.typeErrors = errors;
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

  getStateErrors(): void {
    const control = this.form.get('state');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O estado é obrigatório!');
      this.stateErrors = errors;
      return;
    }
  }

  getCityErrors(): void {
    const control = this.form.get('city');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('A cidade é obrigatória!');
      this.cityErrors = errors;
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
    const control = this.form.get('cep');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O cep é obrigatório!');
      this.cepErrors = errors;
      return;
    }
  }

  getAddressErrors(): void {
    const control = this.form.get('address');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O endereço é obrigatório!');
      this.addressErrors = errors;
      return;
    }
  }

  getHomeNumberErrors(): void {
    const control = this.form.get('homeNumber');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('obrigatório!');
      this.homeNumberErrors = errors;
      return;
    }
  }

  getNeighborhoodErrors(): void {
    const control = this.form.get('neighborhood');
    const errors: string[] = [];

    if (control?.hasError('required')) {
      errors.push('O bairro é obrigatório!');
      this.neighborhoodErrors = errors;
      return;
    }
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
  }
}
