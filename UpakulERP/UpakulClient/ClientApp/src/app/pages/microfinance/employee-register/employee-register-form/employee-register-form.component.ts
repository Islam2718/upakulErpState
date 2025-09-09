import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Button } from '../../../../shared/enums/button.enum';
import { Message } from '../../../../shared/enums/message.enum';
import { EmployeeRegister } from '../../../../models/microfinance/employee-register/employee-register';
import { EmployeeRegisterService } from '../../../../services/microfinance/employee-register/employee-register.service';
import { BtnService } from '../../../../services/btn-service/btn-service';


interface DropdownValues {
  text: string;
  value: string;
  selected: boolean;
}

interface EmployeeRegisterAllDropdown {
  availableGroup: DropdownValues[];
  releaseEmployee: DropdownValues[];
  assignEmployee: DropdownValues[];
}

@Component({
  selector: 'app-employee-register-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './employee-register-form.component.html',
  styleUrl: './employee-register-form.component.css'
})

export class EmployeeRegisterFormComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  successMessage = '';
  message = '';

  transactionDate: string | null = '';
  isPermitted: boolean = false;

  button = Button;

  dropdownEmployeeOptions: DropdownValues[] = [];
  dropdownAssignEmployeeOptions: DropdownValues[] = [];
  dropdownAvailableGroupOptions: DropdownValues[] = [];
  dropdownReleaseEmployeeGroupOptions: DropdownValues[] = [];
  dropdownAssignEmployeeGroupOptions: DropdownValues[] = [];
  filteredAssignOptions = [...this.dropdownEmployeeOptions];


  employeeRegisterAllDropdown: any = {
    assignEmployee: [],
    availableGroup: [],
    releaseEmployee: []
  };



  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: EmployeeRegisterService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {

    this.dataForm = this.fb.group({
      Id: ['', Validators.required],
      ReleaseEmpId: ['', Validators.required],
      AssignEmpId: ['', Validators.required],
      ReleaseNote: ['', Validators.required],
      // AssignedDate: ['', Validators.required],
      // ReleaseDate: ['', Validators.required],
      ReleaseEmployeeGroupList: [[]],
      AssignedGroupList: [[]],
      AvailableGroupList: [[]],
    });

  }

  ngOnInit() {
    const transDate = localStorage.getItem('transactionDate');
    if (transDate) {
      this.transactionDate = transDate;
    }
    const personalData = localStorage.getItem('personal');
    if (personalData) { const parsed = JSON.parse(personalData); this.isPermitted = parsed.office_type_id === 6; }

    this.dataForm = this.fb.group({
      Id: ['', Validators.required],
      ReleaseEmpId: [''],
      AssignEmpId: [''],

      AssignedDate: this.transactionDate,
      ReleaseDate: this.transactionDate,
      ReleaseNote: ['', Validators.required],
      ReleaseEmployeeGroupList: [[]],
      assignedGroupList: [[]],
      AvailableGroupList: [[]],
    });

    this.loadAllDropdown();


    this.dataForm.get('ReleaseEmployeeGroupList')?.valueChanges.subscribe(value => {
      const releaseNoteCtrl = this.dataForm.get('ReleaseNote');

      if (value && value.length > 0) {
        releaseNoteCtrl?.setValidators([Validators.required]);
      } else {
        releaseNoteCtrl?.clearValidators();
      }
      releaseNoteCtrl?.updateValueAndValidity();
    });

     this.isAssignButtonEnabled();
  }

  //Get AvailableGroup, ReleaseEmployee, AssignEmployee Dropdown
  async loadAllDropdown() {
    try {
      const response = await firstValueFrom(this.apiService.employeeRegisterAllDropdown());
      this.employeeRegisterAllDropdown = response || [];
      
      this.dropdownEmployeeOptions = this.employeeRegisterAllDropdown.releaseEmployee;
      this.dropdownAssignEmployeeOptions = this.employeeRegisterAllDropdown.assignEmployee;
      this.dropdownAvailableGroupOptions = this.employeeRegisterAllDropdown.availableGroup;
    } catch (error) {
      console.error('Failed to load dropdown data', error);
    }
  }

  loadEmployeeXGroupDropDown(id: number, empType: any) {
    // console.log("loadGroupByEmployeeIdDropDown", empType);
    this.apiService.getGroupByEmployeeIdForDropdownData(id).subscribe({
      next: (data) => {
        //console.log(data);
        if (empType == "release") {
          this.dropdownReleaseEmployeeGroupOptions = data;
        } else if (empType == "assign") {
          this.dropdownAssignEmployeeGroupOptions = data;
        }
      },
      error: (err) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  ReleaseEmployeeOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.loadEmployeeXGroupDropDown(dropdownValue, "release");
    this.dropdownAssignEmployeeGroupOptions = [];
    this.dataForm.get('AssignEmpId')?.reset();
  }

  AssignedEmployeeOnChange(event: any) {    
    const assignedValue = event.target.value;
    const releaseValue = this.dataForm.get('ReleaseEmpId')?.value;
    
    if (assignedValue && assignedValue === releaseValue) {
      this.toastr.warning("Assigned Employee cannot be the same as Release Employee.", 'warning');
      this.resetAssignFormData();    
    } else {
      this.loadEmployeeXGroupDropDown(assignedValue, "assign");
    }
    this.isAssignButtonEnabled();
  }

  addFromAvailableGroupDropdownToAssignList() {
    const selectedValue = this.dataForm.get('AvailableGroupList')?.value;

    if (!selectedValue) return;

    const isAlreadyAssigned = this.dropdownAssignEmployeeGroupOptions.some(x => x.value === selectedValue);
    if (isAlreadyAssigned) return;

    const selectedItem = this.dropdownAvailableGroupOptions.find(x => x.value === selectedValue);
    if (selectedItem) {
      this.dropdownAssignEmployeeGroupOptions.push(selectedItem);
      this.dropdownAvailableGroupOptions = this.dropdownAvailableGroupOptions.filter(x => x.value !== selectedValue);

      this.dataForm.get('AssignedGroupList')?.setValue(this.dropdownAssignEmployeeGroupOptions.map(x => x.value));

      this.dataForm.get('AvailableGroupList')?.setValue('');
    }
  }

  addToAssignList() {
    // console.log("addToAssignList");
    const selectedIds: any[] = this.dataForm.get('ReleaseEmployeeGroupList')?.value || [];

    if (selectedIds.length === 0) return;

    const selectedItems = this.dropdownReleaseEmployeeGroupOptions.filter(item =>
      selectedIds.includes(item.value)
    );

    this.dropdownAssignEmployeeGroupOptions.push(...selectedItems);

    this.dropdownReleaseEmployeeGroupOptions = this.dropdownReleaseEmployeeGroupOptions.filter(
      item => !selectedIds.includes(item.value)
    );

    this.dataForm.get('ReleaseEmployeeGroupList')?.setValue([]);
    this.dataForm.get('AssignedGroupList')?.setValue(
      this.dropdownAssignEmployeeGroupOptions.map(x => x.value)
    );
  }

  removeFromAssignList() {
    // console.log("removeFromAssignList");
    const selectedIds: any[] = this.dataForm.get('AssignedGroupList')?.value || [];

    if (!selectedIds || selectedIds.length === 0) {
      console.warn('No items selected to remove.');
      return;
    }

    // Extract selected items from assigned list
    const selectedItems = this.dropdownAssignEmployeeGroupOptions.filter(item =>
      selectedIds.includes(item.value)
    );

    // Add selected items back to release list
    this.dropdownReleaseEmployeeGroupOptions = [
      ...this.dropdownReleaseEmployeeGroupOptions,
      ...selectedItems
    ];

    // Remove selected items from assigned list
    this.dropdownAssignEmployeeGroupOptions = this.dropdownAssignEmployeeGroupOptions.filter(
      item => !selectedIds.includes(item.value)
    );

    // Clear selection and update assigned group control
    this.dataForm.get('AssignedGroupList')?.setValue(
      this.dropdownAssignEmployeeGroupOptions.map(item => item.value)
    );
  }

  isAssignButtonEnabled(): boolean {
    const assignEmpId = this.dataForm.get('AssignEmpId')?.value;
    const releaseGroupList = this.dataForm.get('ReleaseEmployeeGroupList')?.value;
    console.log("in isAssignButtonEnabled()");
    //return !!assignEmpId && releaseGroupList && releaseGroupList.length > 0;
    return !!assignEmpId && !!releaseGroupList && releaseGroupList.length > 0;
  }

  resetAssignFormData() {
    this.dataForm.patchValue({
      AssignEmpId: '',          // reset single select dropdown
      AssignedGroupList: []     // reset multi select dropdown
    });
    this.dropdownAssignEmployeeGroupOptions = [];
//      this.dataForm.get('AssignedGroupList')?.setValue(this.dropdownAssignEmployeeGroupOptions.map(x => x.value));

    // if you want to reset the entire form:
    // this.dataForm.reset();
  }

  onSubmit(): void {

    const assignedGroups = this.dropdownAssignEmployeeGroupOptions.map(item => item.value) || [];
    // console.log('Assigned Group IDs:', assignedGroups);

    const releaseGroups = this.dropdownReleaseEmployeeGroupOptions.map(item => item.value) || [];
    // console.log('Released Group IDs:', releaseGroups);

    // const formData = this.dataForm.getRawValue();
    // console.log("formData", formData);


    // if (this.dataForm.invalid) {
    //   this.dataForm.markAllAsTouched();
    //   return;
    // }

    const formValue = this.dataForm.value;
    // console.log("formValue", formValue);
    const dataObj = {
      Id: formValue.Id || 0,
      ReleaseEmployeeId: formValue.ReleaseEmpId || 0,
      ReleaseGroupListId: releaseGroups || [],
      ReleaseDate: formValue.ReleaseDate,
      ReleaseNote: formValue.ReleaseNote,
      AssignEmployeeId: formValue.AssignEmpId || 0,
      AssignedGroupListId: assignedGroups || [],
      JoiningDate: formValue.AssignedDate,
    };

    // console.log("dataObj", dataObj);

    this.apiService.addData(dataObj).subscribe({
      next: (response) => {
        this.toastr.success(response.message, 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.message || 'Something went wrong';

        if (error.type === 'warning') {
          this.toastr.warning(errorMessage, 'Warning'); //error.message
        } else if (error.type === 'strongerror') {
          this.toastr.error(errorMessage, 'Error');//error.message
        } else {
          this.toastr.error(errorMessage);
        }
        this.isSubmitting = false;
      },
      complete: () => {
        this.isSubmitting = false;
      }
    });
  }

  navigateToList() {
    this.router.navigate(['mf/employee-register/employee-register-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }

}
