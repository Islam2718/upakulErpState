import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { DesignationService } from '../../../../services/hrm/designation/designation.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { Designation } from '../../../../models/hr/designation/designation'
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-designation-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './designation-form.component.html',
  styleUrl: './designation-form.component.css'
})

export class DesignationFormComponent implements OnInit {
  qry: string | null = null;
  designationForm: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  button = Button;
  designations: Designation[] = [];

  constructor(
    public Button: BtnService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private DesignationService: DesignationService) {
    this.designationForm = this.fb.group({
      DesignationId: ['', Validators.required],
      DesignationCode: ['', Validators.required],
      DesignationName: ['', Validators.required],
      OrderNo: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.designationForm = this.fb.group({
      DesignationId: [null],
      DesignationCode: [''],
      DesignationName: [''],
      OrderNo: [''],
    });
    const id = history.state.designationId;
    // alert(id)
    if (id) {
      this.isEditMode = true;
      this.DesignationService.getDesignationsById(id).subscribe(res => {
        const designation = res ?? res; // handle both wrapped and raw objects
       // console.log('Fetched designation object for patching:', designation);

        if (designation) {
          this.designationForm.patchValue({
            DesignationId: designation.designationId,
            DesignationCode: designation.designationCode,
            DesignationName: designation.designationName,
            OrderNo: designation.orderNo
          });
        }
      });
    }
  }

  onSubmit() {

    if (this.designationForm.value) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.DesignationService.UpdateDesignation(this.designationForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/hr/hrm/designation-list']);
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
        this.DesignationService.addDesignation(this.designationForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.designationForm.reset();
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
    this.router.navigate(['/hr/hrm/designation-list']);
  }
  onReset(): void {
    this.designationForm.reset();
  }
  filterToDigits(event: Event, controlName: string) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
    this.designationForm.get(controlName)?.setValue(input.value);
  }
}