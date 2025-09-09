import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../shared/enums/button.enum';
import { Message } from '../../../../shared/enums/message.enum';
import { NgFor, CommonModule } from '@angular/common';
import { OccupationService } from '../../../../services/microfinance/occupation/occupation.service'; // Adjust path if needed
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { Occupation } from '../../../../models/microfinance/occupation/occupation';
import { BtnService } from '../../../../services/btn-service/btn-service';


@Component({
  selector: 'app-occupation',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './occupation.component.html',
  styleUrl: './occupation.component.css'
})
export class OccupationComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  occupationId = '';
  isEditMode = false;
  successMessage = '';
  message = '';
  button = Button;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
      private fb: FormBuilder,
      private apiService: OccupationService,
      private toastr: ToastrService,
      public router: Router,
      private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
    ) {
      this.dataForm = this.fb.group({
        OccupationId: ['', Validators.required],
        OccupationName: ['', Validators.required],
      });
  
  
    }


  ngOnInit() {
    this.dataForm = this.fb.group({
      OccupationId: [null],
      OccupationName: ['']
    });


    const id = history.state.editId;
    if (id) {
     // console.log("EditID", id);
      this.isEditMode = true;
      this.apiService.getDataById(id).subscribe(res => {
        const data = res ?? res;  // handle both wrapped and raw objects
       
        if (data) {
          this.dataForm.patchValue({
            OccupationId: data.occupationId,
            OccupationName: data.occupationName,
          });
          
        }
      });

    }
  }




onSubmit() 
{

    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.apiService.UpdateData(this.dataForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/microfinance/occupation/occupation-list']);
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
        this.apiService.addData(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.dataForm.reset();
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
    this.router.navigate(['mf/occupation/occupation-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }

}
