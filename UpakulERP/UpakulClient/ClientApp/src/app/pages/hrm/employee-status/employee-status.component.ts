import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../shared/enums/button.enum';
import { EmployeeStatusService } from '../../../services/hrm/employeestatus/employee-status.service';
import { ToastrService } from 'ngx-toastr';


interface EmployeeStatusValue {
  text: string;
  value: string;
}


@Component({
  selector: 'app-employee-status',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './employee-status.component.html',
  styleUrl: './employee-status.component.css'
})




export class EmployeeStatusComponent implements OnInit {
  buForm!: FormGroup;
  isSubmitting = false;
  successMessage = '';

  EmployeeStatusValues: EmployeeStatusValue[] = [];

  constructor(private fb: FormBuilder, private apiService: EmployeeStatusService,
      private toastr: ToastrService) {
      this.buForm = this.fb.group({
        EmployeeStatusValue: ['', Validators.required],  
        EmployeeStatusName: ['', Validators.required]     
      });
    }
  
    ngOnInit() {
      this.buForm = this.fb.group({
        EmployeeStatusValue: [''],
        EmployeeStatusName: ['']
      });
      this.loadDropDown();
    }

    selectedOption: string = '';
    selectedDisplayName: string = '';

    loadDropDown() {
      this.apiService.getEmployeeStatusDropdown().subscribe({
        next: (data) => {
          this.EmployeeStatusValues = data;
        },
        error: (err) => {
          console.error('Error fetching dropdown status:', err);
        }
      });
    }
  
    onDropDownStatusChange(event: any) {
      const selectedEmployeeStatus = event.target.value; // Get selected value correctly  
      const selectedItem = this.EmployeeStatusValues.find(item => item.value === this.selectedOption);
      this.selectedDisplayName = selectedItem ? selectedItem.text : '';     
    }




    onSubmit() {
      if (this.buForm.valid) {
        this.isSubmitting = true;
        this.apiService.addEmployeeStatus(this.buForm.value).subscribe({
          next: (response) => {
            console.log('Response:', response);
            this.successMessage = 'Employee Status added successfully!';
            this.buForm.reset();
          },
          error: (error) => {
            console.error('Error:', error);
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }    
    }
}
