import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AdminMainSearchUserBoxComponent } from "../../layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { InputErrorMessageComponent } from "../../layout/input-error-message/input-error-message.component";
import { ButtonsFormComponent } from "../buttons-form/buttons-form.component";

@Component({
  selector: 'app-family-form',
  imports: [
    CommonModule,
    AdminMainSearchUserBoxComponent,
    InputErrorMessageComponent,
    ButtonsFormComponent
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
  emailErrros: string[] = [];
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
  NaturalnessErrors: string[] = [];
  NationalityErrors: string[] = [];

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      image: [null],
      rgFile: [null],
      financialFile: [null],
      cpfFile: [null]
    });
  }

  ngAfterViewInit(): void {
    this.updateTemplate();
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
      this.emailErrros = [];
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
    console.log('last: ' + this.step)
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
      alert('cadastrado')
    }
    console.log('now: ' + this.step)
  }

  getRgErrors(): void {

  }

  getRgDispatchedErrors(): void {

  }

  getRgDispatchedDateErrors(): void {

  }

  findCitiesByState(event: Event): void {

  }

  searchCity(event: Event): void {

  }

  getDateOfBirthErrors(): void {

  }

  getRgFileErrors(): void {

  }

  getCpfErrors(): void {

  }

  getPhoneErrors(): void {

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

  }

  getNationalityErrors(): void {

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
