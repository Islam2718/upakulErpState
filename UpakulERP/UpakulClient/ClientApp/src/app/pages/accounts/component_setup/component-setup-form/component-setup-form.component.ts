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
import { ComponentsetupService} from '../../../../services/accounts/componentsetupService/componentsetup.service';
import { Message } from '../../../../shared/enums/message.enum';

interface ComponentTypeValue{
  text: string;
  value: string;
}
@Component({
  selector: 'app-component-setup-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './component-setup-form.component.html',
  styleUrl: './component-setup-form.component.css'
})

export class ComponentSetupFormComponent {
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  button = Button;
  message = Message;
  ComponentTypeValues: ComponentTypeValue[] = [];

  constructor(private fb: FormBuilder, 
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private componentsetupService:ComponentsetupService ) {
    this.dataForm = this.fb.group({
      LineItem: ['', Validators.required],    
    });
  }


  ngOnInit() {
    this.dataForm = this.fb.group({
      ComponetSetupId: [null],
      ComponentType: [''],
      LineItem: [''],
    
    });
    const id = history.state.bankId;
    if (id) {
      this.isEditMode = true;
      this.componentsetupService.GetData(id).subscribe(res => {
        const componentsetup = res.data ?? res; // handle both wrapped and raw objects
        // console.log('Fetched bank object for patching:', bank);
  
        if (componentsetup) {
          this.dataForm.patchValue({
            ComponetSetupId: componentsetup.componetSetupId,
            ComponentType: componentsetup.componentType,
            LineItem: componentsetup.lineItem,
          });
        }
      });
    
    }
    this.loadDropDown();
  }

  loadDropDown() {
    this.componentsetupService.getDataTypeDropdown().subscribe({
      next: (data) => {
        this.ComponentTypeValues = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownTypeChange(event: any) {
    const selectedComponentSetupType = event.target.value; // Get selected value correctly     
  }
  
  
  onSubmit() {

    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        this.componentsetupService.Update(this.dataForm.value).subscribe(() => {
          this.router.navigate(['/ac/accounts/component-setup-list']);
        });
      } else {
        
        this.componentsetupService.add(this.dataForm.value).subscribe({
          next: (response) => {
            // console.log('Response:', response);
            this.toastr.success('Data '+ this.message.SaveMsg, 'Success');  
            this.dataForm.reset();
          },
          error: (error) => {
            console.error('Error:', error);
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
    this.router.navigate(['/ac/accounts/component-setup-list']);
  }
  onReset(): void {
    this.dataForm.reset();
  }
}
