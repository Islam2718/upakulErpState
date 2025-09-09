import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../../shared/enums/button.enum';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { MasterComponentService } from '../../../../../services/microfinance/components/masterComponent/masterComponent.service';
import { BtnService } from '../../../../../services/btn-service/btn-service';
@Component({
  selector: 'app-master-component-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './master-component-form.component.html',
  styleUrl: './master-component-form.component.css'
})
export class MasterComponentFormComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  //message = Message;
  message = '';
  Id = '';
  isEditMode = false;
  button = Button;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: MasterComponentService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      Name: ['', Validators.required],
      Code: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      Id: [null],
      Name: [''],
      Code: ['']
    });


    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getMasterComponentById(id).subscribe(res => {
        const masterComponent = res ?? res; // handle both wrapped and raw objects
        console.log('Fetched mastercomponent object for patching:', masterComponent);
        if (masterComponent) {
          this.dataForm.patchValue({
            Id:masterComponent.id,
            Name: masterComponent.name,
            Code: masterComponent.code
          });
        }
      });
    }
  }




  onSubmit() {

    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.apiService.updateMasterComponent(this.dataForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['mf/masterComponent/master-component-list']);
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

        this.apiService.addMasterComponent(this.dataForm.value).subscribe({
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
    this.router.navigate(['mf/masterComponent/master-component-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }
}
