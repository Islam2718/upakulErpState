import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../../shared/enums/button.enum';

import { ComponentMFService } from '../../../../../services/microfinance/components/componentMF/componentMF.service';
import { MasterComponentService } from '../../../../../services/microfinance/components/masterComponent/masterComponent.service';
import { BtnService } from '../../../../../services/btn-service/btn-service';

interface Dropdown {
  text: string;
  value: string | number;
}

@Component({
  selector: 'app-component-mf-form',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './mfComponent-form.component.html',
  styleUrls: ['./mfComponent-form.component.css']
})
export class ComponentMFFormComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  isEditMode = false;
  button = Button;
  masterComponents: Dropdown[] = [];
  ComponentTypeValues: Dropdown[] = [];
  PaymentFequencyValues: Dropdown[] = [];

  // Visibility flags
  isLoanVisible = false;
  isSavingVisible = false;
  isTermDPSSavingVisible = false;
  isTermFDRSavingVisible = false;
  isCommonVisible = false;
  isLoanTypeVisible = false;
  // RequiredFields=[];

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute

    private fb: FormBuilder,
    private apiService: ComponentMFService,
    private masterComponentService: MasterComponentService,
    private toastr: ToastrService,
    public router: Router
  ) {
    this.dataForm = this.fb.group({
      Id: [null],
      MasterComponentId: [null, Validators.required],
      LoanType: ['G'],
      SavingMap: [null],
      //PaymentFrequency: [null, Validators.required],
      PaymentFrequency: [null],
      ComponentCode: ['', Validators.required],
      ComponentName: ['', Validators.required],
      InterestRate: ['', Validators.required],
      ComponentType: ['', Validators.required],
      DurationInMonth: [null],
      NoOfInstalment: [null],
      GracePeriodInDay: [null],
      //CalculationMethod: ['R', Validators.required],
      CalculationMethod: ['R'],
      MinimumLimit: ['', Validators.required],
      MaximumLimit: ['', Validators.required],
      Latefeeperchantage: [null],
    });
  }

  ngOnInit() {
    this.loadMasterComponents();
    this.loadComponents();
    this.loadPaymentFequency();
    const id = history.state.mfId;

    if (id) {
      this.isEditMode = true;
      this.apiService.getComponentMFById(id).subscribe(res => {
        const data = res ?? res;
        if (data) {
          this.dataForm.patchValue({
            Id: data.id,
            MasterComponentId: data.masterComponentId,
            ComponentName: data.componentName,
            ComponentCode: data.componentCode,
            LoanType: data.loanType,
            SavingMap: data.savingMap,
            ComponentType: data.componentType,
            PaymentFrequency: data.paymentFrequency,
            InterestRate: data.interestRate,
            DurationInMonth: data.durationInMonth,
            NoOfInstalment: data.noOfInstalment,
            GracePeriodInDay: data.gracePeriodInDay,
            MinimumLimit: data.minimumLimit,
            MaximumLimit: data.maximumLimit,
            CalculationMethod: data.calculationMethod,
            Latefeeperchantage: data.latefeeperchantage,
          });
          
          this.onComponentTypeDropDownChange(data.componentType);
        }
      });
    }
  }

  onComponentTypeDropDownChange(event: any): any {   
    const value = event?.target?.value || event; // handles both event object or direct value
    this.applyFieldVisibility(value);
  }
  onLoanTypeDropDownChange(event: any) {
     const value = event?.target?.value || event;
    this.isLoanTypeVisible = value == 'G' ? true : false;
  }
  applyFieldVisibility(type: string | null): void {
    this.isLoanVisible = this.isTermDPSSavingVisible = this.isTermFDRSavingVisible = this.isCommonVisible = this.isLoanTypeVisible = false;
    type == 'L' ? this.isLoanVisible = this.isCommonVisible = this.isTermFDRSavingVisible = true
      : type == 'S' || type == 'V' ? this.isSavingVisible = true
        : type == 'D' ? this.isTermDPSSavingVisible = this.isCommonVisible = this.isTermFDRSavingVisible = true
          : type == 'F' ? this.isCommonVisible = true
            : false;

    this.isLoanTypeVisible = type == 'L' && this.dataForm.get('LoanType')?.value == 'G' ? true : false;

    this.applyValidationWithClean(type);
    //this.applyAdditionalValidation(type);
  }
  applyValidationWithClean(type: string | null): void {
    const allFields = [
      'LoanType', 'PaymentFrequency', 'InterestRate', 'DurationInMonth', 'NoOfInstalment', 'GracePeriodInDay', 'CalculationMethod', 'MinimumLimit', 'MaximumLimit'
    ];
    // const RequiredFields = [];    
    allFields.forEach(field => {
      const control = this.dataForm.get(field);
      control?.clearValidators();
      control?.updateValueAndValidity();
    });

    // console.log('_req:',allFields);
    if (type == 'L') {
      const loanFields = [
        'LoanType', 'PaymentFrequency', 'InterestRate', 'DurationInMonth', 'NoOfInstalment', 'GracePeriodInDay', 'CalculationMethod', 'MinimumLimit', 'MaximumLimit'
      ];
      loanFields.forEach(field => {
        this.dataForm.get(field)?.setValidators([Validators.required]);
      });
    }
    else if (type == 'S' || type == 'V') {
      // this.dataForm.value.LoanType = 'svv';
      const loanFields = [
        'InterestRate'
      ];
      loanFields.forEach(field => {
        this.dataForm.get(field)?.setValidators([Validators.required]);
      });
    }
    else if (type == 'D') {
      const loanFields = [
        'PaymentFrequency', 'InterestRate', 'DurationInMonth', 'NoOfInstalment', 'MinimumLimit', 'MaximumLimit'
      ];
      loanFields.forEach(field => {
        this.dataForm.get(field)?.setValidators([Validators.required]);
      });
    }
    else if (type == 'F') {
      const loanFields = [
        'InterestRate', 'DurationInMonth', 'MinimumLimit', 'MaximumLimit'
      ];
      loanFields.forEach(field => {
        this.dataForm.get(field)?.setValidators([Validators.required]);
      });
    }
  }

  loadMasterComponents(): void {
    this.apiService.getMasterComponentDropdown().subscribe({
      next: (response) => {
        this.masterComponents = response;
        const defaultValue = this.masterComponents[0]?.value;
        this.dataForm.get('MasterComponentId')?.setValue(defaultValue);
      },
      error: (err) => {
        console.error('Error loading master components', err);
      }
    });
  }

  loadComponents(): void {
    this.apiService.getComponentTypeDropdown().subscribe({
      next: (response) => {
        this.ComponentTypeValues = response;
        const selectedOption = this.ComponentTypeValues.find(opt => opt.value == "L");
        const defaultValue = selectedOption?.value ?? this.ComponentTypeValues[0]?.value;
        this.applyFieldVisibility(String(defaultValue));
        this.dataForm.get('ComponentType')?.setValue(defaultValue);
      },
      error: (err) => {
        console.error('Error loading component types', err);
      }
    });
  }

  loadPaymentFequency(): void {
    this.apiService.getPaymentFequencyDropdown().subscribe({
      next: (response) => {
        this.PaymentFequencyValues = response;
      },
      error: (err) => {
        console.error('Error loading component types', err);
      }
    });
  }

  OnChangeMasterComponent(event: any) {
    const selectedValue = event.target.value;
    this.dataForm.controls['MasterComponentId'].setValue(selectedValue);
  }
  onSubmit() {

    if (this.dataForm.invalid) {
      this.dataForm.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;
    const formData = this.dataForm.getRawValue();

    // Normalize optional numeric values
    formData.LoanType = formData.ComponentType != 'L' ? null : formData.LoanType;
    formData.SavingMap = formData.ComponentType == 'L' && formData.LoanType == 'G' ? formData.SavingMap : null;
    formData.PaymentFrequency = formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.PaymentFrequency;
    formData.DurationInMonth = formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.DurationInMonth;
    formData.NoOfInstalment = formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.NoOfInstalment;

    formData.GracePeriodInDay = formData.ComponentType != 'L' ? null : formData.GracePeriodInDay;
    formData.CalculationMethod = formData.ComponentType != 'L' ? null : formData.CalculationMethod;
    formData.Latefeeperchantage = formData.ComponentType == 'F' || formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.Latefeeperchantage;
    formData.MinimumLimit = formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.MinimumLimit;
    formData.MaximumLimit = formData.ComponentType == 'S' || formData.ComponentType == 'V' ? null : formData.MaximumLimit;


    if (this.isEditMode) {
      this.apiService.updateData(formData).subscribe({
        next: (response) => {
          this.toastr.success(response.message, 'Success');
          // this.toastr.success('Data updated successfully!', '', {
          //   toastClass: 'toast-orange ngx-toastr'
          // });
          this.isSubmitting = false;
        },
        error: err => {
          this.toastr.error('Update failed');
          this.isSubmitting = false;
        }
      });
    } else {
      this.apiService.addData(formData).subscribe({
        next: (response) => {
          this.toastr.success(response.message, 'Success');
          const fieldsToReset = ['ComponentCode', 'ComponentName', 'InterestRate'];

          fieldsToReset.forEach(field => {
            this.dataForm.get(field)?.reset('', { emitEvent: true });
          });
          this.dataForm.value.componentName
          this.isSubmitting = false;
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

  navigateToList() {
    this.router.navigate(['mf/Component/component-mf-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }
}