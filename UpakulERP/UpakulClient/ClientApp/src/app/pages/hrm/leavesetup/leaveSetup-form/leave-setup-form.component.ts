import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { LeavesetupService } from '../../../../services/hrm/leavesetup/leavesetup.service';
import { forkJoin } from 'rxjs';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface DropdownValue {
  text: string;
  value: string;
}

@Component({
  selector: 'app-leave-setup-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './leave-setup-form.component.html',
  styleUrls: ['./leave-setup-form.component.css']
})
export class LeaveSetupFormComponent implements OnInit, AfterViewInit {
  qry: string | null = null;
  dataForm!: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  leaveCategories: DropdownValue[] = [];
  employeeTypes: DropdownValue[] = [];
  genderList: DropdownValue[] = [];

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: LeavesetupService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadDropdownsAndData();
  }

  ngAfterViewInit() {
    flatpickr('#datepickerValCalender', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

  private initializeForm(): void {
    this.dataForm = this.fb.group({
      LeaveTypeId: [null],
      LeaveCategoryId: ['', Validators.required],
      LeaveTypeName: ['', Validators.required],
      EmployeeTypeId: [''],
      YearlyMaxLeave: ['', [Validators.required, Validators.min(0)]],
      YearlyMaxAvailDays: ['', [Validators.required, Validators.min(0)]],
      LeaveGender: [''],
      EffectiveStartDate: ['', Validators.required],
      EffectiveEndDate: [null] // Not required but cannot be null
    });
  }

  private loadDropdownsAndData(): void {
    const id = history.state?.LeaveTypeId;
    forkJoin({
      categories: this.apiService.getLeaveCategories(),
      employeeTypes: this.apiService.getEmployeeTypes(),
      genders: this.apiService.getGenders()
    }).subscribe({
      next: ({ categories, employeeTypes, genders }) => {
        this.leaveCategories = categories;
        this.employeeTypes = employeeTypes;
        this.genderList = genders;

        if (id) {
          this.isEditMode = true;
          this.loadEditData(id);
        }
      },
      error: () => this.toastr.error('Dropdown data load failed')
    });
  }

  private loadEditData(id: string): void {
    this.apiService.getLeaveSetupById(id).subscribe({
      next: (res) => {
        if (res) {
          this.dataForm.patchValue({
            LeaveTypeId: res.leaveTypeId ?? null,
            LeaveCategoryId: res.leaveCategoryId ?? '',
            LeaveTypeName: res.leaveTypeName ?? '',
            EmployeeTypeId: res.employeeTypeId ?? '',
            YearlyMaxLeave: res.yearlyMaxLeave ?? '',
            YearlyMaxAvailDays: res.yearlyMaxAvailDays ?? '',
            LeaveGender: res.leaveGender ?? '',
            EffectiveStartDate:formatDate(res.effectiveStartDate, 'dd-MMM-yyyy', 'en-US'),
            EffectiveEndDate: formatDate(res.effectiveEndDate, 'dd-MMM-yyyy', 'en-US')
          });
        }
      },
      error: () => this.toastr.error('Failed to load form data')
    });
  }

  

  onLeaveCategoryChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement | null;
    const selectedValue = selectElement?.value || '';
    const selectedCategory = this.leaveCategories.find(c => c.value === selectedValue);
    this.dataForm.patchValue({
      LeaveTypeName: selectedCategory ? selectedCategory.text : ''
    });
  }

  onSubmit(): void {
    if (this.dataForm.invalid) {
      this.toastr.warning('Please fill all required fields');
      return;
    }

    this.isSubmitting = true;
    const formValue = { ...this.dataForm.value };

    // Convert date inputs to dd-MMM-yyyy format for backend
    formValue.EffectiveEndDate = formValue.EffectiveEndDate;

    if (!formValue.EffectiveStartDate) {
      this.toastr.error('Invalid start date');
      this.isSubmitting = false;
      return;
    }

    const action = this.isEditMode
      ? this.apiService.updateLeaveSetup(formValue)
      : this.apiService.addLeaveSetup(formValue);

    action.subscribe({
      next: (response) => {
        this.toastr.success(response.message, 'Success');
        this.router.navigate(['/hr/hrm/leave-setup-list']);
      },
      error: (error) => {
         if (error.type === 'warning')
              this.toastr.warning(error.message, 'Warning');
            else if (error.type === 'strongerror')
              this.toastr.error(error.message, 'Error');
            else
              this.toastr.error(error.message);
        this.isSubmitting = false;
      },
      complete: () => (this.isSubmitting = false)
    });
  }

  onReset(): void {
    this.dataForm.reset();
    this.toastr.info('Form reset');
  }

  navigateToList(): void {
    this.router.navigate(['/hr/hrm/leave-setup-list']);
  }

  // // Use this in your HTML if you want to display dates in dd-MMM-yyyy format
  // public formatDateForDisplay(dateString: string | null | undefined): string {
  //   if (!dateString) return '';
  //   try {
  //     return formatDate(dateString, 'dd-MMM-yyyy', 'en-US');
  //   } catch {
  //     return '';
  //   }
  // }
}
