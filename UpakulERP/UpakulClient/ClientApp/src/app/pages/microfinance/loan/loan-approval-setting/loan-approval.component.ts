import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormsModule, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../shared/enums/button.enum';
//import { Message } from '../../../../shared/enums/message.enum';
import { NgFor, CommonModule } from '@angular/common';
import { LoanApprovalService } from '../../../../services/microfinance/loan/loan-approval/loan-approval.service'; // Adjust path if needed
import { LoanApprovalModel } from '../../../../models/microfinance/loan-approval/loan-approval-model';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface DropdownValues {
  text: string;
  value: string;
}

@Component({
  selector: 'app-loan-approval',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './loan-approval.component.html',
  styleUrl: './loan-approval.component.css'
})

export class LoanApprovalComponent  implements OnInit{
  dataForm!: FormGroup;
  isSubmitting = false;
  loanApprovalId = '';
  isEditMode = false;
  successMessage = '';
  message = '';
  button = Button;
  buttonDisable: boolean=true;
  approvalTypes:DropdownValues[] = [{text:'Checked',value:'C'},{text:'Approval',value:'A'} ,{text:'Disbursment',value:'D'} ];
  methods = ['Greater Than', 'Less Than'];
  designations: DropdownValues[] = [];

  constructor(
    private fb: FormBuilder,
    private apiService: LoanApprovalService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute,   // ✅ This one, not Router,
    private http: HttpClient  
  ) {

    this.dataForm = this.fb.group({
      approvals: this.fb.array([])
    });

    // Add one default row
    this.addRow();
  }

  get approvals(): FormArray {
    return this.dataForm.get('approvals') as FormArray;
  }

  addRow() {
      const newGroup = this.fb.group({
        level: [0], // Will be set below
        approvalType: ['', Validators.required],
        designationId: ['', Validators.required],
        startingValueAmount: [0, [Validators.required, Validators.pattern('^[0-9]*$')]]
      });

      this.approvals.push(newGroup);
      this.updateLavelNumbers(); // Recalculate lavel numbers
  }

  deleteRow(deleteId: number) {
    //console.log("In DeleteRow", deleteId);
     if (confirm('Are you sure you want to delete this data?')) {
      this.isSubmitting = true;
       this.apiService.deleteData(deleteId).subscribe({
        next: (response) => {
          if (response.type === 'warning') {
            this.toastr.warning(response.message, 'Warning');
          } else if (response.type === 'strongerror') {
            this.toastr.error(response.message, 'Error');
          } else {
            this.loadList(); 
            this.updateLavelNumbers();
            this.toastr.success(response.message, 'Success');
          }
          // Reload the list or redirect based on your use case
          
        },
        error: (error) => {
          if (error.type === 'warning') {
            this.toastr.warning(error.message, 'Warning');
          } else if (error.type === 'strongerror') {
            this.toastr.error(error.message, 'Error');
          } else {
            this.toastr.error('Delete failed');
          }
          this.isSubmitting = false;
        },
        complete: () => {
          this.isSubmitting = false;
        }
      });
     }

  }


  updateLavelNumbers() {
  this.approvals.controls.forEach((group, index) => {
    group.get('level')?.setValue(index + 1);
  });
}
  
  ngOnInit(): void {
    this.dataForm = this.fb.group({
      approvals: this.fb.array([])
    });

    this.loadList() ;
    this.loadDesignationDropDown();
    this.buttonDisable = false;
  }

  loadList() {
      this.apiService.getAll().subscribe(data => {
      const approvalFGs = data.map(item => this.fb.group({
        level: [item.level],
        //loanApprovalId: [item.loanApprovalId],
        approvalType: [item.approvalType],
        designationId: [item.designationId],
        //method: [item.method],
        startingValueAmount: [item.startingValueAmount]
      }));
      const formArray = this.fb.array(approvalFGs);
      this.dataForm.setControl('approvals', formArray);
    });
  }

loadDesignationDropDown() {
    this.apiService.getDesignationDropDownData().subscribe({
      next: (data) => {
        this.designations = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

   submit() {
    if (this.dataForm.valid) {
      const dataArrayObj = this.dataForm.value.approvals;
      this.apiService.CreateData(dataArrayObj).subscribe({
          next: (response) => {
            //console.log(response.toString());
            this.toastr.success("Saved Successfully");
            this.loadList() ;

          },
          error: (error) => {
            
            if (error.status<500) {
              this.toastr.warning(error.error.message, 'Warning');
            } /*else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            }*/ else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });   

    } else {
      this.dataForm.markAllAsTouched();
    }
  }


}
