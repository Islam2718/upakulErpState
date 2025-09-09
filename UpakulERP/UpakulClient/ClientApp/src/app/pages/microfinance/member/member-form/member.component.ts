import { Component, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule, formatDate } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { MemberService } from '../../../../services/microfinance/member/member.service';
import { CommonGlobalServiceService } from '../../../../services/generic/common-global-service.service';
import { DefaultGlobalConfig, ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
// import flatpickr from 'flatpickr';
import { MemberFormReviewModalComponent } from '../components/member-form-review-modal/member-form-review-modal.component';
import { Member } from '../../../../models/microfinance/member/member';
import { firstValueFrom, Observable, of, switchMap, tap } from 'rxjs';
import { WebcamComponent } from '../../../../shared/webcam/webcam.component';
import { NgSelectModule } from '@ng-select/ng-select';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface DropdownValue {
  text: string;
  value: string;
  selected: boolean;
}

@Component({
  selector: 'app-create-member',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MemberFormReviewModalComponent,
    WebcamComponent,
    NgSelectModule,
  ],
  templateUrl: './member.component.html',
  styleUrl: './member.component.css',
})
export class CreateMemberComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  profileThumbSrc: string = '../../../assets/img/thumb/prof.webp';
  idThumbSrc: string = '../../../assets/img/thumb/nid.jpg';
  signatureThumbSrc: string = '../../../assets/img/thumb/signature.png';

  isAgeVlid = false;
  isSubmitting = false;
  isEditMode = false;
  successMessage = '';
  message = '';
  spouseOption: boolean = false;
  button = Button;
  showReviewModal: boolean | undefined;

  showWebcam: any;
  capturedImageUrlProfile: string | null = null;
  capturedImageUrlSignature: string | null = null;
  capturedImageUrlNidFront: string | null = null;
  capturedImageUrlNidBack: string | null = null;

  
  memberAllDropdown: any = {
    group: [],
    division: [],
    district: [],
    upazila: [],
    referenceMember: [],
  };

  present_district_DropdownValues: DropdownValue[] = [];
  present_Upazila_DropdownValues: DropdownValue[] = [];
  present_union_DropdownValues: DropdownValue[] = [];
  present_village_DropdownValues: DropdownValue[] = [];

  permanent_district_DropdownValues: DropdownValue[] = [];
  permanent_Upazila_DropdownValues: DropdownValue[] = [];
  permanent_union_DropdownValues: DropdownValue[] = [];
  permanent_village_DropdownValues: DropdownValue[] = [];

  isShown: boolean = true; // hidden by default
  selectedValue: any;
  transactionDate: string | null = '';
  isPermitted: boolean = false;
  division: any;
  formData: any;
  editId: any;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: MemberService,
    private commonGlobalService: CommonGlobalServiceService,
    private toastr: ToastrService,
    public router: Router,
    private memberService: MemberService // NgSelectModule
  ) {
    this.dataForm = this.fb.group({
      memberId: [null],
      groupId: [null, Validators.required],
      admissionDate: ['', Validators.required],
      memberName: ['', Validators.required],
      MemberCode: [Validators.required, Validators.pattern(/^\S*$/)],
      occupationId: ['', Validators.required],
      fatherName: ['', Validators.required],
      motherName: ['', Validators.required],
      maritalStatus: ['', Validators.required],
      spouseName: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      // startDate: formatDate(holiday.startDate, 'dd-MMM-yyyy', 'en-US'),
      birthYear: ['', Validators.required],
      age: ['', Validators.required],

      nationalId: ['', Validators.required],
      smartCard: ['', Validators.required],
      nidVerified: [null],
      birthCertificate: ['', Validators.required],
      birthCertificateVerified: ['', Validators.required],
      tin: ['', Validators.required],
      otherIdType: ['', Validators.required],
      otherIdNumber: ['', Validators.required],
      contactNoOwn: ['', Validators.required],
      mobileNumber: [''],
      academicQualification: ['', Validators.required],
      academicQualificationId: [null, Validators.required],
      // remarks: ['', Validators.required],
      memberRemarks: [null, Validators.required],
      noOfDependents: ['', Validators.required],

      memberImgUrl: ['', Validators.required],
      memberImg: [null, Validators.required],
      signatureImgUrl: ['', Validators.required],
      signatureImg: [null, Validators.required],
      nidFrontImgUrl: ['', Validators.required],
      nidFrontImg: [null, Validators.required],
      nidBackImgUrl: ['', Validators.required],
      nidBackImg: [null, Validators.required],

      authorizedPersonId: [null, Validators.required], //may no need
      authorizedEmployeeId: [null, Validators.required],
      ovalDate: ['', Validators.required],
      maximumMember: ['', Validators.required],
      currentMember: ['', Validators.required],
      admissionFee: ['', Validators.required],
      passbookFee: ['', Validators.required],
      applicationNo: ['', Validators.required],
      passbookNo: ['', Validators.required],
      verificationNote: ['', Validators.required],
      // UploadDocument: ['', Validators.required],

      //  Address
      presentCountryId: [''],
      presentDivisionId: [''],
      presentDistrictId: [null],
      presentUpazilaId: [''],
      presentUnionId: [''],
      presentVillageId: [''],
      presentAddress: [''],

      permanentCountryId: [''],
      permanentDivisionId: [''],
      permanentDistrictId: [''],
      permanentUpazilaId: [''],
      permanentUnionId: [''],
      permanentVillageId: [''],
      permanentAddress: [''],

      referenceMemberId: ['', Validators.required],
      identifierName: ['', Validators.required],
      relationWithIdentifier: ['', Validators.required],
      totalFamilyMember: ['', Validators.required],
      totalChildren: ['', Validators.required],
      totalIncome: ['', Validators.required],
      incomeType: ['', Validators.required],
      incomeAmt: ['', Validators.required],
      residentialHouseArea: ['', Validators.required],
      arableLandArea: ['', Validators.required],
      previouslyLoanReceiver: [false, Validators.required],
      relatedOtherProgram: [false, Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],
      memberOfOtherOrganization: [false],
    });
  }

  // set a required array
  requiredList = [
    'groupId',
    'memberName',
    'occupationId',
    'fatherName',
    'motherName',
    // 'maritalStatus',
    // 'spouseName',
    'gender',
    'dateOfBirth',
    'age',
    'nationalId',
    'smartCard',
    'birthCertificate',
    // 'tin',
    // 'otherIdType',
    // 'otherIdNumber',
    'contactNoOwn',
    'mobileNumber',
    'academicQualificationId',

    // 'admissionFee',
    // 'passbookFee',
    // 'applicationNo',
    // 'passbookNo',
    'authorizedEmployeeId',
    'admissionDate',
    'nidVerified',
    'birthCertificateVerified',
    'nidFront',
    'nidBack',
    'photo',
    // 'signature',
    'presentCountryId',
    'presentDivisionId',
    'presentDistrictId',
    'presentUpazilaId',
    'presentUnionId',
    'presentVillageId',
    'presentAddress',

    'permanentCountryId',
    'permanentDivisionId',
    'permanentDistrictId',
    'permanentUpazilaId',
    'permanentUnionId',
    'permanentVillageId',
    'permanentAddress',

    'identifierName',
    // 'relationWithIdentifier',
    // 'referenceMemberId',
    // 'incomeType',
    // 'incomeAmt',
    // 'totalFamilyMember',
    // 'totalChildren',
    // 'totalIncome',
    // 'residentialHouseArea',
    // 'latitude',
    // 'longitude',
    // 'arableLandArea',
  ];

  base64ToFile(base64: string, filename: string): File {
    const arr = base64.split(',');
    const mime = arr[0].match(/:(.*?);/)![1];
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);
    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
  }
  handleCaptureProfile(file: File) {
    const reader = new FileReader();

    reader.onload = () => {
      const base64Image = reader.result as string;
      this.capturedImageUrlProfile = base64Image;

      // Convert base64 to File object
      const convertedFile = this.base64ToFile(
        base64Image,
        'captured-image.png'
      );

      // Store real File for form-data
      this.dataForm.patchValue({
        memberImg: convertedFile,
      });
    };

    reader.readAsDataURL(file);
  }
  handleUploadProfile(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();

      reader.onload = () => {
        const base64Image = reader.result as string;
        this.capturedImageUrlProfile = base64Image;

        // Set the image data in the form
        this.dataForm.patchValue({
          memberImg: file, // <-- This is the File object for form-data
        });
      };

      reader.readAsDataURL(file);
    }
  }
  // base 64
  handleCaptureSignature(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      const base64Image = reader.result as string;
      this.capturedImageUrlSignature = base64Image;
      // Convert base64 to File object
      const convertedFile = this.base64ToFile(
        base64Image,
        'captured-image.png'
      );
      // Store real File for form-data
      this.dataForm.patchValue({
        signatureImg: convertedFile,
      });
    };
    reader.readAsDataURL(file);
  }
  handleUploadSignature(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const base64Image = reader.result as string;
        this.capturedImageUrlSignature = base64Image;
        // Set the image data in the form
        this.dataForm.patchValue({
          signatureImg: file, // <-- This is the File object for form-data
        });
      };
      reader.readAsDataURL(file);
    }
  }
  // base 64
  handleCaptureNidFront(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      const base64Image = reader.result as string;
      this.capturedImageUrlNidFront = base64Image;
      // Convert base64 to File object
      const convertedFile = this.base64ToFile(
        base64Image,
        'captured-image.png'
      );
      // Store real File for form-data
      this.dataForm.patchValue({
        nidFrontImg: convertedFile,
      });
    };
    reader.readAsDataURL(file);
  }
  handleUploadNidFront(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const base64Image = reader.result as string;
        this.capturedImageUrlNidFront = base64Image;
        // Set the image data in the form
        this.dataForm.patchValue({
          nidFrontImg: file, // <-- This is the File object for form-data
        });
      };
      reader.readAsDataURL(file);
    }
  }
  // base 64
  handleCaptureNidBack(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      const base64Image = reader.result as string;
      this.capturedImageUrlNidBack = base64Image;
      const convertedFile = this.base64ToFile(
        base64Image,
        'captured-image.png'
      );
      this.dataForm.patchValue({
        nidBackImg: convertedFile,
      });
    };
    reader.readAsDataURL(file);
  }
  handleUploadNidBack(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const base64Image = reader.result as string;
        this.capturedImageUrlNidBack = base64Image;
        this.dataForm.patchValue({
          nidBackImg: file, // <-- This is the File object for form-data
        });
      };
      reader.readAsDataURL(file);
    }
  }

  ngOnInit() {
    const transDate = localStorage.getItem('transactionDate');
    if (transDate) {
      this.transactionDate = transDate;
    }
    this.dataForm.patchValue({ admissionDate: this.transactionDate });

    const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }

    this.dataForm.get('dateOfBirth')?.valueChanges.subscribe((dob) => {
      if (dob) {
        const age = this.calculateAge(new Date(dob));
        this.dataForm.get('age')?.setValue(age, { emitEvent: false });
        const birthYear = this.findBirthYear(new Date(dob));
        this.dataForm
          .get('birthYear')
          ?.setValue(birthYear, { emitEvent: false });
      }
    });
    this.loadAllDropdown();

    // for edit get memberId
    this.editId = history.state.memberId;
    if (this.editId) {
      this.getMemberByIdFunc(this.editId);
    }
  }
  ngAfterViewInit() {
    flatpickr('#datepickerVal', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }
  // constructor(private memberService: MemberService) {}
  async getMemberByIdFunc(memberId: number | string) {
    try {
      const data = await firstValueFrom(
        this.memberService.getDataById(memberId)
      );
      this.dataForm.patchValue({
        ...data,
        groupId: String(data.groupId),
        academicQualificationId: String(data.academicQualificationId),
        admissionDate: formatDate(data.admissionDate, 'dd-MMM-yyyy', 'en-US'),
        dateOfBirth: formatDate(data.dateOfBirth, 'dd-MMM-yyyy', 'en-US') /*? data.dateOfBirth.split('T')[0] : null*/,
        authorizedEmployeeId: String(data.authorizedEmployeeId),
        memberRemarks: String(data.memberRemarks),
        otherIdNumber: String(data.otherIdNumber),
        presentCountryId: String(data.presentCountryId),
        // presentDivisionId: String(data.presentDivisionId),
        // presentDistrictId: String(data.presentDistrictId),
        // presentUpazilaId: String(data.presentUpazilaId),
        // presentUnionId: String(data.presentUnionId),
        // presentVillageId: String(data.presentVillageId),
      });

      this.spouseOption = true;
      this.populateAddressOnEdit(data);
    } catch (error) {
      console.error('Error fetching member:', error);
    }
  }

  populateAddressOnEdit(data: any) {
    if (data.presentDivisionId) {
      this.loadGeoData('presentDivisionId', data.presentDivisionId).subscribe(
        () => {
          if (data.presentDistrictId) {
            this.dataForm.patchValue({
              presentDistrictId: data.presentDistrictId,
            });

            this.loadGeoData(
              'presentDistrictId',
              data.presentDistrictId
            ).subscribe(() => {
              if (data.presentUpazilaId) {
                this.dataForm.patchValue({
                  presentUpazilaId: data.presentUpazilaId,
                });

                this.loadGeoData(
                  'presentUpazilaId',
                  data.presentUpazilaId
                ).subscribe(() => {
                  if (data.presentUnionId) {
                    this.dataForm.patchValue({
                      presentUnionId: data.presentUnionId,
                    });

                    this.loadGeoData(
                      'presentUnionId',
                      data.presentUnionId
                    ).subscribe(() => {
                      if (data.presentVillageId) {
                        this.dataForm.patchValue({
                          presentVillageId: data.presentVillageId,
                        });
                      }
                    });
                  }
                });
              }
            });
          }
        }
      );
    }

    if (data.permanentDivisionId) {
      this.loadGeoData('permanentDivisionId', data.permanentDivisionId).subscribe(
        () => {
          if (data.permanentDistrictId) {
            this.dataForm.patchValue({
              permanentDistrictId: data.permanentDistrictId,
            });

            this.loadGeoData(
              'permanentDistrictId',
              data.permanentDistrictId
            ).subscribe(() => {
              if (data.permanentUpazilaId) {
                this.dataForm.patchValue({
                  permanentUpazilaId: data.permanentUpazilaId,
                });

                this.loadGeoData(
                  'permanentUpazilaId',
                  data.permanentUpazilaId
                ).subscribe(() => {
                  if (data.permanentUnionId) {
                    this.dataForm.patchValue({
                      permanentUnionId: data.permanentUnionId,
                    });

                    this.loadGeoData(
                      'permanentUnionId',
                      data.permanentUnionId
                    ).subscribe(() => {
                      if (data.permanentVillageId) {
                        this.dataForm.patchValue({
                          permanentVillageId: data.permanentVillageId,
                        });
                      }
                    });
                  }
                });
              }
            });
          }
        }
      );
    }
  }

  selectedLabels: { [key: string]: string } = {};
  onGeoLocationChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const controlName = selectElement.getAttribute('formControlName')!;
    const value = Number(selectElement.value);
    const text = selectElement.options[selectElement.selectedIndex].text;
    this.selectedLabels[controlName] = text;

    if (value) {
      this.loadGeoData(controlName, value).subscribe();
    } else {
      // reset dropdowns if no value
      switch (controlName) {
        case 'presentDivisionId':
          this.present_district_DropdownValues = [];
          break;
        case 'presentDistrictId':
          this.present_Upazila_DropdownValues = [];
          break;
        case 'presentUpazilaId':
          this.present_union_DropdownValues = [];
          break;
        case 'presentUnionId':
          this.present_village_DropdownValues = [];
          break;

        case 'permanentDivisionId':
          this.permanent_district_DropdownValues = [];
          break;
        case 'permanentDistrictId':
          this.permanent_Upazila_DropdownValues = [];
          break;
        case 'permanentUpazilaId':
          this.permanent_union_DropdownValues = [];
          break;
        case 'permanentUnionId':
          this.permanent_village_DropdownValues = [];
          break;
      }
    }
  }

  // onGeoLocationChange(event: any) {
  //   // const input = event.target as HTMLInputElement;
  //   // const controlName = input.getAttribute('formControlName')!;
  //   // const value = Number(input.value);
  //   // const text = input.options[input.selectedIndex].text;
  //   const selectElement = event.target as HTMLSelectElement;
  //   // Get formControlName
  //   const controlName = selectElement.getAttribute('formControlName')!;
  //   // Get selected value
  //   const value = Number(selectElement.value);
  //   // Get selected text (label)
  //   const text = selectElement.options[selectElement.selectedIndex].text;

  //   if (value) {
  //     this.loadGeoData(controlName, value).subscribe();
  //   } else {
  //     // reset if no value
  //     switch (controlName) {
  //       case 'presentDivisionId':
  //         this.present_district_DropdownValues = [];
  //         break;
  //       case 'presentDistrictId':
  //         this.present_Upazila_DropdownValues = [];
  //         break;
  //       case 'presentUpazilaId':
  //         this.present_union_DropdownValues = [];
  //         break;
  //       case 'presentUnionId':
  //         this.present_village_DropdownValues = [];
  //         break;

  //       case 'permanentDivisionId':
  //         this.permanent_district_DropdownValues = [];
  //         break;
  //       case 'permanentDistrictId':
  //         this.permanent_Upazila_DropdownValues = [];
  //         break;
  //       case 'permanentUpazilaId':
  //         this.permanent_union_DropdownValues = [];
  //         break;
  //       case 'permanentUnionId':
  //         this.permanent_village_DropdownValues = [];
  //         break;
  //     }
  //   }
  // }

  loadGeoData(controlName: string, parentId: number) {
    return this.apiService.getGeoLocationDropdown(parentId).pipe(
      tap((data) => {
        switch (controlName) {
          case 'presentDivisionId':
            this.present_district_DropdownValues = data;
            break;
          case 'presentDistrictId':
            this.present_Upazila_DropdownValues = data;
            break;
          case 'presentUpazilaId':
            this.present_union_DropdownValues = data;
            break;
          case 'presentUnionId':
            this.present_village_DropdownValues = data;
            break;

          case 'permanentDivisionId':
            this.permanent_district_DropdownValues = data;
            break;
          case 'permanentDistrictId':
            this.permanent_Upazila_DropdownValues = data;
            break;
          case 'permanentUpazilaId':
            this.permanent_union_DropdownValues = data;
            break;
          case 'permanentUnionId':
            this.permanent_village_DropdownValues = data;
            break;
        }
      })
    );
  }

  async loadAllDropdown() {
    try {
      const response = await firstValueFrom(
        this.apiService.memberAllDropdown()
      );
      this.memberAllDropdown = response || [];
      // Find the country with selected = true
      const selectedCountry = this.memberAllDropdown.country.find(
        (c: { selected: string | boolean }) =>
          c.selected === true || c.selected === 'true'
      );
      // Patch the form with its value
      this.dataForm.patchValue({
        permanentCountryId: selectedCountry ? selectedCountry.value : null,
        presentCountryId: selectedCountry ? selectedCountry.value : null,
      });
    } catch (error) {
      console.error('_dropdown:', error);
    }
  }

  jsonToFormData(data: any): FormData {
    const formData = new FormData();
    Object.keys(data).forEach((key) => {
      if (data[key] !== null && data[key] !== undefined) {
        formData.append(key, data[key]);
      }
    });
    return formData;
  }

  // private handleServerErrors(error: any) {
  //   const serverErrors = error?.error?.errors;
  //   if (serverErrors) {
  //     Object.keys(serverErrors).forEach((field) => {
  //       const control = this.dataForm.get(field);
  //       if (control) {
  //         control.setErrors({ serverError: serverErrors[field][0] });
  //       }
  //     });
  //   }
  // }

  @ViewChild('reviewModal') reviewModal!: MemberFormReviewModalComponent;
  // member form submission
  onSubmitMemberForm() {
    if (this.editId) {
      this.dataForm.patchValue({ memberId: this.editId });
      const formData = new FormData();
      Object.keys(this.dataForm.controls).forEach((key) => {
        const control = this.dataForm.get(key);
        if (control && control.value !== null && control.value !== undefined) {
          if (control.value instanceof Date) {
            formData.append(key, control.value.toISOString());
          } else if (typeof control.value === 'object') {
            formData.append(key, JSON.stringify(control.value));
          } else {
            formData.append(key, control.value);
          }
        }
      });

      this.memberService.updateMember(formData).subscribe({
        next: (response) => {
          this.dataForm.reset();
          this.toastr.success(response.message, 'Success');
          this.router.navigate(['mf/microfinance/member-list']);
        },
        error: (error) => {
          if (error.type === 'warning')
            this.toastr.warning(error.message, 'Warning');
          else if (error.type === 'strongerror')
            this.toastr.error(error.message, 'Error');
          else
            this.toastr.error(error.message);
        },
      });
    } else {
      const formData = new FormData();
      Object.keys(this.dataForm.controls).forEach((key) => {
        const value = this.dataForm.get(key)?.value;
        if (value !== null && value !== undefined) {
          formData.append(key, value);
        }
      });

      this.apiService.checkMember(formData).subscribe({
        next: (response) => {
          this.reviewModal.data = this.dataForm.value; // Still useful for showing the modal

          this.reviewModal.data.groupName = this.getTextById(this.memberAllDropdown.group, this.dataForm.value.groupId);
          this.reviewModal.data.occupationLabel = this.getTextById(this.memberAllDropdown.occupation, this.dataForm.value.occupationId);
          this.reviewModal.data.maritalStatusLabel = this.getTextById(this.memberAllDropdown.maritalStatus, this.dataForm.value.maritalStatus);
          this.reviewModal.data.genderLabel = this.getTextById(this.memberAllDropdown.gender, this.dataForm.value.gender);
          // this.reviewModal.data.authorizedEmployeeName = this.getTextById( this.memberAllDropdown.authorizedEmployeeId, this.dataForm.value.authorizedEmployeeId);
          // this.reviewModal.data.otherIdTypeLabel = this.getTextById( this.memberAllDropdown.otherIdType, this.dataForm.value.otherIdType);
          // this.reviewModal.data.memberRemarksLabel = this.getTextById( this.memberAllDropdown.otherIdType, this.dataForm.value.memberRemarks);
          // present address 
          this.reviewModal.data.presentCountryName = this.getTextById(this.memberAllDropdown.country, this.dataForm.value.presentCountryId);
          this.reviewModal.data.presentDivisionName = this.getTextById(this.memberAllDropdown.division, this.dataForm.value.presentDivisionId);
          this.reviewModal.data.presentDistrictName = this.getTextById(this.present_district_DropdownValues, this.dataForm.value.presentDistrictId);
          this.reviewModal.data.presentUpazilaName = this.getTextById(this.present_Upazila_DropdownValues, this.dataForm.value.presentUpazilaId);
          this.reviewModal.data.presentUnionName = this.getTextById(this.present_union_DropdownValues, this.dataForm.value.presentUnionId);
          this.reviewModal.data.presentVillageName = this.getTextById(this.present_village_DropdownValues, this.dataForm.value.presentVillageId);

          // present address 
          this.reviewModal.data.permanentCountryName = this.getTextById(this.memberAllDropdown.country, this.dataForm.value.permanentCountryId);
          this.reviewModal.data.permanentDivisionName = this.getTextById(this.memberAllDropdown.division, this.dataForm.value.permanentDivisionId);
          this.reviewModal.data.permanentDistrictName = this.getTextById(this.permanent_district_DropdownValues, this.dataForm.value.permanentDistrictId);
          this.reviewModal.data.permanentUpazilaName = this.getTextById(this.permanent_Upazila_DropdownValues, this.dataForm.value.permanentUpazilaId);
          this.reviewModal.data.permanentUnionName = this.getTextById(this.permanent_union_DropdownValues, this.dataForm.value.permanentUnionId);
          this.reviewModal.data.permanentVillageName = this.getTextById(this.permanent_village_DropdownValues, this.dataForm.value.permanentVillageId);

          const selectedGroup = this.memberAllDropdown.group.find(
            (opt: { value: any }) => opt.value === this.dataForm.value.groupId
          );
          this.reviewModal.show();
        },
        error: (error) => {
          const serverErrors = error?.error?.errors;
          if (serverErrors) {
            Object.keys(serverErrors).forEach((field) => {
              const control = this.dataForm.get(field);
              if (control) {
                control.setErrors({ serverError: serverErrors[field][0] });
              }
            });
          }
        },
        complete: () => { },
      });
    }
  }

  getTextById(list: any[], id: any): string {
    return list.find((x) => x.value === id)?.text || '';
  }

  // present address into parmanent address func
  setParmanentAddress(event: any) {
    if (event.target.checked) {
      // copy the basic values
      this.dataForm.patchValue({
        permanentCountryId: this.dataForm.get('presentCountryId')?.value,
        permanentDivisionId: this.dataForm.get('presentDivisionId')?.value,
        permanentAddress: this.dataForm.get('presentAddress')?.value,
      });

      const divisionId = this.dataForm.get('presentDivisionId')?.value;
      const districtId = this.dataForm.get('presentDistrictId')?.value;
      const upazilaId = this.dataForm.get('presentUpazilaId')?.value;
      const unionId = this.dataForm.get('presentUnionId')?.value;
      const villageId = this.dataForm.get('presentVillageId')?.value;
      // console.log('_pDIS:', districtId);

      // step 1: load districts
      if (divisionId) {
        this.loadGeoData('permanentDivisionId', divisionId).subscribe(() => {
          this.dataForm.patchValue({ permanentDistrictId: districtId });

          // step 2: load upazilas
          if (districtId) {
            this.loadGeoData('permanentDistrictId', districtId).subscribe(
              () => {
                this.dataForm.patchValue({ permanentUpazilaId: upazilaId });

                // step 3: load unions
                if (upazilaId) {
                  this.loadGeoData('permanentUpazilaId', upazilaId).subscribe(
                    () => {
                      this.dataForm.patchValue({ permanentUnionId: unionId });

                      // step 4: load villages
                      if (unionId) {
                        this.loadGeoData('permanentUnionId', unionId).subscribe(
                          () => {
                            this.dataForm.patchValue({
                              permanentVillageId: villageId,
                            });
                          }
                        );
                      }
                    }
                  );
                }
              }
            );
          }
        });
      }
    } else {
      // reset when unchecked
      this.dataForm.patchValue({
        // permanentCountryId: null,
        permanentDivisionId: null,
        permanentDistrictId: null,
        permanentUpazilaId: null,
        permanentUnionId: null,
        permanentVillageId: null,
        permanentAddress: null,
      });

      this.permanent_district_DropdownValues = [];
      this.permanent_Upazila_DropdownValues = [];
      this.permanent_union_DropdownValues = [];
      this.permanent_village_DropdownValues = [];
    }
  }

  ///Calculate DOB
  calculateAge(dateOfBirth: Date): number {
    const today = new Date();
    let age = today.getFullYear() - dateOfBirth.getFullYear();
    const monthDiff = today.getMonth() - dateOfBirth.getMonth();
    const dayDiff = today.getDate() - dateOfBirth.getDate();
    // Adjust if birthday hasn't occurred yet this year
    if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)) {
      age--;
    }
    if (age >= 18 && age <= 55) {
      this.isAgeVlid = true;
    } else {
      this.isAgeVlid = false;
    }
    return age;
  }

  ///findBirthYear
  findBirthYear(dateOfBirth: Date): number {
    const today = new Date();
    let birthyear = dateOfBirth.getFullYear();
    return birthyear;
  }

  removeSpaces(event: any) {
    event.target.value = event.target.value.replace(/\s/g, '');
    this.dataForm
      .get('memberCode')
      ?.setValue(event.target.value, { emitEvent: false });
    return 0;
  }

  OnChangeMaritalStatus(event: any) {
    const dropdownValue = event.target.value;
    if (dropdownValue == 'M') {
      this.spouseOption = true;
    } else if (dropdownValue == 'W') {
      this.spouseOption = true;
    } else {
      this.spouseOption = false;
    }
  }

  // reset form input fields
  onReset(): void {
    this.dataForm.reset();
  }
  navigateToList() {
    this.router.navigate(['mf/microfinance/member-list']);
  }
}
