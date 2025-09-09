import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { BudgetComponentService } from '../../../../services/accounts/budget/budget-component.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';


interface DropdownValues {
  text: string;
  value: string;
}

@Component({
  selector: 'app-budget',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './budget.component.html',
  styleUrl: './budget.component.css'
})
export class BudgetComponent implements OnInit{
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  button = Button;
  message = Message;
  Id ='';
  isEditMode = false;
  ComponentValues: DropdownValues[] = [];
  parentId: number = 0;

  
constructor(
    private fb: FormBuilder, 
    private apiService: BudgetComponentService,
    private toastr: ToastrService, 
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      ParentId: ['', Validators.required] ,   
      ComponentName: ['', Validators.required],    
      IsMedical: ['', Validators.required],    
      IsDesignation: ['', Validators.required]    
    });
  }

  ngOnInit() {
    // console.log("IM Herer");
    this.dataForm = this.fb.group({
      Id: [null],
      ComponentName: [''],
      IsDesignation: [false],  // default unchecked
      IsMedical: [false],
      ParentId: [null]
    });

    const id = history.state.editId;
    if (id) {
      //console.log("In Edit");
      this.isEditMode = true;
      this.apiService.getDataById(id).subscribe(res => {
        const dataObj = res ?? res; // handle both wrapped and raw objects
        //console.log(dataObj);
        //console.log('Fetched education object for patching:', dataObj);
        if (dataObj) {
          this.dataForm.patchValue({
            Id: dataObj.id,
            ParentId: dataObj.parentId,
            ComponentName: dataObj.componentName,
            IsMedical: dataObj.isMedical,
            IsDesignation: dataObj.isDesignation
          });
        }
      });
    }
    
     this.loadDropDown();
  }


  loadDropDown() {
    this.apiService.getDropDownComponent().subscribe({
      next: (data) => {
        this.ComponentValues = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownChange(event: any) {
    const selectedValue = event.target.value;
    this.parentId = selectedValue;
  }
  

  onSubmit() {
      if (this.dataForm.valid) {
        this.isSubmitting = true;
        // console.log('Before Edit', this.dataForm.value);
  
        if (this.isEditMode) {
          // console.log('In onSubmit EditMode');
          this.apiService.updateData(this.dataForm.value).subscribe(() => {
            this.router.navigate(['ac/account/budget-list'], { state: { pId: this.parentId } });
          });
        } else {
         // console.log('Before Add');                  
          this.apiService.addData(this.dataForm.value).subscribe({
            next: (response) => {
              //console.log('Response:', response);
              this.toastr.success('Data '+ this.message.SaveMsg, 'Success');            
              this.dataForm.reset();
            },
            error: (error) => {
              //console.error('Error:', error);
              this.toastr.error(this.message.SaveErr, 'Data Adding Failed'+ this.message.SaveMsg);
            },
            complete: () => {
              this.isSubmitting = false;
            }
          });
        }
      }      
    
    }


  
    navigateToList() {
      this.router.navigate(['ac/accounts/budget-list']);
    }

    onReset(): void {
      this.dataForm.reset();
    }

}
