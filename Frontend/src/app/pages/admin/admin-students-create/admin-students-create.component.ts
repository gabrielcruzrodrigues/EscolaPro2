import { Component } from '@angular/core';
import { StudentService } from '../../../services/student.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SpinningComponent } from "../../../components/layout/spinning/spinning.component";
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { StudentsFormComponent } from "../../../components/forms/students-form/students-form.component";
import { CommonModule } from '@angular/common';
import { FamiliesMainSearchBoxComponent } from "../../../components/layout/families-main-search-box/families-main-search-box.component";
import { UserSelectedDownMainSearchBoxComponent } from "../../../components/layout/user-selected-down-main-search-box/user-selected-down-main-search-box.component";
import { FamilyService } from '../../../services/family.service';
import { Family } from '../../../types/Family';
import { map } from 'rxjs';
import { familyTypeConvertInString } from '../../../utils/FamilyTypeConvertInString';
import { convertCpfToBrazilPattern } from '../../../utils/ConvertCpfToBrazilPattern';

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
  title: string = 'Cadastrar dados do estudante';
  student: FormData | null = null;

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
    this.student = student;

    this.studentData = student;
    this.studentFormShow = false;
    this.familyCreateOrSelectOptionPageShow = true;
    this.title = "Selecionar ou cadastrar um familiar";
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
    this.title = 'Cadastrar dados do estudante';
  }
}
