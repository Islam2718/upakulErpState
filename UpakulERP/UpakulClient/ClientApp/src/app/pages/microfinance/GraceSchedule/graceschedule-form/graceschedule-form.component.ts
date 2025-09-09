import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule, formatDate } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { ToastrService } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { GraceSchedule } from '../../../../models/microfinance/GraceSchedule/graceschedule.model';
import { GraceScheduleService } from '../../../../services/microfinance/GraceSchedule/graceschedule.service';
import flatpickr from 'flatpickr';
import { forkJoin } from 'rxjs';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface OfficeTypeValue {
  text: string;
  value: string;
}
interface GroupTypeValue {
  text: string;
  value: string;
}
interface LoanTypeValue {
  text: string;
  value: string;
}

@Component({
  selector: 'app-graceschedule-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    BsDatepickerModule,
  ],
  templateUrl: './graceschedule-form.component.html',
  styleUrl: './graceschedule-form.component.css'
})
export class GraceScheduleFormComponent implements OnInit, AfterViewInit {
  qry: string | null = null;
  gracescheduleForm!: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  button = Button;
  isPermitted: boolean = false;
  graceschedule: GraceSchedule[] = [];
  officeTypes: OfficeTypeValue[] = [];
  groupTypes: GroupTypeValue[] = [];
  loans: LoanTypeValue[] = [];

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    public router: Router,
    private toastr: ToastrService,
    private gracescheduleservice: GraceScheduleService
  ) { }

  ngAfterViewInit() {
    flatpickr('.dtpickr', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

  ngOnInit(): void {
   const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }
    this.gracescheduleForm = this.fb.group({
      id: [0],
      reason: ['', Validators.required],
      officeId: ['', Validators.required],
      groupId: ['', Validators.required],
      loanId: [null],
      graceFrom: ['', Validators.required],
      graceTo: ['', Validators.required],
    });

    // Load dropdowns first, then patch form if editing
    const officeDropdown$ = this.gracescheduleservice.getBranchOfficeDropdown();
    const groupDropdown$ = this.gracescheduleservice.getGroupTypeDropdown();

  forkJoin([officeDropdown$, groupDropdown$]).subscribe({
  next: ([offices, groups]) => {
    this.officeTypes = offices;
    this.groupTypes = groups;

    const id = history.state.id;
    if (id) {
      // Edit mode: load data
      this.isEditMode = true;
      this.gracescheduleservice.getGraceScheduleById(id).subscribe(res => {
        if (res) {
          this.gracescheduleForm.patchValue({
            id: res.id,
            reason: res.reason,
            officeId: res.officeId,
            groupId: res.groupId,
            loanId: res.loanId,
            graceFrom: formatDate(res.graceFrom, 'dd-MMM-yyyy', 'en-US'),
            graceTo: formatDate(res.graceTo, 'dd-MMM-yyyy', 'en-US'),
          });
        }
      });
    } else {
      // Create mode: auto-select office
      const personalData = localStorage.getItem('personal');
      if (personalData) {
        const parsed = JSON.parse(personalData);
        // assuming parsed.officeId is the default office you want to set
        this.gracescheduleForm.patchValue({
          officeId: parsed.office_id || offices[0]?.value // fallback: first office in dropdown
        });
      } else {
        // no personal data, fallback to first option
        if (offices.length > 0) {
          this.gracescheduleForm.patchValue({
            officeId: offices[0].value
          });
        }
      }
    }
  },
  error: (err) => {
    console.error('Error loading dropdowns:', err);
  }
});

    // Subscribe to date changes for any extra logic if needed
    this.gracescheduleForm.get('graceFrom')?.valueChanges.subscribe(() => this.calculateDays());
    this.gracescheduleForm.get('graceTo')?.valueChanges.subscribe(() => this.calculateDays());

    this.calculateDays();
  }

  calculateDays() {
    const fromDate = new Date(this.gracescheduleForm.get('graceFrom')?.value);
    const toDate = new Date(this.gracescheduleForm.get('graceTo')?.value);
    if (fromDate && toDate && toDate >= fromDate) {
      // your calculation logic here if needed
    }
  }
  onDropDownChange(event: any) {
    const selectedBank = event.target.value; // Get selected value correctly     
  }
  
  onSubmit() {
    if (this.gracescheduleForm.valid) {
      this.isSubmitting = true;

      const formValue = {
        ...this.gracescheduleForm.value,
        graceFrom: new Date(this.gracescheduleForm.value.graceFrom).toISOString(),
        graceTo: new Date(this.gracescheduleForm.value.graceTo).toISOString(),
      };

      if (this.isEditMode) {
        this.gracescheduleservice.UpdateData(formValue).subscribe({
          next: (response) => {
            this.router.navigate(['mf/graceschedule/graceschedule-list']);
            this.toastr.success(response.message, 'Success');
          },
          error: (error) => {
            this.toastr.error(error.message || 'Update failed');
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        this.gracescheduleservice.addData(formValue).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.gracescheduleForm.reset();
          },
          error: (error) => {
            if (error.type === 'warning') this.toastr.warning(error.message, 'Warning');
            else if (error.type === 'strongerror') this.toastr.error(error.message, 'Error');
            else this.toastr.error(error.message);
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }
    }
  }

  navigateToList() {
    this.router.navigate(['mf/graceschedule/graceschedule-list']);
  }

  onReset(): void {
    this.gracescheduleForm.reset();
  }
}
