import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { CountryService } from '../../../../services/Global/country/country.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-country',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './country.component.html',
  styleUrl: './country.component.css'
})
export class CountryComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  educationId = '';
  isEditMode = false;
  button = Button;

  constructor(
     public Button: BtnService,
        // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: CountryService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      CountryName: ['', Validators.required],
      CountryCode: ['', Validators.required]
    });
    // Button.editBtn='';
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      CountryId: [null],
      CountryName: [''],
      CountryCode: ['']
    });


    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getCountryById(id).subscribe(res => {
        const country = res ?? res; // handle both wrapped and raw objects
        //console.log('Fetched education object for patching:', country);
        if (country) {
          this.dataForm.patchValue({
            CountryId: country.countryId,
            CountryName: country.countryName,
            CountryCode: country.countryCode
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
        this.apiService.updateCountry(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['gs/country/country-list']);
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

        this.apiService.addCountry(this.dataForm.value).subscribe({
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
    this.router.navigate(['gs/country/country-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }
}
