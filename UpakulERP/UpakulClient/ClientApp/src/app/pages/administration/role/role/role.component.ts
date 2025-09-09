import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { RoleService } from '../../../../services/administration/role.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';

interface DropdownValues {
  text: string;
  value: string;
}


@Component({
  selector: 'app-role',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css'
})

export class RoleComponent implements OnInit{
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  button = Button;
  message = '';
  Id ='';
  isEditMode = false;
  ModuleValues: DropdownValues[] = [];
  ModId: number = 0;


  constructor(
    private fb: FormBuilder, 
    private apiService: RoleService,
    private toastr: ToastrService, 
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      ModuleId: ['', Validators.required] ,   
      Name: ['', Validators.required]    
    });
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      Id: [null],
      Name: [''],
      ModuleId: [null]
    });

    const id = history.state.editId;
    if (id){
          this.isEditMode = true;
          this.ModId = id;
          this.apiService.getRoleById(id).subscribe(res => {
          const getData = res ?? res; // handle both wrapped and raw objects
          console.log('Fetched object for patching:', getData);
          if (getData){
               this.dataForm.patchValue({
                 Id: getData.id,
                 Name: getData.name,
                 ModuleId: getData.moduleId
                });
            }                      
          });     
     }
     this.loadDropDownModule();
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

  onDropDownModuleChange(event: any) {
    const selectedValue = event.target.value;
    this.ModId = selectedValue;
  }



     onSubmit() {
      if (this.dataForm.valid) {
        this.isSubmitting = true;
        //console.log('Before Edit', this.dataForm.value);
  
        if (this.isEditMode) {
         // console.log('In onSubmit EditMode');
          this.apiService.UpdateRole(this.dataForm.value).subscribe(() => {
           // this.router.navigate(['adm/role/role-list']);
            this.router.navigate(['adm/role/role-list'], { state: { moduId: this.ModId } });
          });
        } else {
          //console.log('Before Add');                  
          this.apiService.addRole(this.dataForm.value).subscribe({
            next: (response) => {
              debugger
              //console.log('Response:', response);
              this.toastr.success(this.message, 'Data inserted Successfully');         
              this.dataForm.reset();
            },
            error: (error) => {
              //console.error('Error:', error);
            this.toastr.error(this.message);
            },
            complete: () => {
              this.isSubmitting = false;
            }
          });
        }
      }      
    
    }
    navigateToList() {
      this.router.navigate(['adm/role/role-list']);
    }
    onReset(): void {
      this.dataForm.reset();
    }
}







  
