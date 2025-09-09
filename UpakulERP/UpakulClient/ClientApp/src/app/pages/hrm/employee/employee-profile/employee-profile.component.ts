/* employee-profile.component.ts */
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule
} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { EmployeeService } from '../../../../services/hrm/employeeprofile/employee.service';
import { EmployeeDropdownList } from '../../../../models/hr/employeeprofile/employeeAllDropdown';
import { ConfigService } from '../../../../core/config.service';
import { ToastrService } from 'ngx-toastr';
import { ImageurlMappingConstant } from '../../../../shared/image-url-mapping-constant';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';
interface DropdownValues {
  text: string;
  value: string;
  //selected:string;
}
@Component({
  selector: 'app-employee-profile',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule, BsDatepickerModule],
  templateUrl: './employee-profile.component.html',
  styleUrl: './employee-profile.component.css'
})


export class EmployeeProfileComponent implements OnInit {
  qry: string | null = null;
  ngAfterViewInit() {
    flatpickr('#datepickerValCalender', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }
  /* ──────────────────────── constants for selects ──────────────────────── */
  employeeAllDropdown: EmployeeDropdownList = {
    department: [],
    designation: [],
    country: [],
    office: [],
    circular: [],
    bank: [],
    division: [],
    employeeType: [],
    employeeStatus: [],
    gender: [],
    religion: [],
    bloodGroup: [],
    maritalStatus: [],
    occupation: []
  };
  bankBranchDropdownValues: DropdownValues[] = [];
  projectDropdownValues: DropdownValues[] = [];
  present_district_DropdownValues: DropdownValues[] = [];
  present_thana_DropdownValues: DropdownValues[] = [];
  present_union_DropdownValues: DropdownValues[] = [];
  present_village_DropdownValues: DropdownValues[] = [];

  permanent_district_DropdownValues: DropdownValues[] = [];
  permanent_thana_DropdownValues: DropdownValues[] = [];
  permanent_union_DropdownValues: DropdownValues[] = [];
  permanent_village_DropdownValues: DropdownValues[] = [];

  employeePreviewPicURL: string = 'assets/img/no-user.gif';
  nidPreviewUrl: string = 'assets/img/nid.jpg';
  previewSignatureUrl: string = 'assets/img/signature.png';
  spousePreviewPicURL: string = 'assets/img/no-user.gif';
  maritalStatusvalue: string = "";
  isProject: boolean = false;
  isSubmitting = false;

  onEmpImageSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.employeePreviewPicURL = e.target.result;

        this.empProfileForm.patchValue({ EmployeePic: file });
      }
    } else {
      this.employeePreviewPicURL = 'assets/img/no-user.gif';
    }
  }

  onEmpSignatureSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.previewSignatureUrl = e.target.result;
        this.empProfileForm.patchValue({ EmpSignature: file/*reader.result*/ });
      }
    } else {
      this.previewSignatureUrl = 'assets/img/signature.png';
    }
  }

  onNidFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.nidPreviewUrl = e.target.result;
        this.empProfileForm.patchValue({ NIDPic: file });
      }
    } else {
      this.previewSignatureUrl = 'assets/img/nid.jpg';
    }
  }
  onSpouseFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.spousePreviewPicURL = e.target.result;
        this.empProfileForm.patchValue({ SpousePic: file });
      }
    } else
      this.spousePreviewPicURL = 'assets/img/no-user.gif';

  }

  /* ───────────────────────── multistep control ────────────────────────── */
  steps = [
    { label: 'Employee Profile' },
     { label: 'Official Information' },
    // { label: 'Personal Information' },
    { label: 'Educational Information' },
    { label: 'Nominee Information' },
    { label: 'Guarantor Information' },
    { label: 'Reference Information' }
  ];
  currentStep = 0;

  /* ─────────────────────────── form group ─────────────────────────────── */
  empProfileForm!: FormGroup;
  private domain_url_hrm: string;
  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private http: HttpClient, private fb: FormBuilder, private apiService: EmployeeService,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    // Mahfuz
    private configService: ConfigService
  ) {
    this.domain_url_hrm = this.configService.hrmApiBaseUrl();
  }

  ngOnInit(): void {

    this.empProfileForm = this.fb.group({
      // Step 1: Employee Basic Information
      EmployeeId: [''],
      EmployeeCode: ['', Validators.required],
      FirstName: ['', Validators.required],
      LastName: [''],
      EmployeeFullName: [''],
      EmployeeNameBn: [''],
      OfficeId: ['', Validators.required],
      ProjectId: [''],
      DepartmentId: ['', Validators.required],
      DesignationId: ['', Validators.required],
      JoiningDate: ['', Validators.required],
      ConfirmationDate: [''],
      PermanentDate: [''],
      EmployeeTypeId: ['', Validators.required],
      EmployeeStatusId: ['', Validators.required],
      PersonalContactNo: [''],
      OfficialMobileNo: [''],
      PersonalEmail: [''],
      OfficialEmail: [''],
      CircularId: [''],
      // General
      DOB: ['', Validators.required],
      Height: [''],
      Weight: [''],
      IdentificationMarks: [''],

      CountryId: ['', Validators.required],
      PlaceOfBirth: [''],
      Gender: ['', Validators.required],
      Religion: ['', Validators.required],
      BloodGroup: [''],
      BankId: [''],
      BankBranchId: [''],
      BankAccountNo: [''],
      PassportNo: [''],
      DrivingLicense: [''],
      DrivingLicenseExpDate: [''],
      TIN: [''],
      NID: [''],
      // Image 
      EmployeePicURL: [''],
      EmployeePic: [File, Validators.required],
      NIDPicUrl: [''],
      NIDPic: [File, Validators.required],

      EmpSignatureUrl: [''],
      EmpSignature: [File, Validators.required],
      //  Personal Information

      FatherName: ['', Validators.required],
      FatherNameBn: [''],
      FatherOccupation: [''],
      MotherName: [''],
      MotherNameBn: [''],
      MotherOccupation: [''],
      MaritalStatus: ['', Validators.required],
      SpouseName: [''],
      SpouseNameBn: [''],
      SpouseNID: [''],
      SpouseContactNo: [''],
      SpouseOccupation: [''],
      SpousePicURL: [''],
      SpousePic: [File, Validators.required],
      NoOfChild: [''],
      DivorcedDate: [''],
      WidowerDate: [''],

      //  Address
      PresentDistrictId: [''],
      PresentDivisionId: [''],
      PresentThanaId: [''],
      PresentUnionId: [''],
      PresentVillageId: [''],
      PresentAddress: [''],

      PermanentDivisionId: [''],
      PermanentDistrictId: [''],
      PermanentThanaId: [''],
      PermanentUnionId: [''],
      PermanentVillageId: [''],
      PermanentAddress: [''],

    });
    this.loadAllDropdown();
    const id = history.state.employeeId;
    if (id) {
      this.apiService.getEmployeeById(id).subscribe(res => {

        const emp = res ?? res;
        if (emp) {
          const b_url = this.domain_url_hrm.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL;
          if (emp.employeePicURL)
            this.employeePreviewPicURL = b_url + emp.employeePicURL;
          if (emp.empSignatureUrl)
            this.previewSignatureUrl = b_url + emp.empSignatureUrl;
          if (emp.nIDPicUrl)
            this.nidPreviewUrl = b_url + emp.nIDPicUrl;
          if (emp.spousePicURL)
            this.spousePreviewPicURL = b_url + emp.spousePicURL;

          this.empProfileForm.patchValue({
            EmployeeId: emp.employeeId,
            EmployeeCode: emp.employeeCode,
            FirstName: emp.firstName,
            LastName: emp.lastName,
            EmployeeFullName: emp.firstName + (!emp.lastName ? "" : (" " + emp.lastName)),
            EmployeeNameBn: emp.employeeNameBn,
            OfficeId: emp.officeId,
            ProjectId: emp.projectId,
            DepartmentId: emp.departmentId,
            DesignationId: emp.designationId,
            JoiningDate: emp.joiningDate,
            ConfirmationDate: emp.confirmationDate,
            PermanentDate: emp.permanentDate,
            EmployeeTypeId: emp.employeeTypeId,
            EmployeeStatusId: emp.employeeStatusId,
            PersonalContactNo: emp.personalContactNo,
            OfficialMobileNo: emp.officialMobileNo,
            PersonalEmail: emp.personalEmail,
            OfficialEmail: emp.officialEmail,
            CircularId: emp.circularId,
            DOB: emp.dob,
            Height: emp.height,
            Weight: emp.weight,
            IdentificationMarks: emp.identificationMarks,
            CountryId: emp.countryId,
            PlaceOfBirth: emp.placeOfBirth,
            Gender: emp.gender,
            Religion: emp.religion,
            BloodGroup: emp.bloodGroup,
            BankId: emp.bankId,
            BankBranchId: emp.bankBranchId,
            BankAccountNo: emp.bankAccountNo,
            PassportNo: emp.passportNo,
            DrivingLicense: emp.drivingLicense,
            DrivingLicenseExpDate: emp.drivingLicenseExpDate,
            TIN: emp.tin,
            NID: emp.nid,
            // Image 
            EmployeePicURL: [''],
            EmployeePic: [File, Validators.required],
            NIDPicUrl: [''],
            NIDPic: [File, Validators.required],

            EmpSignatureUrl: [''],
            EmpSignature: [File, Validators.required],
            SpousePicURL: emp.spousePicURL,
            SpousePic: [File, Validators.required],
            //  Personal Information

            FatherName: emp.fatherName,
            FatherNameBn: emp.fatherNameBn,
            FatherOccupation: emp.fatherOccupation,
            MotherName: emp.motherName,
            MotherNameBn: emp.motherNameBn,
            MotherOccupation: emp.motherOccupation,
            MaritalStatus: emp.maritalStatus,
            SpouseName: emp.spouseName,
            SpouseNameBn: emp.spouseNameBn,
            SpouseNID: emp.spouseNID,
            SpouseContactNo: emp.spouseContactNo,
            SpouseOccupation: emp.spouseOccupation,
            NoOfChild: emp.noOfChild,
            DivorcedDate: emp.divorcedDate,
            WidowerDate: emp.widowerDate,

            //  Address
            PresentDivisionId: emp.presentDivisionId,
            PresentDistrictId: emp.presentDistrictId,
            PresentThanaId: emp.presentThanaId,
            PresentUnionId: emp.presentUnionId,
            PresentVillageId: emp.presentVillageId,
            PresentAddress: emp.presentAddress,

            PermanentDivisionId: emp.permanentDivisionId,
            PermanentDistrictId: emp.permanentDistrictId,
            PermanentThanaId: emp.permanentThanaId,
            PermanentUnionId: emp.permanentUnionId,
            PermanentVillageId: emp.permanentVillageId,
            PermanentAddress: emp.permanentAddress,
          })
        }
      });
    }

  }
  loadAllDropdown() {
    this.apiService.getEmployeeAllDropdown().subscribe({
      next: (res) => {
        this.employeeAllDropdown = res.data;
      }
    })
  }
  onBankChange(event: any) {
    if (event.target.value) {
      this.apiService.getBankBranchDropdown(event.target.value).subscribe({
        next: (data) => {
          this.bankBranchDropdownValues = data;
        },
        error: (err) => {
          console.error('Error fetching dropdown data:', err);
        }
      });
    } else
      this.bankBranchDropdownValues = []
  }

  /* ──────────────────────── navigation helpers ────────────────────────── */

  /** check only the controls that belong to the given step */
  private isStepValid(step: number): boolean {
    switch (step) {
      case 0:
        return this.empProfileForm.validStepOne;
      case 1:
        // return (
        //   this.empProfileForm.get('Email')!.valid &&
        //   this.empProfileForm.get('TelNo')!.valid
        // );
        return this.empProfileForm.validStepTwo;
      case 2:
        return this.empProfileForm.validStepThree;
      default:
        return this.empProfileForm.valid;
    }
  }

  /** enable clicking on a tab only if all previous steps valid */
  canNavigateToStep(targetStep: number): boolean {
    if (targetStep <= this.currentStep) return true;
    for (let i = 0; i < targetStep; i++) {
      if (!this.isStepValid(i)) return false;
    }
    return true;
  }

  nextStep(): void {
    if (this.isStepValid(this.currentStep) && this.currentStep < this.steps.length - 1) {
      this.currentStep++;
    }
  }

  prevStep(): void {
    if (this.currentStep > 0) this.currentStep--;
  }

  goToStep(index: number): void {
    if (this.canNavigateToStep(index)) this.currentStep = index;
  }

  onMaritalStatusEvent(event: any) {
    this.maritalStatusvalue = event.target.value;
  }
  onOfficeChange(event: any) {
    if (event.target.value == 2) {
      this.apiService.getProjectDropdown().subscribe({
        next: (data) => {
          this.projectDropdownValues = data;
        },
        error: (err) => {
          console.error('Error fetching dropdown data:', err);
        }
      });

      this.isProject = true;
    }
    else {
      this.isProject = false;
      this.projectDropdownValues = [];
    }
  }
  onGeoLocationChange(event: any) {
    const input = event.target as HTMLInputElement;
    const controlName = input.getAttribute('formControlName');
    if (input.value) {
      this.apiService.getGeoLocationDropdown(Number(input.value)).subscribe({
        next: (data) => {
          (controlName == "PresentDivisionId" ? this.present_district_DropdownValues = data
            : controlName == "PresentDistrictId" ? this.present_thana_DropdownValues = data
              : controlName == "PresentThanaId" ? this.present_union_DropdownValues = data
                : controlName == "PresentUnionId" ? this.present_village_DropdownValues = data

                  : controlName == "PermanentDivisionId" ? this.permanent_district_DropdownValues = data
                    : controlName == "PermanentDistrictId" ? this.permanent_thana_DropdownValues = data
                      : controlName == "PermanentThanaId" ? this.permanent_union_DropdownValues = data
                        : controlName == "PermanentUnionId" ? this.permanent_village_DropdownValues = data

                          : null);

        },
        error: (err) => {
          console.error('Error fetching dropdown data:', err);
        }
      });
    } else {
      (controlName == "PresentDivisionId" ? this.present_district_DropdownValues = []
        : controlName == "PresentDistrictId" ? this.present_thana_DropdownValues = []
          : controlName == "PresentThanaId" ? this.present_union_DropdownValues = []
            : controlName == "PresentUnionId" ? this.present_village_DropdownValues = []

              : controlName == "PermanentDivisionId" ? this.permanent_district_DropdownValues = []
                : controlName == "PermanentDistrictId" ? this.permanent_thana_DropdownValues = []
                  : controlName == "PermanentThanaId" ? this.permanent_union_DropdownValues = []
                    : controlName == "PermanentUnionId" ? this.permanent_village_DropdownValues = []

                      : null);
    }

  }

  get combinedText(): string {
    if (!this.empProfileForm.get('LastName')?.value) return `${this.empProfileForm.get('FirstName')?.value}`.trim();
    else return `${this.empProfileForm.get('FirstName')?.value} ${this.empProfileForm.get('LastName')?.value}`.trim();
  }
  get ageCalculation(): number {
    const birth = new Date(this.empProfileForm.get('DOB')?.value);
    if (this.isValidDate(birth)) {
      const today = new Date();

      let age = today.getFullYear() - birth.getFullYear();
      const monthDiff = today.getMonth() - birth.getMonth();
      const dayDiff = today.getDate() - birth.getDate();
      // Adjust if birthday hasn't occurred yet this year
      if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0))
        age--;
      return age;
    }
    else return 0

  }

  isValidDate(date: any): boolean {
    return date instanceof Date && !isNaN(date.getTime());
  }
  /* ──────────────────────────── submit ────────────────────────────────── */
  /* ──────────────────────────── Employee Profile ────────────────────────────────── */
  employeeProfile(): FormData {
    const formData = new FormData();
    formData.append('EmployeeId', this.empProfileForm.get('EmployeeId')?.value);
    formData.append('OfficeId', this.empProfileForm.get('OfficeId')?.value);
    if (this.empProfileForm.get('ProjectId')?.value) formData.append('ProjectId', this.empProfileForm.get('ProjectId')?.value);
    formData.append('EmployeeCode', this.empProfileForm.get('EmployeeCode')?.value);
    formData.append('FirstName', this.empProfileForm.get('FirstName')?.value);
    formData.append('LastName', this.empProfileForm.get('LastName')?.value);
    if (this.empProfileForm.get('EmployeeNameBn')?.value) formData.append('EmployeeNameBn', this.empProfileForm.get('EmployeeNameBn')?.value);
    if (this.empProfileForm.get('EmployeePic')?.value) formData.append('EmployeePic', this.empProfileForm.get('EmployeePic')?.value);
    if (this.empProfileForm.get('EmpSignature')?.value) formData.append('EmpSignature', this.empProfileForm.get('EmpSignature')?.value);

    formData.append('FatherName', this.empProfileForm.get('FatherName')?.value);
    formData.append('FatherNameBn', this.empProfileForm.get('FatherNameBn')?.value);
    if (this.empProfileForm.get('FatherOccupation')?.value) formData.append('FatherOccupation', this.empProfileForm.get('FatherOccupation')?.value);
    formData.append('MotherName', this.empProfileForm.get('MotherName')?.value);
    formData.append('MotherNameBn', this.empProfileForm.get('MotherNameBn')?.value);
    if (this.empProfileForm.get('MotherOccupation')?.value) formData.append('MotherOccupation', this.empProfileForm.get('MotherOccupation')?.value);


    formData.append('MaritalStatus', this.empProfileForm.get('MaritalStatus')?.value);
    if (this.empProfileForm.get('MaritalStatus')?.value == "M") {
      formData.append('SpouseName', this.empProfileForm.get('SpouseName')?.value);
      formData.append('SpouseNameBn', this.empProfileForm.get('SpouseNameBn')?.value);
      formData.append('SpouseNID', this.empProfileForm.get('SpouseNID')?.value);
      formData.append('SpouseContactNo', this.empProfileForm.get('SpouseContactNo')?.value);
      if (this.empProfileForm.get('SpousePic')?.value) formData.append('SpousePic', this.empProfileForm.get('SpousePic')?.value);
      if (this.empProfileForm.get('SpouseOccupation')?.value) formData.append('SpouseOccupation', this.empProfileForm.get('SpouseOccupation')?.value);
      if (this.empProfileForm.get('NoOfChild')?.value) formData.append('NoOfChild', this.empProfileForm.get('NoOfChild')?.value);
    }
    else if (this.empProfileForm.get('MaritalStatus')?.value == "D" && this.empProfileForm.get('DivorcedDate')?.value)
      formData.append('DivorcedDate', this.empProfileForm.get('DivorcedDate')?.value);
    else if (this.empProfileForm.get('MaritalStatus')?.value == "W" && this.empProfileForm.get('WidowerDate')?.value)
      formData.append('WidowerDate', this.empProfileForm.get('WidowerDate')?.value);
    if (this.empProfileForm.get('Height')?.value) formData.append('Height', this.empProfileForm.get('Height')?.value);
    if (this.empProfileForm.get('Weight')?.value) formData.append('Weight', this.empProfileForm.get('Weight')?.value);
    formData.append('IdentificationMarks', this.empProfileForm.get('IdentificationMarks')?.value);
    formData.append('DepartmentId', this.empProfileForm.get('DepartmentId')?.value);
    formData.append('DesignationId', this.empProfileForm.get('DesignationId')?.value);
    formData.append('EmployeeTypeId', this.empProfileForm.get('EmployeeTypeId')?.value);
    formData.append('EmployeeStatusId', this.empProfileForm.get('EmployeeStatusId')?.value);
    formData.append('JoiningDate', this.empProfileForm.get('JoiningDate')?.value);
    if (this.empProfileForm.get('ConfirmationDate')?.value) formData.append('ConfirmationDate', this.empProfileForm.get('ConfirmationDate')?.value);
    if (this.empProfileForm.get('PermanentDate')?.value) formData.append('PermanentDate', this.empProfileForm.get('PermanentDate')?.value);
    if (this.empProfileForm.get('CircularId')?.value) formData.append('CircularId', this.empProfileForm.get('CircularId')?.value);
    formData.append('Gender', this.empProfileForm.get('Gender')?.value);
    formData.append('Religion', this.empProfileForm.get('Religion')?.value);
    formData.append('DOB', this.empProfileForm.get('DOB')?.value);
    if (this.empProfileForm.get('CountryId')?.value) formData.append('CountryId', this.empProfileForm.get('CountryId')?.value);
    formData.append('PlaceOfBirth', this.empProfileForm.get('PlaceOfBirth')?.value);
    formData.append('BloodGroup', this.empProfileForm.get('BloodGroup')?.value);
    formData.append('TIN', this.empProfileForm.get('TIN')?.value);
    formData.append('NID', this.empProfileForm.get('NID')?.value);
    if (this.empProfileForm.get('NIDPic')?.value) formData.append('NIDPic', this.empProfileForm.get('NIDPic')?.value);

    formData.append('PassportNo', this.empProfileForm.get('PassportNo')?.value);
    if (this.empProfileForm.get('DrivingLicense')?.value) {
      formData.append('DrivingLicense', this.empProfileForm.get('DrivingLicense')?.value);
      if (this.empProfileForm.get('DrivingLicenseExpDate')?.value) formData.append('DrivingLicenseExpDate', this.empProfileForm.get('DrivingLicenseExpDate')?.value);
    }
    if (this.empProfileForm.get('BankId')?.value) formData.append('BankId', this.empProfileForm.get('BankId')?.value);
    if (this.empProfileForm.get('BankBranchId')?.value) formData.append('BankBranchId', this.empProfileForm.get('BankBranchId')?.value);
    formData.append('BankAccountNo', this.empProfileForm.get('BankAccountNo')?.value);
    formData.append('PersonalEmail', this.empProfileForm.get('PersonalEmail')?.value);
    formData.append('OfficialEmail', this.empProfileForm.get('OfficialEmail')?.value);
    //debugger
    formData.append('PersonalContactNo', this.empProfileForm.get('PersonalContactNo')?.value);
    formData.append('OfficialMobileNo', this.empProfileForm.get('OfficialMobileNo')?.value);

    if (this.empProfileForm.get('PresentDivisionId')?.value) formData.append('PresentDivisionId', this.empProfileForm.get('PresentDivisionId')?.value);
    if (this.empProfileForm.get('PresentDistrictId')?.value) formData.append('PresentDistrictId', this.empProfileForm.get('PresentDistrictId')?.value);
    if (this.empProfileForm.get('PresentThanaId')?.value) formData.append('PresentThanaId', this.empProfileForm.get('PresentThanaId')?.value);
    if (this.empProfileForm.get('PresentUnionId')?.value) formData.append('PresentUnionId', this.empProfileForm.get('PresentUnionId')?.value);
    if (this.empProfileForm.get('PresentVillageId')?.value) formData.append('PresentVillageId', this.empProfileForm.get('PresentVillageId')?.value);
    formData.append('PresentAddress', this.empProfileForm.get('PresentAddress')?.value);
    if (this.empProfileForm.get('PermanentDivisionId')?.value) formData.append('PermanentDivisionId', this.empProfileForm.get('PermanentDivisionId')?.value);
    if (this.empProfileForm.get('PermanentDistrictId')?.value) formData.append('PermanentDistrictId', this.empProfileForm.get('PermanentDistrictId')?.value);
    if (this.empProfileForm.get('PermanentThanaId')?.value) formData.append('PermanentThanaId', this.empProfileForm.get('PermanentThanaId')?.value);
    if (this.empProfileForm.get('PermanentUnionId')?.value) formData.append('PermanentUnionId', this.empProfileForm.get('PermanentUnionId')?.value);
    if (this.empProfileForm.get('PermanentVillageId')?.value) formData.append('PermanentVillageId', this.empProfileForm.get('PermanentVillageId')?.value);
    formData.append('PermanentAddress', this.empProfileForm.get('PermanentAddress')?.value);
    return formData;
  }
  onEmployeeProfileSubmit() {
    if (this.empProfileForm.valid) {
      const formData = this.employeeProfile();
      if (this.empProfileForm.get('EmployeeId')?.value) {
        //debugger
        this.http.put(`${this.domain_url_hrm}employee/update`, formData, { observe: 'response' }).subscribe({
          next: (response: any) => {
            this.toastr.success(response.body.message);
            this.empProfileForm.patchValue({ Employeeid: response.body.data.id });
            //; console.log('Save successfully!', response.body.message);console.log(response.body.data.id) },
          },
          error: (error: any) => {
            if (error.status >= 400 && error.status < 500)
              if (error.error.message)
                this.toastr.warning(error.error.message);
              else
                this.toastr.warning(error.error.title);
            else this.toastr.error(error.error.message);
          }

        });
      }
      else {
        // // Replace with your API endpoint
        this.http.post(`${this.domain_url_hrm}employee/create`, formData, { observe: 'response' }).subscribe({
          next: (response: any) => {
            this.toastr.success(response.body.message);
            this.empProfileForm.patchValue({ Employeeid: response.body.data.id });
            //; console.log('Save successfully!', response.body.message);console.log(response.body.data.id) },
          },
          error: (error: any) => {
            //debugger
            if (error.status >= 400 && error.status < 500)
              if (error.error.message)
                this.toastr.warning(error.error.message);
              else
                this.toastr.warning(error.error.title);
            else this.toastr.error(error.error.message);
          }

        });
      }

    }
    else this.toastr.warning("Check All field.", 'Warning');

    //if (!this.empProfileForm.valid) {

    //this.empProfileForm.value
    // this.apiService.addEmployeeProfile(this.empProfileForm.value).subscribe({
    //   next: (response) => {
    //     //this.toastr.success(response.message, 'Success');
    //     this.empProfileForm.reset();
    //   },

    //   error: (error) => {
    //     if (error.type === 'warning') {
    //       //this.toastr.warning(error.message, 'Warning');
    //     } else if (error.type === 'strongerror') {
    //       // this.toastr.error(error.message, 'Error');
    //     } else {
    //       //this.toastr.error(error.message);
    //     }
    //     this.isSubmitting = false;
    //   },
    //   complete: () => {
    //     this.isSubmitting = false;
    //   }
    // });
  }
  navigateToList() {
    this.router.navigate(['/hr/hrm/emp-list']);
  }
  /* Reset*/
  onEmployeeProfileReset() {
    this.empProfileForm.reset();
  }


  /* ───────── convenience getters for template error states ────────────── */
  get f() {
    return this.empProfileForm.controls;
  }

  getIconClass(index: number): string {
    if (index === 0) return 'bi bi-info-circle'; // bi bi-card-list || bi bi-table || 
    if (index === 1) return 'bi bi-file-earmark-text'; // bi bi-journal-text || bi bi-clipboard-data
    if (index === 2) return 'bi bi-person-vcard';  // bi bi-person-vcard || bi bi-file-person || bi bi-person-badge
    return 'bi bi-check-circle';
  }
}

/* ------------- small extension on FormGroup for step 1 validity --------- */
/* optional, purely for readability */
declare module '@angular/forms' {
  interface FormGroup {
    readonly validStepOne: boolean;
    readonly validStepTwo: boolean;
    readonly validStepThree: boolean;
  }
}
Object.defineProperty(Object.getPrototypeOf(new FormGroup({})), 'validStepOne', {
  get() {
    /* put every step‑1 control key here */
    const keysStep1 = [
      'employeeCode',
      'office',
      'officialEmail',
      'FatherName',
      'MaritalStatus', // add others if you later mark them required 
    ];
    // @ts-ignore
    return keysStep1.every(k => this.get(k)?.valid);
  }
});

Object.defineProperty(Object.getPrototypeOf(new FormGroup({})), 'validStepTwo', {
  get() {
    /* put every step‑2 control key here */
    const keysStep2 = [
      'Degree', // add others if you later mark them required
      'PassDate'
    ];
    // @ts-ignore
    return keysStep2.every(k => this.get(k)?.valid);
  }
});

Object.defineProperty(Object.getPrototypeOf(new FormGroup({})), 'validStepThree', {
  get() {
    /* put every step‑2 control key here */
    const keysStep3 = [
      'FatherName', // add others if you later mark them required
      'MaritalStatus'
    ];
    // @ts-ignore
    return keysStep3.every(k => this.get(k)?.valid);
  }
});