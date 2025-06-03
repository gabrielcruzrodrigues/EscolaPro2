import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../types/Student';
import { ActivatedRoute } from '@angular/router';
import { StudentService } from '../../../services/student.service';
import { map } from 'rxjs';
import { Family } from '../../../types/Family';
import { FamilyService } from '../../../services/family.service';
import { ToastrService } from 'ngx-toastr';
import { familyTypeConvertInString } from '../../../utils/FamilyTypeConvertInString';
import { convertCpfToBrazilPattern } from '../../../utils/ConvertCpfToBrazilPattern';

@Component({
  selector: 'app-user-selected-down-main-search-box',
  imports: [],
  templateUrl: './user-selected-down-main-search-box.component.html',
  styleUrl: './user-selected-down-main-search-box.component.sass'
})
export class UserSelectedDownMainSearchBoxComponent {
  @Input() family: Family | null = null;
  @Output() deleteFamily = new EventEmitter<string>();
  isLoading: boolean = false;

  type: string = '';
  cpf: string = '';

  deleteFamilySelected(type: string | undefined): void {
    if (type) {
      this.deleteFamily.emit(type);
    }
  }
}
