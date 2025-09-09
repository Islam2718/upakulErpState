import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { EmployeeRegistrationService } from '../../../../services/administrator/employee-registration.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';

interface DropdownValues {
  text: string;
  value: string;
}

interface Role {
  text: string;
  value: string | null;
  selected: boolean;
}

interface Module {
  moduleId: number;
  moduleName: string;
  isSelected: boolean;
  roles: Role[];
  selectedRole?: string | null; // for ngModel binding
}

interface ApiResponse {
  statusCode: number;
  message: string;
  data: {
    employeeId: number;
    firstName: string;
    lastName: string;
    userId: number;
    userName: string | null;
    userXModule: Module[];
  };
}

@Component({
  selector: 'app-employee-registration',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './employee-registration.component.html',
  styleUrl: './employee-registration.component.css'
})
export class EmployeeRegistrationComponent implements OnInit {

  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  message = Message;
  Id = '';
  isEditMode = false;
  ModuleValues: DropdownValues[] = [];
  EmployeeDropdownValues: DropdownValues[] = [];
  userModules: Module[] = [];
  selectedEmployeeCode: string = '';
  selectedEmployeeName: string = '';


  editUserId: number = 0;
  editEmployeeId: number = 0;
  ModId: number = 0;
  EmployeeId: number = 0;

  constructor(private fb: FormBuilder,
    private apiService: EmployeeRegistrationService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      UserId: [null],
      EmployeeId: [null, Validators.required],
      RegUserName: ['', Validators.required],
      Password: ['', Validators.required],
      ConfirmPassword: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.dataForm = this.fb.group({
      UserId: [null],
      EmployeeCode: [''],
      EmployeeName: [''],
      EmployeeId: [null, Validators.required],
      RegUserName: ['', Validators.required],
      Password: ['', Validators.required],
      ConfirmPassword: ['', Validators.required],
      modules: this.fb.array([])
    }, { validators: this.passwordsMatchValidator }); // 👈 Apply validator here



    /*Edit*/
    const navState = history.state;
    //console.log("navState", navState);
    if (navState.editUserId && navState.editEmployeeId) {
      this.editUserId = navState.editUserId;
      this.editEmployeeId = navState.editEmployeeId;
      this.isEditMode = true;

      //console.log("navState.editEmployeeId", navState.editEmployeeId);
      this.loadDropDownEmployee(navState.editEmployeeId);
      this.dataForm.patchValue({ EmployeeId: this.editEmployeeId });
      this.dataForm.get('EmployeeCode')?.disable();
      this.dataForm.get('EmployeeName')?.disable();
      this.dataForm.get('RegUserName')?.disable();
      this.dataForm.get('EmployeeId')?.disable();
      this.dataForm.get('Password')?.disable();
      this.dataForm.get('ConfirmPassword')?.disable();
    } else {
      this.loadDropDownEmployee(0);
      this.dataForm.get('RegUserName')?.enable();
      this.dataForm.get('EmployeeId')?.enable();
      this.dataForm.get('Password')?.enable();
      this.dataForm.get('ConfirmPassword')?.enable();
    }
  }

  loadDropDownModule() {
    this.apiService.getModuleDropdown().subscribe({
      next: (data) => {
        this.ModuleValues = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadDropDownEmployee(edit_empId?: number) {
    this.apiService.getEmployeeDropdown(edit_empId).subscribe({
      next: (data) => {
        this.EmployeeDropdownValues = data;
        this.setEmployeeInfo(edit_empId ?? 0);
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownModuleChange(event: any) {
    const selectedValue = event.target.value;
    this.ModId = selectedValue;
  }

  onDropDownEmployeeChange(event: any) {
    if (event.target.value) {
      this.setEmployeeInfo(event.target.value);
    }
    else {
      this.dataForm.patchValue({ RegUserName: "" });
      this.dataForm.patchValue({ EmployeeCode: "" });
      this.dataForm.patchValue({ EmployeeName: "" });
      this.selectedEmployeeCode = "";
      this.selectedEmployeeName = "";
      this.userModules = [];
    }
  }

  passwordsMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('Password')?.value;
    const confirmPassword = formGroup.get('ConfirmPassword')?.value;
    return password === confirmPassword ? null : { passwordsMismatch: true };
  }

  setEmployeeInfo(empId?: number) {
    if (empId ?? 0 > 0) {
      const selectedValue = this.dataForm.get('EmployeeId')?.value;
      const selected = this.EmployeeDropdownValues.find(x => x.value == selectedValue);

      if (selected) {
        var arr = selected.text.split(" - ");
        this.selectedEmployeeCode = arr[0];
        this.selectedEmployeeName = arr[1];

        this.dataForm.patchValue({
          EmployeeCode: this.selectedEmployeeCode,
          EmployeeName: this.selectedEmployeeName,
          RegUserName: this.selectedEmployeeCode
        });

      }
      this.fetchModules(empId ?? 0);
    }
  }

  fetchModules(employeeId: number): void {
    this.apiService.getModulePermission(employeeId).subscribe({
      next: (response: ApiResponse) => {
        //this.userModules = response.data?.userXModule || [];
        this.userModules = response.data?.userXModule ?? [];
        // Preselect the selected role from response
        this.userModules.forEach(module => {
          //const selectedRole = module.roles.find(role => role.selected);
          const selectedRole = module.roles?.find(role => role.selected);
          module.selectedRole = selectedRole?.value || null;
        });
      },
      error: (err) => {
        console.error('Error fetching module permissions:', err);
      }
    });
  }

  getSelectedRoleValue(roles: any[]): string | null {
    const selected = roles.find((role: any) => role.selected);
    return selected ? selected.value : null;
  }


  onRoleChange(module: Module, event: any): void {
    module.selectedRole = event; // or event.target.value
    // console.log(`Role selected for module ${module.moduleId}: ${module.selectedRole}`);
  }


  validateModuleRoles(): boolean {
    for (const module of this.userModules) {
      if (module.isSelected && (!module.selectedRole || module.selectedRole === '')) {
        this.toastr.error(`Please select a role for module: ${module.moduleName}`);
        return false;
      }
    }
    return true;
  }

  onSubmit() {
    this.dataForm.markAllAsTouched();

    if (this.dataForm.invalid || this.dataForm.errors?.['passwordsMismatch']) {
      this.toastr.error('Please complete all required fields');
      return;
    }

    const invalidModules = this.userModules.filter(
      mod => mod.isSelected && (!mod.selectedRole || mod.selectedRole === "")
    );

    if (invalidModules.length > 0) {
      this.toastr.error('Please select roles for all selected modules');
      return;
    }

    this.isSubmitting = true;

    const employeeId = this.dataForm.value.EmployeeId;
    const finalEmployeeId = employeeId ? employeeId : this.editEmployeeId;

    const user_name = this.dataForm.value.RegUserName;
    const finalUserName = user_name ? user_name : "username";

    const password_chk = this.dataForm.value.Password;
    const finalPasswordChk = password_chk ? password_chk : "@Pass1111";

    const confPassword_chk = this.dataForm.value.ConfirmPassword;
    const finalConfPasswordChk = confPassword_chk ? confPassword_chk : "@Pass1111";

    // Prepare payload
    const dataObj = {
      UserId: this.editUserId ?? 0, // Replace with actual logic if editing
      EmployeeId: finalEmployeeId, //this.dataForm.value.EmployeeId?this.EmployeeId:this.editEmployeeId,
      UserName: finalUserName,
      Password: finalPasswordChk,
      ConfirmPassword: finalConfPasswordChk,

      rolesXModules: this.userModules
        .filter(mod => mod.isSelected)
        .map(mod => ({
          ModuleId: mod.moduleId,
          RoleId: Number(mod.selectedRole),
        }))
    };

    //console.log('Data:', dataObj);

    this.apiService.saveUserRegistration(dataObj).subscribe({
      next: (res) => {
        this.toastr.success('User registered successfully!');
        this.isSubmitting = false;
        this.router.navigate(['/adm/user/user-list']);
      },
      error: (err) => {
        this.toastr.error('Error during registration.');
        this.isSubmitting = false;
        console.error(err);
      }
    });

  }

  navigateToList() {
    this.router.navigate(['adm/user/user-list']);
  }

}
