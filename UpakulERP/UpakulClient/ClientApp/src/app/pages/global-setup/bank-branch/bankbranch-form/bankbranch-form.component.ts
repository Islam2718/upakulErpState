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
import { BankBranchService } from '../../../../services/Global/bankbranch/bank-branch.service';
import { BankBranch } from '../../../../models/Global/bankbranch/bankbranch';

interface Banks {
  text: string;
  value: string;
}

@Component({
  selector: 'app-bankbranch-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './bankbranch-form.component.html',
  styleUrl: './bankbranch-form.component.css'
})

export class BankbranchFormComponent {
  bankbranchForm: FormGroup;
  isSubmitting = false;
  //successMessage = '';
  message = '';
  isEditMode = false;
  button = Button;
  bankss: Banks[] = [];
  //principalTypes: PrincipalType[] = [];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    public router: Router,
    private BankBranchService: BankBranchService) {
    this.bankbranchForm = this.fb.group({
      BankId: ['', Validators.required],
      BranchName: ['', Validators.required],
      BranchAddress: ['', Validators.required],
      RoutingNo: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.bankbranchForm = this.fb.group({
      BankBranchId: [null],
      BankId: [null],
      Bank: [''],
      BranchName: [''],
      BranchAddress: [''],
      RoutingNo: [''],
    });
    const id = history.state.editId;
    // alert(id)
    if (id) {
      this.isEditMode = true;
      this.BankBranchService.getBankBranchsById(id).subscribe(res => {
        const bankbranch = res ?? res; // handle both wrapped and raw objects
        console.log('Fetched bankbranch object for patching:', bankbranch);

        if (bankbranch) {
          this.bankbranchForm.patchValue({
            BankBranchId: bankbranch.bankBranchId,
            BankId: bankbranch.bankId,
            BranchName: bankbranch.branchName,
            BranchAddress: bankbranch.branchAddress,
            RoutingNo: bankbranch.routingNo
          });
        }
      });
    }
    this.loadDropDown();
  }
  loadDropDown() {
    //alert('hj')
    this.BankBranchService.getBankDropdown().subscribe({
      next: (data) => {

        this.bankss = data;
        // console.log('banks',data)
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

    if (this.bankbranchForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.BankBranchService.updateBankBranch(this.bankbranchForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/gs/global/bankbranch-list']);
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
        this.BankBranchService.addBankBranch(this.bankbranchForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.bankbranchForm.reset();
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
    this.router.navigate(['gs/global/bankbranch-list']);
  }
  onReset(): void {
    this.bankbranchForm.reset();
  }
}