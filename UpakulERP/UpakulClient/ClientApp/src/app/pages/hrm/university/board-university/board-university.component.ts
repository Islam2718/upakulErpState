import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { BoardUniversityService } from '../../../../services/hrm/boarduniversity/board-university.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BtnService } from '../../../../services/btn-service/btn-service';



@Component({
  selector: 'app-board-university',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './board-university.component.html',
  styleUrl: './board-university.component.css'
})
export class BoardUniversityComponent implements OnInit {
  qry: string | null = null;
  //buForm!: FormGroup;
  dataForm: FormGroup;
  isSubmitting = false;
  button = Button;
  //  BUId ='';
  isEditMode = false;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: BoardUniversityService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,
  ) {
    this.dataForm = this.fb.group({
      BUName: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      BUId: [''],
      BUName: ['']
    });

    const id = history.state.editId;
    //console.log('In formv '+id);
    if (id) {
      //console.log(id);
      this.isEditMode = true;
      this.apiService.getBoardUniversityById(id).subscribe(res => {
        const singleData = res ?? res; // handle both wrapped and raw objects
        //console.log('Fetched data object for patching:', singleData);


        if (singleData) {
          this.dataForm.patchValue({
            BUId: singleData.buId,
            BUName: singleData.buName
          });
        }
      });
    }

  }

  onSubmit() {

    if (this.dataForm.valid) {
      this.isSubmitting = true;
      //console.log('Before Edit', this.dataForm.value);
      //const formValue = { ...this.dataForm.value };

      if (this.isEditMode) {
        //console.log('In onSubmit EditMode');
        this.apiService.updateData(this.dataForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['hr/hrm/board-universitylist']);
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

        this.apiService.addBoartUniversity(this.dataForm.value).subscribe({
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
    this.router.navigate(['hr/hrm/board-universitylist']);
  }
  onReset(): void {
    this.dataForm.reset();
  }
}
