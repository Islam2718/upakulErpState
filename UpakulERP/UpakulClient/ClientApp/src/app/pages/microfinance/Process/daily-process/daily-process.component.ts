import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { BudgetComponentService } from '../../../../services/accounts/budget/budget-component.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { DailyProcess } from '../../../../models/microfinance/process/daily-process'
import { DailyProcessService } from '../../../../services/microfinance/process/daily-process.service';
import flatpickr from 'flatpickr';



@Component({
  selector: 'app-daily-process',
  imports: [ 
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './daily-process.component.html',
  styleUrl: './daily-process.component.css'
})


export class DailyProcessComponent implements OnInit{
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  message = '';
  officeName: string | null = '';

ngAfterViewInit() {
    flatpickr('#datepickerValCalender', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

constructor(
    private fb: FormBuilder, 
    private apiService: DailyProcessService,
    private toastr: ToastrService, 
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      OfficeId: ['', Validators.required],
      TransactionDate: ['', Validators.required]    
    });
  }

ngOnInit() {
    this.dataForm = this.fb.group({
      OfficeId: [null],
      TransactionDate: ['']
    });

    const userData = localStorage.getItem('personal');
    console.log(userData);
    if (userData) {
      const parsedData = JSON.parse(userData);
      this.officeName = parsedData.office_name;
    }
}


  onSubmit() {
  
    if (this.dataForm.value) {
      this.isSubmitting = true;

      // if (this.isEditMode) {
      //   this.DesignationService.UpdateDesignation(this.designationForm.value).subscribe(() => {
      //     this.router.navigate(['/hr/hrm/designation-list']);
      //   });
      // } else {
        
        this.apiService.addData(this.dataForm.value).subscribe({
          next: (response) => {
            //  console.log('dept Response:', response);
            //this.toastr.success('Data '+ this.message.SaveMsg, 'Success');  
            this.message = response.message;
            //console.log('Response:', response);
            this.toastr.success(this.message, 'Success');
            this.dataForm.reset();
          },
          error: (error) => {
            console.log(error);
            this.message = error;
            this.toastr.error(this.message + " ");
            this.dataForm.reset();
          },
          complete: () => {
            this.isSubmitting = true;
          }
        });

        
      }    
  }



  //navigateToList() {
  //  this.router.navigate(['/hr/hrm/designation-list']);
  //}


  onReset(): void {
    this.dataForm.reset();
  }


}
