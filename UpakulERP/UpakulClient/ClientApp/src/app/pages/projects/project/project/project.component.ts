import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { ProjectService } from '../../../../services/Projects/project.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';



@Component({
  selector: 'app-project',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  message = '';
  projectId = '';
  isEditMode = false;
  button = Button;
ngAfterViewInit() {
    flatpickr('#datepickerValCalender', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }
  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: ProjectService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      ProjectTitle: ['', Validators.required],
      ProjectType: ['', Validators.required],
      Objective: [''],
      ChipEmployeeId: ['0', Validators.required],
      TotalStaff: ['0', Validators.required],
      MonitoringPeriod:['0', Validators.required],
      Target: [''],
      TotalTarget: [''],
      StartMonth: ['', Validators.required],
      ExpireDate: ['', Validators.required],
      ProjectShortName: ['', Validators.required],
      TargetType: ['', Validators.required],
      MonthlyQuarterly: ['', Validators.required],
      FinancialTarget: ['', Validators.required]
    });
  }

filterToDigits(event: Event, controlName: string) {
  const input = event.target as HTMLInputElement;
  input.value = input.value.replace(/[^0-9]/g, '');
  this.dataForm.get(controlName)?.setValue(input.value);
}

  ngOnInit() {
    this.dataForm = this.fb.group({
      ProjectId: [null],
      ProjectTitle: [''],
      ProjectType: [''],
      Objective: [''],
      ChipEmployeeId: [''],
      TotalStaff: [''],
      MonitoringPeriod: [''],
      Target: [''],
      TotalTarget: [''],
      StartMonth: [''],
      ExpireDate: [''],
      ProjectShortName: [''],
      TargetType: [''],
      MonthlyQuarterly: [''],
      FinancialTarget: ['']
    });


    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getProjectById(id).subscribe(res => {
        const fetchedRow = res ?? res; // handle both wrapped and raw objects
        // console.log('Fetched object for patching:', fetchedRow);

        if (fetchedRow) {
          this.dataForm.patchValue({
            ProjectId: fetchedRow.projectId,
            ProjectTitle: fetchedRow.projectTitle,
            ProjectType: fetchedRow.projectType,
            Objective: fetchedRow.objective,
            ChipEmployeeId: fetchedRow.chipEmployeeId,
            TotalStaff: fetchedRow.totalStaff,
            MonitoringPeriod: fetchedRow.monitoringPeriod,
            Target: fetchedRow.target,
            TotalTarget: fetchedRow.totalTarget,
            StartMonth: fetchedRow.startMonth,
            ExpireDate: fetchedRow.expireDate,
            ProjectShortName: fetchedRow.projectShortName,
            TargetType: fetchedRow.targetType,
            MonthlyQuarterly: fetchedRow.monthlyQuarterly,
            FinancialTarget: fetchedRow.financialTarget
          });
        }
      });
    }
  }



  // onSubmit() {
  //   if (this.dataForm.valid) {
  //     this.isSubmitting = true;
  //     //console.log('Before Edit', this.dataForm.value);

  //     if (this.isEditMode) {
  //       // console.log('In onSubmit EditMode');
  //       this.apiService.updateProject(this.dataForm.value).subscribe(() => {
  //         this.router.navigate(['pr/project/project-list']);
  //       });
  //     } else {
  //       //console.log('Before Add');

  //       this.apiService.addProject(this.dataForm.value).subscribe({
  //         next: (response) => {
  //           this.message = response.message;
  //           this.toastr.success(this.message, 'Success');
  //           this.dataForm.reset();
  //         },
  //         error: (error) => {
  //           this.message = error.message;
  //           this.toastr.error(this.message);
  //         },
  //         complete: () => {
  //           this.isSubmitting = false;
  //         }
  //       });
  //     }
  //   }


  // }
onSubmit() {

    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.apiService.update(this.dataForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['']);
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

        this.apiService.addProject(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.dataForm.reset();
          },
          error: (error) => {
            //console.error('Error:', error);
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            //console.log(error)
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
    this.router.navigate(['pr/project/project-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }


}


