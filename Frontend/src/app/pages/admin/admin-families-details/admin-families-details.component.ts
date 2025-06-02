import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FamilyService } from '../../../services/family.service';
import { AuthService } from '../../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { formatDate } from '../../../utils/FormatDate';
import { Family } from '../../../types/Family';
import { AdminNavbarComponent } from "../../../components/layout/admin/admin-navbar/admin-navbar.component";
import { InfoTopComponent } from "../../../components/layout/info-top/info-top.component";
import { familyTypeConvertInString } from '../../../utils/FamilyTypeConvertInString';
import { convertCpfToBrazilPattern } from '../../../utils/ConvertCpfToBrazilPattern';
import { convertFamilyEnumSexToString } from '../../../utils/FamilyEnumToStringConvert';
import { convertRgToBrazilPattern } from '../../../utils/ConvertRgToBrazilPattern';
import { ViewDocumentButtonComponent } from "../../../components/layout/view-document-button/view-document-button.component";

@Component({
  selector: 'app-admin-families-details',
  imports: [AdminNavbarComponent, InfoTopComponent, ViewDocumentButtonComponent],
  templateUrl: './admin-families-details.component.html',
  styleUrl: './admin-families-details.component.sass'
})
export class AdminFamiliesDetailsComponent implements OnInit {
  family: Family | null = null;
  type: string = '';
  createdAt: string = '';
  dateOfBirth: string = '';
  cpf: string = '';
  sex: string = '';
  rg: string = '';

  rgFilePath: string = '';
  cpfFilePath: string = '';
  proofOfResidenceFilePath: string = '';

  isLoading: boolean = false;

  constructor(
    private familyService: FamilyService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    const id = this.activatedRoute.snapshot.paramMap.get('familyId') ?? '0';
    this.familyService.getById(id).subscribe({
      next: (response: HttpResponse<Family>) => {
        if (response.status == 200) {
          this.family = response.body;
          this.type = familyTypeConvertInString(Number(response.body?.type));
          this.createdAt = formatDate(String(response.body?.createdAt));
          this.cpf = convertCpfToBrazilPattern(String(response.body?.cpf));
          this.sex = convertFamilyEnumSexToString(Number(response.body?.sex));
          this.rg = convertRgToBrazilPattern(String(response.body?.rg));
          this.dateOfBirth = formatDate(String(response.body?.dateOfBirth));

          if (response.body?.rgFilePath) {
            this.rgFilePath = response.body?.rgFilePath;
            console.log(this.rgFilePath)
          }
           
          if (response.body?.cpfFilePath) {
            this.cpfFilePath = response.body?.cpfFilePath;
            console.log(this.cpfFilePath)
          }

          if (response.body?.proofOfResidenceFilePath) {
            this.proofOfResidenceFilePath = response.body?.proofOfResidenceFilePath;
            console.log(this.proofOfResidenceFilePath)
          }
        }
        this.isLoading = false;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 404) {
          this.toastr.info("O Familiar não foi encontrado, contate o suporte técnico!");
          this.isLoading = false;
          this.router.navigate(['/admin/families-panel']);
        } 
        if (error.status == 500) {
          this.toastr.error("Ocorreu um erro desconhecido, contate o suporte técnico!");
          this.isLoading = false;
          this.router.navigate(['/admin/families-panel']);
        }
      }
    })
  }
}
