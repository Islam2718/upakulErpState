import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field'; // ✅ FIXED
import { ActivatedRoute, Router } from '@angular/router';
import { LeavemappingService } from '../../../../services/hrm/leavemapping/leavemapping.service';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface OfficeTypeValue {
  text: string;
  value: string;
}
interface LeaveCategoryTypeValue {
  text: string;
  value: string;
}
interface DesignationsValue {
  text: string;
  value: string;
}


@Component({
  selector: 'app-leave-mapping-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './leave-mapping-form.component.html',
  styleUrl: './leave-mapping-form.component.css'
})
export class LeaveMappingFormComponent implements OnInit {
  qry: string | null = null;
  dataForm!: FormGroup;
  isEditMode = false;
  isSubmitting = false;
  officeTypes: OfficeTypeValue[] = [];
  designations: DesignationsValue[] = [];
  leaveCategories: LeaveCategoryTypeValue[] = [];
  levels = ['Level 1', 'Level 2', 'Level 3']; // or load from API if dynamic

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    public router: Router,
    private activeatedRoute: ActivatedRoute,   // ✅ This one, not Router,
    private http: HttpClient,
    private apiService: LeavemappingService,
  ) {

  }

  ngOnInit(): void {
    this.dataForm = this.fb.group({
      OfficeTypeId: ['', Validators.required],
      ApplicantDesignationId: ['', Validators.required],
      LeaveCategoryId: ['', Validators.required],
      mappings: this.fb.array([])
    });

    this.OfficeTypeDropdown();
    this.DesignationDropdown();
    this.LeaveCategoryDropdown();

    // Add first row by default
    this.addRow();
  }

  get mappings(): FormArray {
    return this.dataForm.get('mappings') as FormArray;
  }
  OfficeTypeDropdown(): void {
    this.apiService.getOfficeTypeDropdown().subscribe({
      next: (data) => {
        this.officeTypes = data;
      },
      error: (err) => {
        console.error('Error fetching office types:', err);
      }
    });
  }
  DesignationDropdown(): void {
    this.apiService.getDesignationTypeDropdown().subscribe({
      next: (data) => {
        this.designations = data;
      },
      error: (err) => {
        console.error('Error fetching designation:', err);
      }
    });
  }
  LeaveCategoryDropdown(): void {
    this.apiService.getLeaveCategoryTypeDropdown().subscribe({
      next: (data) => {
        this.leaveCategories = data;
      },
      error: (err) => {
        console.error('Error fetching leave categories:', err);
      }
    });
  }

  get rows(): FormArray {
    return this.dataForm.get('rows') as FormArray;
  }


  addRow() {
    const nextLevel = this.mappings.length + 1;
    const row = this.fb.group({
      ApproverDesignationId: ['', Validators.required],
      LevelNo: [nextLevel, Validators.required],
      MinimumLeave: ['', [Validators.required, Validators.min(0)]],
      MaximumLeave: ['']
    });

    this.mappings.push(row);
  }

  deleteRow(index: number) {
    this.mappings.removeAt(index);
    this.recalculateLevels();
  }
  recalculateLevels() {
    this.mappings.controls.forEach((group, index) => {
      group.get('LevelNo')?.setValue(index + 1);
    });
  }
  submit() {
    if (this.dataForm.valid) {
      const formValue = this.dataForm.value;

      const requestBody = {
        OfficeTypeId: +formValue.OfficeTypeId,
        ApplicantDesignationId: +formValue.ApplicantDesignationId,
        LeaveCategoryId: formValue.LeaveCategoryId,
        Mappings: formValue.mappings.map((m: any) => ({  // ✅ PascalCase key
          ApproverDesignationId: +m.ApproverDesignationId,
          LevelNo: +m.LevelNo,
          MinimumLeave: +m.MinimumLeave,
          MaximumLeave: +m.MaximumLeave
        }))
      };


      this.apiService.createLeaveMappingWithDetails(requestBody).subscribe({
        next: (res) => {
          console.log('API Response:', res);
          alert(res?.message || 'Saved successfully!');
        },
        error: (err) => {
          console.error('Submit error:', err);
          alert('Error saving data.');
        }
      });
    } else {
      this.dataForm.markAllAsTouched();
    }
  }

  DesignationOnChange(event: any): void {
    const selectedValue = event.target.value;
  }
  OfficeTypeOnChange(event: any): void {
    const selectedValue = event.target.value;
  }
  LeaveCategoryOnChange(event: any): void {
    const selectedValue = event.target.value;

  }

}
