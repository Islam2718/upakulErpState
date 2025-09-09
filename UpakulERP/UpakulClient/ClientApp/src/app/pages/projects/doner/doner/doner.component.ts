import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { DonerService } from '../../../../services/Projects/doner.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BtnService } from '../../../../services/btn-service/btn-service';


interface DropDown {
  text: string;
  value: string;
}


@Component({
  selector: 'app-doner',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './doner.component.html',
  styleUrl: './doner.component.css'
})


export class DonerComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  message = '';
  educationId = '';
  isEditMode = false;

  CountryDropdown: DropDown[] = [];


  constructor( 
   public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: DonerService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      DonerCode: ['', Validators.required],
      DonerName: ['', Validators.required],
      CountryId: ['', Validators.required],
      Location: ['', Validators.required],
      FirstContactPersonName: ['', Validators.required],
      FirstContactPersonContactNo: ['', Validators.required],
      SecendContactPersonName: ['', Validators.required],
      SecendContactPersonContactNo: ['', Validators.required]
    });
  }


  ngOnInit() {
    this.dataForm = this.fb.group({
      DonerId: [null],
      DonerName: [''],
      DonerCode: [''],
      CountryId: [null],
      Location: [''],
      FirstContactPersonName: [''],
      FirstContactPersonContactNo: [''],
      SecendContactPersonName: [''],
      SecendContactPersonContactNo: ['']

    });


    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getDonerById(id).subscribe(res => {
        const fetchedRow = res ?? res; // handle both wrapped and raw objects
        console.log('Fetched education object for patching:', fetchedRow);


        if (fetchedRow) {
          this.dataForm.patchValue({
            DonerId: fetchedRow.donerId,
            DonerCode: fetchedRow.donerCode,
            DonerName: fetchedRow.donerName,
            CountryId: fetchedRow.countryId,
            Location: fetchedRow.location,
            FirstContactPersonName: fetchedRow.firstContactPersonName,
            FirstContactPersonContactNo: fetchedRow.firstContactPersonContactNo,
            SecendContactPersonName: fetchedRow.secendContactPersonName,
            SecendContactPersonContactNo: fetchedRow.secendContactPersonContactNo
          });
        }
      });
    }


    this.loadDropDown();
  }


  loadDropDown() {
    this.apiService.getCountryDropdown().subscribe({
      next: (data) => {
        this.CountryDropdown = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownChange(event: any) {
    const selectedBank = event.target.value; // Get selected value correctly     
  }


 onSubmit() {

    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.apiService.updateData(this.dataForm.value).subscribe({
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

        this.apiService.addDoner(this.dataForm.value).subscribe({
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
    this.router.navigate(['pr/doner/doner-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }

}
