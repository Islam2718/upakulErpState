import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { DepartmentService } from '../../../../services/hrm/department/department.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { Department } from '../../../../models/hr/department/department';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-dept-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './dept-form.component.html',
  styleUrl: './dept-form.component.css'
})

export class DepartmentFormComponent implements OnInit {
  qry: string | null = null;
  departmentForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  button = Button;

  departments: Department[] = [];
  constructor(
     public Button: BtnService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private DepartmentService: DepartmentService) {
    this.departmentForm = this.fb.group({
      DepartmentId: ['', Validators.required],
      DepartmentCode: ['', Validators.required],
      DepartmentName: ['', Validators.required],
      OrderNo: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.departmentForm = this.fb.group({
      DepartmentId: [null],
      DepartmentCode: [''],
      DepartmentName: [''],
      OrderNo: [''],
    });
    const id = history.state.departmentId;
    // alert(id)
    if (id) {
      this.isEditMode = true;
      this.DepartmentService.getDepartmentById(id).subscribe(res => {
        const department = res ?? res; // handle both wrapped and raw objects
        //console.log('Fetched department object for patching:', department);

        if (department) {
          this.departmentForm.patchValue({
            DepartmentId: department.departmentId,
            DepartmentCode: department.departmentCode,
            DepartmentName: department.departmentName,
            OrderNo: department.orderNo
          });
        }
      });
    }
  }


  onSubmit() {
    //debugger
    if (this.departmentForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.DepartmentService.UpdateDepartment(this.departmentForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/hr/hrm/dept-list']);
          },
          error: (error) => {
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });

      } else {

        this.DepartmentService.addDepartment(this.departmentForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.departmentForm.reset();
            this.isSubmitting = true;
          },
          error: (error) => {
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
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
    this.router.navigate(['/hr/hrm/dept-list']);
  }
  onReset(): void {
    this.departmentForm.reset();
  }
  filterToDigits(event: Event, controlName: string) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
    this.departmentForm.get(controlName)?.setValue(input.value);
  }

}


