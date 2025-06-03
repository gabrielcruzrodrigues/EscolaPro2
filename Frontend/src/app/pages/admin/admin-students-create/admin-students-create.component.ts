import { Component } from '@angular/core';
import { StudentService } from '../../../services/student.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { StudentsFormComponent } from "../../../components/forms/students-form/students-form.component";
import { CommonModule } from '@angular/common';
import { AdminMainSearchUserBoxComponent } from "../../../components/layout/admin/admin-main-search-user-box/admin-main-search-user-box.component";
import { FamiliesMainSearchBoxComponent } from "../../../components/layout/families-main-search-box/families-main-search-box.component";
import { UserSelectedDownMainSearchBoxComponent } from "../../../components/layout/user-selected-down-main-search-box/user-selected-down-main-search-box.component";
import { FamilyService } from '../../../services/family.service';
import { Family } from '../../../types/Family';
import { map } from 'rxjs';
import { familyTypeConvertInString } from '../../../utils/FamilyTypeConvertInString';
import { convertCpfToBrazilPattern } from '../../../utils/ConvertCpfToBrazilPattern';
import { Student } from '../../../types/Student';

@Component({
  selector: 'app-admin-students-create',
  imports: [
    SpinningComponent,
    AdminNavbarComponent,
    InfoTopComponent,
    StudentsFormComponent,
    CommonModule,
    FamiliesMainSearchBoxComponent,
    UserSelectedDownMainSearchBoxComponent
  ],
  templateUrl: './admin-students-create.component.html',
  styleUrl: './admin-students-create.component.sass'
})
export class AdminStudentsCreateComponent {
  isLoading: boolean = false;
  title: string = 'Dados do Estudante';
  student: Student | null = null;

  //forms
  studentData: FormData | null = null;
  studentFormShow: boolean = true; //true

  familyCreateOrSelectOptionPageShow: boolean = false; //false

  //duplicate fields - 409
  emailDuplicate: boolean = false;
  phoneDuplicate: boolean = false;
  rgDuplicate: boolean = false;
  cpfDuplicate: boolean = false;

  //selectedFamilies
  fatherSelected: Family | null = null;
  motherSelected: Family | null = null;

  constructor(
    private studentService: StudentService,
    private familyService: FamilyService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  onStudentData(student: FormData): void {
    this.student = this.formDataToStudent(student);

    this.studentData = student;
    this.studentFormShow = false;
    this.familyCreateOrSelectOptionPageShow = true;
    this.title = "Selecionar ou cadastrar um familiar";

    //   this.emailDuplicate = false;
    //   this.phoneDuplicate = false;
    //   this.rgDuplicate = false;
    //   this.cpfDuplicate = false;

    //   this.isLoading = true;
    //   this.studentService.create(family).subscribe({
    //     next: (response: HttpResponse<Student>) => {
    //       this.isLoading = false;
    //       if (response.status === 201) {
    //         this.toastr.success("Estudante criado com sucesso!");
    //         this.router.navigate(['/admin/students-panel']);
    //         return;
    //       }

    //       this.toastr.info("Uma responsta inesperada foi enviada pelo servidor, contate o suporte técnico!");
    //       return;
    //     },
    //     error: (error: HttpErrorResponse) => {
    //       this.isLoading = false;
    //       console.log(error);

    //       if (error.status === 500) {
    //         this.toastr.error("Um erro desconhecido aconteceu, contate o suporte técnico!");
    //         this.router.navigate(['/admin/students-panel']);
    //         return;
    //       }

    //       if (error.status === 409) {
    //         this.toastr.info("Existem campos duplicados! Resolva os problemas antes de continuar!");
    //         (error.error.fields as string[]).forEach((field: string) => {
    //           switch (field) {
    //             case 'email':
    //               this.emailDuplicate = true;
    //               break;
    //             case 'phone':
    //               this.phoneDuplicate = true;
    //               break;
    //             case 'rg':
    //               this.rgDuplicate = true;
    //               break;
    //             case 'cpf':
    //               this.cpfDuplicate = true;
    //               break;
    //           }
    //         })
    //       }
    //     }
    //   })
  }

  searchESelectFamily(familyId: number): void {
    if (familyId) {
      this.familyService.getById(String(familyId)).pipe(map(response => response.body))
        .subscribe({
          next: (family: Family | null) => {
            let familySearched = family;
            if (familySearched) {
              familySearched.type = familyTypeConvertInString(Number(family?.type));
              familySearched.cpf = convertCpfToBrazilPattern(String(family?.cpf));
            }

            this.isLoading = false;

            if (familySearched?.type == 'PAI') {
              if (this.fatherSelected == null) {
                this.fatherSelected = familySearched;
              } else {
                this.toastr.info("Um familiar do tipo PAI já foi selecionado!");
              }
            }

            if (familySearched?.type == 'MÃE') {
              if (this.motherSelected == null) {
                this.motherSelected = familySearched;
              } else {
                this.toastr.info("Um familiar do tipo MÃE já foi selecionado!");
              }
            }
          },
          error: (error) => {
            if (error.status === 500) {
              this.toastr.error("Houve um erro ao buscar os dados do familiar, contate um administrador do sistema!");
              this.isLoading = false;
            }
          }
        })
    }
  }

  formDataToStudent(formData: FormData): Student {
    return {
      id: Number(formData.get('id')),
      address: formData.get('address') as string,
      cep: formData.get('cep') as string,
      city: formData.get('city') as string,
      cpf: formData.get('cpf') as string,
      cpfFile: null,
      cpfFilePath: formData.get('cpfFilePath') as string,
      dateOfBirth: formData.get('dateOfBirth') as string,
      email: formData.get('email') as string,
      financialFile: null,
      proofOfResidenceFilePath: formData.get('proofOfResidenceFilePath') as string,
      homeNumber: formData.get('homeNumber') as string,
      image: null,
      name: formData.get('name') as string,
      nationality: formData.get('nationality') as string,
      naturalness: formData.get('naturalness') as string,
      neighborhood: formData.get('neighborhood') as string,
      phone: formData.get('phone') as string,
      rg: formData.get('rg') as string,
      rgDispatched: formData.get('rgDispatched') as string,
      rgDispatchedDate: formData.get('rgDispatchedDate') as string,
      rgFile: null,
      rgFilePath: formData.get('rgFilePath') as string,
      sex: formData.get('sex') as string,
      state: formData.get('state') as string,
      createdAt: formData.get('createdAt') as string,
      financialResponsibleId: Number(formData.get('financialResponsibleId')),
      fatherId: Number(formData.get('fatherId')),
      motherId: Number(formData.get('motherId')),
      FixedHealth: null,
      studentFinancialResponsible: formData.get('studentFinancialResponsible') === 'true'
    };
  }

  deleteFamilySelected(type: string): void {
    if (type == "PAI") {
      this.fatherSelected = null;
    }
    if (type == "MÃE") {
      this.motherSelected = null;
    }
  }

  returnStudentForm(): void {
    this.familyCreateOrSelectOptionPageShow = false;
    this.studentFormShow = true;
  }
}
