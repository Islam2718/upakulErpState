import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormControl, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule, formatDate } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { ToastrService } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { Holiday } from '../../../../models/hr/holiday/holiday.model';
import { HoliDayService } from '../../../../services/hrm/holiday/holiday.service';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';
@Component({
  selector: 'app-holiday-form',
  standalone: true, // Allow the component to be used without a module
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    BsDatepickerModule,

  ],
  templateUrl: './holiday-form.component.html',
  styleUrl: './holiday-form.component.css'
})
export class HolidayFormComponent implements OnInit {
  qry: string | null = null;
  holidayForm!: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  button = Button;
  holidays: Holiday[] = [];

  ngAfterViewInit() {
    flatpickr('.dtpickr', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private holidayService: HoliDayService) {

  }

  ngOnInit() {
    this.holidayForm = this.fb.group({
      holidayId: [0],
      holidayName: [''],
      startDate: [''],
      endDate: [''],
      numberofDays: [''],
    });

    const id = history.state.holidayId;
    if (id) {
      this.isEditMode = true;
      this.holidayService.getHoliDayById(id).subscribe(res => {

        const holiday = res ?? res;

        if (holiday) {
          this.holidayForm.patchValue({
            holidayId: holiday.holidayId,
            holidayName: holiday.holidayName,
            startDate: formatDate(holiday.startDate, 'dd-MMM-yyyy', 'en-US'),
            endDate: formatDate(holiday.endDate, 'dd-MMM-yyyy', 'en-US'),
          });
        }
      });
    }
    this.holidayForm.get('startDate')?.valueChanges.subscribe(() => this.calculateDays());
    this.holidayForm.get('endDate')?.valueChanges.subscribe(() => this.calculateDays());

    this.calculateDays();

  }

  parseDate(dateStr: string): Date {
    return new Date(dateStr); // Or use a date library like moment or dayjs if needed
  }

  calculateDays() {
    const start = new Date(this.holidayForm.get('startDate')?.value);
    const end = new Date(this.holidayForm.get('endDate')?.value);
    if (start && end && end >= start) {
      const diff = (end.getTime() - start.getTime()) / (1000 * 60 * 60 * 24) + 1;
      this.holidayForm.get('numberofDays')?.setValue(diff);
    } else {
      this.holidayForm.get('numberofDays')?.setValue('');
    }
  }

  formatDate(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toISOString(); // or return in 'yyyy-MM-dd' format if preferred
  }

  onSubmit() {

    if (this.holidayForm.valid) {
      this.isSubmitting = true;

      // Clone and format the form data
      const formValue = { ...this.holidayForm.value };

      if (this.isEditMode) {

        this.holidayService.UpdateData(formValue).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/hr/hrm/holiday-list']);
          },
          error: (error) => {
            if (error.type === 'warning')
              this.toastr.warning(error.message, 'Warning');
            else if (error.type === 'strongerror')
              this.toastr.error(error.message, 'Error');
            else
              this.toastr.error(error.message);
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        //debugger
        this.holidayService.addData(formValue).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.holidayForm.reset();
          },
          error: (error) => {
            //console.error('Error:', error);
            if (error.type === 'warning')
              this.toastr.warning(error.message, 'Warning');
            else if (error.type === 'strongerror')
              this.toastr.error(error.message, 'Error');
            else
              this.toastr.error(error.message);

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
    this.router.navigate(['hr/hrm/holiday-list']);
  }
  onReset(): void {
    this.holidayForm.reset();
  }
}


