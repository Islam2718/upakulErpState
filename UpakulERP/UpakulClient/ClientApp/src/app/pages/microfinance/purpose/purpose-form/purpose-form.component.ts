import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Message } from '../../../../shared/enums/message.enum';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { PurposeServiceTs } from '../../../../services/microfinance/purpose/purpose.service';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { Subject, Observable, of } from 'rxjs';
import {
  debounceTime,
  distinctUntilChanged,
  switchMap,
  tap,
  filter,
  catchError,
} from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface DropDown {
  text: string;
  value: string;
}

@Component({
  selector: 'app-purpose-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgSelectModule],

  templateUrl: './purpose-form.component.html',
  styleUrl: './purpose-form.component.css'
})
export class PurposeFormComponent implements OnInit {
  qry: string | null = null;
  dataForm!: FormGroup;
  isSubmitting = false;
  successMessage = '';
  message = Message;
  educationId = '';
  isEditMode = false;
  category = '';

  MRAPurposeDropdown: DropDown[] = [];
  mainPurposeDropdown: DropDown[] = [];
  subcategoryPurposeDropdown: DropDown[] = [];

  //filteredMainPurposeDropdown: any[] = [];
  typeahead$ = new Subject<string>();
  isLoading = false;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private toastr: ToastrService,
    public router: Router,
    private apiService: PurposeServiceTs,
    private http: HttpClient,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) { }

  ngOnInit() {
    this.dataForm = this.fb.group({
      Id: [null],
      Name: [''],
      Code: [''],
      ParentId: [null],
      MRAPurposeId: [],
      ParentName: [''],
      Category: [],
      CategoryId: [],
      Subcategory: [],
      SubcategoryId: [],

    });
    this.loadMainPurposeDropDown();
    const id = history.state.editId;
    if (id) {
      this.isEditMode = true;
      this.apiService.getById(id).subscribe(res => {
        const restData = res.data ?? res; // handle both wrapped and raw objects
        // console.log('Fetched education object for patching:', restData);

        if (restData) {
          this.dataForm.patchValue({
            Id: restData.id,
            Name: restData.name,
            Code: restData.code,
            Category: restData.category,
            CategoryId: restData.categoryId,

            Subcategory: restData.subcategory,
            SubcategoryId: restData.subcategoryId,
            MRAPurposeId: restData.mraPurposeId,
            ParentId: restData.parentId,
          });

          if (restData.subcategoryId) {
            this.dataForm.get('CategoryId')?.patchValue(restData.categoryId, { emitEvent: true });
            const selectCategory = document.querySelector('select[formControlName="CategoryId"]')!;
            selectCategory.dispatchEvent(new Event('change'));
            this.loadMRAPurposeDropDown(restData.category, restData.subcategory);
          }
          else {
            this.loadMRAPurposeDropDown(restData.category);
            this.dataForm.get('MRAPurposeId')?.patchValue(restData.mraPurposeId, { emitEvent: true });
            // var select_sub = document.querySelector('select[formControlName="SubcategoryId"]')!;
            // select_sub.dispatchEvent(new Event('change'));
          }
        }
      });
    }
  }


  loadMRAPurposeDropDown(category: string, subcategory: string = "") {
    this.apiService.getMRAPurposeDropdown(category, subcategory).subscribe({
      next: (data) => {
        this.MRAPurposeDropdown = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadMainPurposeDropDown(pid: any = null) {
    this.apiService.getMainPurposeDropdown(pid).subscribe({
      next: (data) => {
        if (pid > 0)
          this.subcategoryPurposeDropdown = data;
        else
          this.mainPurposeDropdown = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }
  onCategoryChange(event: any) {

    this.category = event.target.options[event.target.options.selectedIndex].text;
    this.loadMainPurposeDropDown(event.target.value);
    this.loadMRAPurposeDropDown(this.category);
    //}
  }
  onSubcategoryChange(event: any) {

    const subcategory = event.target.options[event.target.options.selectedIndex].text;
    this.loadMRAPurposeDropDown(this.category, subcategory);
    //}
  }
  onSubmit() {
    if (this.dataForm.valid) {
      this.isSubmitting = true;

      const parentId: number = (this.dataForm.controls['SubcategoryId'].value ? this.dataForm.controls['SubcategoryId'].value : this.dataForm.controls['CategoryId'].value ? this.dataForm.controls['CategoryId'].value : null);
      this.dataForm.controls['ParentId'].setValue(parentId);

      if (this.isEditMode) {
        this.apiService.Update(this.dataForm.value).subscribe({
          next: (response) => {
            //console.log('desig Update Response:', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['mf/purpose/purpose-list']);
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
      } else {

        this.apiService.add(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.dataForm.reset();
          },
          error: (error) => {
            //console.error('Error:', error);
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            //console.log(error)
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }

    }
  } navigateToList() {
    this.router.navigate(['mf/purpose/purpose-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }
}