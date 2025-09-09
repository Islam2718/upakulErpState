import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { AuthService } from '../../../../services/administration/auth/auth.service';
import { Button } from '../../../../shared/enums/button.enum';
import { ActivatedRoute, Router } from '@angular/router'; // ✅ Add this
import { ToastrService } from 'ngx-toastr';
import { BankService } from '../../../../services/Global/bank/bank.service';
import { BtnService } from '../../../../services/btn-service/btn-service';


interface BankTypeValue {
  text: string;
  value: string;
}


@Component({
  selector: 'app-bank',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './bank.component.html',
  styleUrl: './bank.component.css'
})
export class BankComponent {
  qry: string | null = null;
  bankForm: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  button = Button;
  BankTypeValues: BankTypeValue[] = [];

  constructor(
    public Button: BtnService,   
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private bankService: BankService) {
    this.bankForm = this.fb.group({
      BankType: ['', Validators.required],
      BankName: ['', Validators.required],
      BankShortCode: ['', Validators.required],
    });
     
  }



  ngOnInit() {
    this.bankForm = this.fb.group({
      BankId: [null],
      BankType: [''],
      BankName: [''],
      BankShortCode: [''],
    });
    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.bankService.getBankById(id).subscribe(res => {
        const bank = res ?? res; // handle both wrapped and raw objects
        //console.log('Fetched bank object for patching:', bank);

        if (bank) {
          this.bankForm.patchValue({
            BankId: bank.bankId,
            BankName: bank.bankName,
            BankType: bank.bankType,
            BankShortCode: bank.bankShortCode
          });
        }
      });

    }
    this.loadDropDown();
  }

  loadDropDown() {
    this.bankService.getBankTypeDropdown().subscribe({
      next: (data) => {
        this.BankTypeValues = data;
      },
      error: (err) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownTypeChange(event: any) {
    const selectedBankType = event.target.value; // Get selected value correctly     
  }


  onSubmit() {

    if (this.bankForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.bankService.updateBank(this.bankForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/gs/bank/bank-list']);
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

        this.bankService.addBank(this.bankForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.bankForm.reset();
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
    this.router.navigate(['gs/global/bank-list']);
  }
  onReset(): void {
    this.bankForm.reset();
  }
}
