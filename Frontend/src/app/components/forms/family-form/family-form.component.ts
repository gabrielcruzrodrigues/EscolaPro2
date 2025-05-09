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
  stepName: string = 'etapa familiar : 1';

  //view childs
  @ViewChild('step1') step1!: ElementRef;
  @ViewChild('step2') step2!: ElementRef;
  @ViewChild('step3') step3!: ElementRef;

  //arrays with errors 
  nameErrors: string[] = [];
  emailErrros: string[] = [];
  rgErrors: string[] = [];
  rgDispatchedErrors: string[] = [];
  rgDispatchedDateErrors: string[] = [];
  stateErrors: string[] = [];
  cityErrors: string[] = [];
  dateOfBirthErrors: string[] = [];

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      image: [null]
    });
  }

  ngAfterViewInit(): void {
    this.updateTemplate();
  }

  onFileSelected(event: Event) {
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

  clearInputErrors(option: string): void {
    if (option == 'name') {
      this.nameErrors = [];
    }

    if (option == 'email') {
      this.emailErrros = [];
    }
  }

  changeStep(option: string): void {
    if (option === 'next' && this.step <= 4) {
      this.step++;
    }

    if (option === 'back' && this.step >= 2) {
      this.step--;
    }

    this.updateTemplate();
  }

  updateTemplate(): void {
    switch (this.step) {
      case 1:
        // alert('oi')
        this.step1.nativeElement.classList.remove('disable');
        this.step2.nativeElement.classList.add('disable');
        this.stepName = 'etapa familiar : 1';
          break;
        
        case 2:
          this.step1.nativeElement.classList.add('disable');
          this.step2.nativeElement.classList.remove('disable');
          this.step3.nativeElement.classList.add('disable');
          this.stepName = 'etapa familiar : 2';
          break;
        case 3:
          this.step2.nativeElement.classList.add('disable');
          this.step3.nativeElement.classList.remove('disable');
          this.stepName = 'etapa familiar : 3';
        break;
    }
  }

  onStepChange(event: string): void {
    if (event.match('next')) {
      this.step++;
      this.updateTemplate();
    } else {
      if (this.step >= 1) {
        this.step--;
        this.updateTemplate();
      }
    }
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
