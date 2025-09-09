import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../shared/enums/button.enum';
import { EmployeeTypeService } from '../../../services/hrm/employeetype/employee-type.service';
import { ToastrService } from 'ngx-toastr';


interface EmployeeTypeValue {
  text: string;
  value: string;
}


@Component({
  selector: 'app-employee-type',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './employee-type.component.html',
  styleUrl: './employee-type.component.css'
})
export class EmployeeTypeComponent implements OnInit{
  buForm!: FormGroup;
  isSubmitting = false;
  successMessage = '';

  EmployeeTypeValues: EmployeeTypeValue[] = [];


  constructor(private fb: FormBuilder, private apiService: EmployeeTypeService,
    private toastr: ToastrService) {
    this.buForm = this.fb.group({
      EmployeeTypeValue: ['', Validators.required],  
      EmployeeTypeName: ['', Validators.required]     
    });
  }

  ngOnInit() {
    this.buForm = this.fb.group({
      EmployeeTypeValue: [''],
      EmployeeTypeName: ['']
    });
    this.loadDropDown();
  }

  selectedOption: string = '';
  selectedDisplayName: string = '';
  
  loadDropDown() {
    this.apiService.getEmployeeTypeDropdown().subscribe({
      next: (data) => {
        this.EmployeeTypeValues = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown types:', err);
      }
    });
  }

  onDropDownTypeChange(event: any) {
    const selectedEmployeeType = event.target.value; // Get selected value correctly   
    const selectedItem = this.EmployeeTypeValues.find(item => item.value === this.selectedOption);
    this.selectedDisplayName = selectedItem ? selectedItem.text : '';       
  }





  onSubmit() {
    if (this.buForm.valid) {
      this.isSubmitting = true;
      this.apiService.addEmployeeType(this.buForm.value).subscribe({
        next: (response) => {
          console.log('Response:', response);
          this.successMessage = 'Employee Type added successfully!';
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
