import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormsModule,
  FormGroup,
  FormArray,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../../shared/enums/button.enum';
import { Message } from '../../../../../shared/enums/message.enum';
import { NgFor, CommonModule, formatDate } from '@angular/common';
import { LoanProposalsService } from '../../../../../services/microfinance/loan/loan-proposal/loan-proposals.service'; // Adjust path if needed
import { LoanProposal } from '../../../../../models/microfinance/loan-proposal/loan-proposal';
import { Member } from '../../../../../models/microfinance/member/member';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonGlobalServiceService } from '../../../../../services/generic/common-global-service.service';
import flatpickr from 'flatpickr';
import { WebcamComponent } from '../../../../../shared/webcam/webcam.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { data } from 'jquery';
import { ImageurlMappingConstant } from '../../../../../shared/image-url-mapping-constant';
import { GroupService } from '../../../../../services/microfinance/group/group.service';
import { MemberService } from '../../../../../services/microfinance/member/member.service';
import { ComponentMFService } from '../../../../../services/microfinance/components/componentMF/componentMF.service';
import { BtnService } from '../../../../../services/btn-service/btn-service';
import { ConfigService } from '../../../../../core/config.service';

export interface DropdownValue {
  value: number | string | null; // number, string বা null হতে পারে
  text: string;
  selected: boolean;
}

@Component({
  selector: 'app-loan-proposals',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    WebcamComponent,
    NgSelectModule,
  ],
  templateUrl: './loan-proposals.component.html',
  styleUrl: './loan-proposals.component.css',
})
export class LoanProposalsComponent {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  samityId = '';
  isEditMode = false;
  successMessage = '';
  message = '';
  spouseOption: boolean = false;
  button = Button;

  dropdownProposedBy: any[] = [];
  groupDropDownList: any[] = [];
  dropdownPurpose: any[] = [];
  dropdownMemberList: any[] = [];
  dropdownComponent: any[] = [];

  capturedImageUrlLoneeMemberImage: string | undefined;
  loneeGroupImgUrl: any;
  groupService: any;
  datePipe: any;
  memberDetails: any;
  // configService: any;
  domain_url_mf: any;
  imageURL: string | undefined;
  // memImgURL: any;
  memImgURL: string = '';

  ngAfterViewInit() {
    flatpickr('.dtpickr', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true,
    });
  }
  
  ngOnInit() {
    // personal info (if needed later)
    this.memImgURL = this.domain_url_mf.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL;
    // console.log('___:', this.imageURL);

    const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsedData = JSON.parse(personalData);
      // use parsedData if needed
    }

    const transactionDateString = localStorage.getItem('transactionDate'); // string | null

    if (transactionDateString) {
      const formattedDate = formatDate(
        transactionDateString,
        'dd-MMM-yyyy',
        'en-US'
      );
      this.dataForm.patchValue({ applicationDate: formattedDate });
    }

    // load dropdowns

    this.loadDropdownProposedBy();
    this.loadDropdownPurpose();
    this.loadDropdownComponent();

    // edit mode
    const id = history.state.editId;
    if (id) {
      this.getLoanByApplicationIdFunc(id);
    }
  }

  getLoanByApplicationIdFunc(applicationid: any) {
    try {
      this.apiService.getDataById(applicationid).subscribe({
        next: (data: any) => {
          // this.dataForm.patchValue({ groupId: String(data.groupId) });
          // this.selectMemberListFunc({ value: String(data.groupId) });

          //const formattedDate = formatDate(data.applicationDate, 'dd-MMM-yyyy', 'en-US');
          this.dataForm.patchValue({
            ...data,
          });
          this.dataForm.patchValue({
            applicationDate: formatDate(
              data.applicationDate,
              'dd-MMM-yyyy',
              'en-US'
            ),
          });

          this.dataForm.patchValue({ proposedBy: String(data.proposedBy) });
          this.selectGroupListFunc({ value: data.proposedBy });

          this.dataForm.patchValue({ groupId: String(data.groupId) });
          this.selectMemberListFunc({ value: String(data.groupId) });

          this.dataForm.patchValue({ memberId: String(data.memberId) });
          this.getMemberDetailsByIdFunc({ value: String(data.memberId) });

          this.dataForm.patchValue({ componentId: String(data.componentId) });
          this.dataForm.patchValue({ purposeId: String(data.purposeId) });
          this.dataForm.patchValue({
            firstGuarantorMemberId: String(data.firstGuarantorMemberId),
          });
          this.dataForm.patchValue({
            secondGuarantorMemberId: String(data.secondGuarantorMemberId),
          });
        },
      });
    } catch (error) {
      console.error('Error fetching member:', error);
    }
  }

  constructor(
    public Button: BtnService,
    // private route: ActivatedRoute

    private fb: FormBuilder,
    private apiService: LoanProposalsService,
    private apiServiceGroup: GroupService,
    private apiServiceMember: MemberService,
    private apiComponentService: ComponentMFService,
    private toastr: ToastrService,
    private commonGlobalService: CommonGlobalServiceService,
    public router: Router,
    private activeatedRoute: ActivatedRoute, //  This one, not Router,
    private http: HttpClient,
    private configService: ConfigService,
  ) {
    this.dataForm = this.fb.group({
      loanApplicationId: [null, Validators.required],
      // applicationNo:[0],
      officeId: [null, Validators.required],

      proposedBy: ['', Validators.required], //proposedBy: number,
      groupId: ['', Validators.required],
      memberId: ['', Validators.required],
      phaseNumber: ['', Validators.required],
      componentId: ['', Validators.required],
      purposeId: ['', Validators.required],
      applicationDate: ['', Validators.required],
      proposedAmount: [null, Validators.required],
      loneeGroupImgUrl: [''],
      loneeGroupImg: [''],

      firstGuarantorMemberId: ['', Validators.required],
      firstGuarantorName: ['', Validators.required],
      firstGuarantorContactNo: ['', Validators.required],
      firstGuarantorRelation: ['', Validators.required],
      firstGuarantorRemark: ['', Validators.required],

      secondGuarantorMemberId: ['', Validators.required],
      secondGuarantorName: ['', Validators.required],
      secondGuarantorContactNo: ['', Validators.required],
      secondGuarantorRelation: ['', Validators.required],
      secondGuarantorRemark: ['', Validators.required],

      // applicationStatus: ['', Validators.required],
      // approvedLevel: ['', Validators.required],

      emp_SelfFullTimeMale: ['', Validators.required],
      emp_SelfFullTimeFemale: ['', Validators.required],
      emp_SelfPartTimeMale: ['', Validators.required],
      emp_SelfPartTimeFemale: ['', Validators.required],
      emp_WageFullTimeMale: ['', Validators.required],
      emp_WageFullTimeFemale: ['', Validators.required],
      emp_WagePartTimeMale: ['', Validators.required],
      emp_WagePartTimeFemale: ['', Validators.required],
    });

    this.domain_url_mf = configService.mfApiBaseUrl();
  }

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
  handleCaptureLoneeMemberImage(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      const base64Image = reader.result as string;
      this.capturedImageUrlLoneeMemberImage = base64Image;
      // Convert base64 to File object
      const convertedFile = this.base64ToFile(
        base64Image,
        'captured-image.png'
      );
      // Store real File for form-data
      this.dataForm.patchValue({
        loneeGroupImg: convertedFile,
      });
    };
    reader.readAsDataURL(file);
  }
  handleUploadLoneeMemberImage(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();

      reader.onload = () => {
        const base64Image = reader.result as string;
        this.capturedImageUrlLoneeMemberImage = base64Image;

        // Set the image data in the form
        this.dataForm.patchValue({
          loneeGroupImg: file, // <-- This is the File object for form-data
        });
      };

      reader.readAsDataURL(file);
    }
  }

  loadDropdownMember() {
    throw new Error('Method not implemented.');
  }

  loadDropdownProposedBy() {
    this.apiService.getProposedByDropDownData().subscribe({
      next: (data) => {
        this.dropdownProposedBy = data;
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      },
    });
  }

  selectGroupListFunc(event: any) {
    if (!event?.value) {
      this.groupDropDownList = this.dropdownMemberList = [];
      this.dataForm.patchValue({
        groupId: '',
        memberId: '',
      });
      return;
    }
    //const selectElement = event.target as HTMLSelectElement;
    //const empId = parseInt(selectElement.value, 10);

    //if (isNaN(empId)) return;

    this.apiServiceGroup.getGroupDropListByEmpId(event?.value).subscribe({
      next: (data: DropdownValue[]) => {
        this.groupDropDownList = data;
        //console.log('Group list:', this.groupDropDownList);
      },
      error: (error: any) => {
        console.error('Error fetching group dropdown list:', error);
      },
    });
  }

  selectMemberListFunc(selectedItem: any) {
    if (!selectedItem?.value) {
      this.dropdownMemberList = [];
      this.dataForm.patchValue({
        memberId: '',
      });
      return;
    }

    this.apiServiceMember
      .getMemberDropListByGroupId(selectedItem?.value)
      .subscribe({
        next: (data) => {
          this.dropdownMemberList = data;
        },
      });
    // if (!selectedItem || !selectedItem.value) return;
    // const empId = parseInt(selectedItem.value, 10);

    // this.apiService.getMemberDropDownData().subscribe({
    //   next: (data) => {
    //     this.dropdownMemberList = data;
    //   },
    //   error: (error) => { console.error('Error fetching dropdown data:', error); },
    //   complete: () => { /**/ },
    // });
  }

  getMemberDetailsByIdFunc(memberId: any) {
    memberId = parseInt(memberId.value, 10);
    this.apiServiceMember.getMemberDetailById(memberId).subscribe({
      next: (data) => {
        this.memberDetails = data;
        // alert(this.memberDetails.memberName);
        //this.toastr.success('Member is available...');
      },
      error: (error) => {
        console.error('No Member Found', error);
      },
    });
  }

  loadDropdownComponent() {
    this.apiComponentService.getLoanComponentDropdown().subscribe({
      next: (data) => {
        this.dropdownComponent = data;
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        /*console.log('Dropdown data fetching completed.');*/
      },
    });
  }
  loadDropdownPurpose() {
    this.apiService.getMainPurposeDropdown().subscribe({
      next: (data) => {
        this.dropdownPurpose = data;
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      },
    });
  }

  Guarantor1OnChange(selectedItem: any) {
    const selectedValue = parseInt(selectedItem.value, 10); // 10 is radix - (convert into decimal)
    console.log('Selected Second Guarantor ID (int):', selectedValue);
    this.loadGurantor1(selectedValue);
  }

  loadGurantor1(id: number) {
    this.apiService.getMemberById(id).subscribe({
      next: (response) => {
        if (response?.data) {
          this.dataForm.patchValue({
            firstGuarantorName: response.data.memberName,
            firstGuarantorContactNo: response.data.contactNoOwn,
          });
        }
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      },
    });
  }

  Guarantor2OnChange(selectedItem: any) {
    const selectedValue = parseInt(selectedItem.value, 10); // 10 is radix - (convert into decimal)
    console.log('Selected Second Guarantor ID (int):', selectedValue);
    this.loadGurantor2(selectedValue);
  }

  loadGurantor2(id: number) {
    // console.log('thisid:', id);
    this.apiService.getMemberById(id).subscribe({
      next: (response) => {
        if (response?.data) {
          this.dataForm.patchValue({
            secondGuarantorName: String(response.data.memberName),
            secondGuarantorContactNo: response.data.contactNoOwn,
          });
        }
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
    });
  }

  jsonToFormData(data: any): FormData {
    const formData = new FormData();
    Object.keys(data).forEach((key) => {
      const value = data[key];
      if (value !== null && value !== undefined) {
        formData.append(
          key,
          typeof value === 'number' ? value.toString() : value
        );
      }
    });
    return formData;
  }

  onSubmitForm() {
    const payload: LoanProposal = { ...this.dataForm.value };
    const formData = this.jsonToFormData(payload);

    const isUpdate = !!payload.loanApplicationId;

    const request$ = isUpdate
      ? this.apiService.updateData(formData) // update if ID exists
      : this.apiService.addData(formData); // add if new

    request$.subscribe({
      next: (res) => {
        // console.log('Operation successful', res);
        this.toastr.success(
          isUpdate
            ? 'Form updated successfully!'
            : 'Form submitted successfully!'
        );

        if (isUpdate) {
          this.navigateToList(); // go to list after update
        } else {
          this.memberDetails = '';
          this.onReset(); // just reset form after create
        }
      },
      error: (error) => {
        // console.error('Error:', error);
        this.toastr.error(
          error?.message || 'Something went wrong while submitting!'
        );
      },
      complete: () => {
        console.log('Operation completed.');
      },
    });
  }

  // destinations
  navigateToList() {
    this.router.navigate(['mf/loan-proposal/loan-proposal-list']);
  }
  onReset() {
    this.dataForm.reset();
  }
}
