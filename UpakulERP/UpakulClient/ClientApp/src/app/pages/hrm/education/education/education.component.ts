import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { EducationService } from '../../../../services/hrm/education/education.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BtnService } from '../../../../services/btn-service/btn-service';


@Component({
  selector: 'app-education',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './education.component.html',
  styleUrl: './education.component.css'
})
export class EducationComponent implements OnInit {
  qry: string | null = null;
  //educationForm!: FormGroup;
  dataForm: FormGroup;
  isSubmitting = false;
  button = Button;
  educationId = '';
  isEditMode = false;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: EducationService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      EducationName: ['', Validators.required],
      EducationDescription: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      EducationId: [null],
      EducationName: [''],
      EducationDescription: ['']
    });

    const id = history.state.educationId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getEducationById(id).subscribe(res => {
        const education = res ?? res; // handle both wrapped and raw objects
        //  console.log('Fetched education object for patching:', education);


        if (education) {
          this.dataForm.patchValue({
            EducationId: education.educationId,
            EducationName: education.educationName,
            EducationDescription: education.educationDescription
          });
        }
      });
    }

  }

  onSubmit() {
    if (this.dataForm.valid) {
      this.isSubmitting = true;
      // console.log('Before Edit', this.dataForm.value);

      if (this.isEditMode) {
        //  console.log('In onSubmit EditMode');
        this.apiService.UpdateEducation(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['hr/education/education-list']);
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
          complete: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        // console.log('Before Add');

        this.apiService.addEducation(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.dataForm.reset();
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
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }

    }


  }


  navigateToList() {
    this.router.navigate(['hr/hrm/education-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }
}
