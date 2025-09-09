import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormsModule, FormGroup, Validators, ValidationErrors, ValidatorFn, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { OfficeComponentMappingService } from '../../../services/microfinance/office-component-mapping/office-component-mapping.service'; 
import { ComponentMFService } from '../../../services/microfinance/components/componentMF/componentMF.service';


interface DropdownValues {
  text: string;
  value: string;
  selected: boolean;
}

@Component({
  selector: 'app-office-component-mapping',
  imports: [
      CommonModule,
      ReactiveFormsModule,
      MatFormFieldModule,
      MatInputModule,
      MatButtonModule,
      FormsModule
    ],
  templateUrl: './office-component-mapping.component.html',
  styleUrl: './office-component-mapping.component.css'
})

export class OfficeComponentMappingComponent implements OnInit{
  dataForm: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  successMessage = '';
  message = '';

  dropdownBranchOfficeValues: DropdownValues[] = [];
  dropdownComponentValues: DropdownValues[] = [];
  rightItems: any[] = [];
  leftItems: any[] = [];
  
  constructor(
      private fb: FormBuilder,
      private apiService: OfficeComponentMappingService,
      private apiComponentService:ComponentMFService,
      private toastr: ToastrService,
      public router: Router,
      private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
    ) {
      this.dataForm = this.fb.group({        
        ComponentId: [null, Validators.required]                
      });
  }

    ngOnInit() {
      this.dataForm = this.fb.group({
       ComponentId: [null, Validators.required],
       BranchList: [[]],
       SelectedBranch: [[]]
      }); 
      
      this.loadComponentDropDown();
      this.loadBranchDropDown();
    }

    loadBranchDropDown() {
    console.log("Called loadBranchDropDown");
    this.apiService.getBranchDropDown().subscribe({
      next: (data) => {
        this.dropdownBranchOfficeValues = data;
        console.log("branchList", this.dropdownBranchOfficeValues);
      },
      error: (err) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }
  
  loadComponentDropDown() {
    this.apiComponentService.getProjectLoanComponentDropdown().subscribe({
      next: (data) => {
        this.dropdownComponentValues = data as DropdownValues[];
        const defaultOption = this.dropdownComponentValues.find(opt => opt.selected);
        if (defaultOption) {
          this.dataForm.get('ComponentId')?.setValue(defaultOption.value);
        }
      },
      error: (err) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  ComponentOnChange(event: any) { 
    
    this.rightItems = [];
    this.leftItems =[];

    const dropdownValue = event.target.value;
    this.apiService.getByComponentId(dropdownValue).subscribe(response => {
          if (response.statusCode === 200 && response.data?.length) {
            const componentId = response.data[0].componentId;
            const selectedBranch = response.data.map((item: any) => item.officeId.toString());

             this.rightItems= this.dropdownBranchOfficeValues.filter(item => selectedBranch.includes(item.value));
            
             this.leftItems = this.dropdownBranchOfficeValues.filter(item => !selectedBranch.includes(item.value));
          
          }else{
            this.leftItems = this.dropdownBranchOfficeValues;
          }
        });
  }


  addBranch(): void {
  const selectedIds: string[] = this.dataForm.value.BranchList || [];

  const selected = this.leftItems.filter(x => selectedIds.includes(x.value));
  selected.forEach(item => {
    if (!this.rightItems.find(x => x.value === item.value)) {
      this.rightItems.push(item);
    }
  });

  this.leftItems = this.leftItems.filter(x => !selectedIds.includes(x.value));
  
  this.dataForm.get('BranchList')?.setValue([]);
  this.dataForm.get('SelectedBranch')?.setValue(this.rightItems.map(x => x.value));
}


  removeBranch(): void {
  const selectedIds: string[] = this.dataForm.value.SelectedBranch || [];

  const selected = this.rightItems.filter(x => selectedIds.includes(x.value));

  selected.forEach(item => {
    if (!this.leftItems.find(x => x.value === item.value)) {
      this.leftItems.push(item);
    }
  });

  this.rightItems = this.rightItems.filter(x => !selectedIds.includes(x.value));

  this.dataForm.get('SelectedBranch')?.setValue(this.rightItems.map(x => x.value));
}

  resetComponentDropdown(){
    const defaultOption = this.dropdownComponentValues.find(opt => opt.selected);
        if (defaultOption) {
          this.dataForm.get('ComponentId')?.setValue(defaultOption.value);
        }
  }

  onSubmit() {
   const selectedValues = this.dataForm.get('SelectedBranch')?.value || [];
   this.dataForm.get('SelectedBranch')?.setValue(selectedValues);
    const formData = this.dataForm.getRawValue();
    
    if (this.dataForm.valid) {
            this.isSubmitting = true;          
              console.log("UpdateformData", formData);

              this.apiService.addData(formData).subscribe({
                next: (response) => {
                  this.toastr.success(response.message, 'Success');
                  
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

  onReset(): void {
    this.dataForm.reset();
  }


}