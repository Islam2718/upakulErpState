import { Component, OnDestroy, OnInit, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployeeRegisterService } from '../../../../services/microfinance/employee-register/employee-register.service';
import { EmployeeRegister } from '../../../../models/microfinance/employee-register/employee-register';
import { Modal } from 'bootstrap';
import { BtnService } from '../../../../services/btn-service/btn-service';

// declare var bootstrap: any;


@Component({
  selector: 'app-employee-register-list',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
    ],
  templateUrl: './employee-register-list.component.html',
  styleUrl: './employee-register-list.component.css'
})
export class EmployeeRegisterListComponent implements OnInit, OnDestroy, AfterViewInit {
  qry: string | null = null;
    releaseForm: FormGroup; 

    isSubmitting = false;
    isEditMode = false;
    isPermitted: boolean = false;

    listData: EmployeeRegister[] = [];
    page = 1;
    pageSize = 10;
    totalPages = 1;
    totalRecords = 0;
    searchTerm = '';
    sortColumn = 'Id';
    sortDirection = 'asc';
    sortOrder: string = 'Id asc'
    private unsubscribe$ = new Subject<void>(); // For cleanup
   // searchForm: FormGroup;
    // ✅ Form Control for page size selection
    pageSizeControl = new FormControl(this.pageSize);
    searchForm: FormGroup;
  
     @ViewChild('releaseModal') releaseModal!: ElementRef;   // modal reference
    private releaseModalInstance!: Modal;

    ngAfterViewInit() {
      // Initialize Bootstrap modal instance
      this.releaseModalInstance = new Modal(this.releaseModal.nativeElement);
    }

    openReleaseModal() {
      this.releaseModalInstance.show();
    }

    closeReleaseModal() {
      if (this.releaseModalInstance) {
        this.releaseModalInstance.hide();   // ✅ removes backdrop automatically
      }
    }
   
    
  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    public router: Router, 
    private http: HttpClient, 
    private apiService: EmployeeRegisterService,
    private toastr: ToastrService) {
      // Initialize searchForm with searchTerm FormControl
      this.searchForm = this.fb.group({
         searchTerm: ['']
      });

     this.releaseForm = this.fb.group({
         Id:[null],
         ReleaseNote:'',
         EmployeeId: [null],
         GroupId: [null],
         ReleaseGroup: '',
         ReleaseEmployee: '',
         JoiningDate: ''
     });
 }
    
    ngOnInit() {

      const transDate = localStorage.getItem('transactionDate');
      // if (transDate) { this.transactionDate = transDate; }
      // this.dataForm.patchValue({ AdmissionDate: this.transactionDate });
      
      const personalData = localStorage.getItem('personal');
      if (personalData) { const parsed = JSON.parse(personalData); this.isPermitted = parsed.office_type_id === 6; }
   

      this.loadList();
      // console.log("this.loadList", this.listData);
        // Listen for value changes on the searchTerm control
        this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
          this.onSearchChange(searchTerm);
        });
        // ✅ Listen for pageSize changes
        this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
          this.pageSize = newSize as number;
          this.page = 1; // Reset to first page
          this.loadList();
        });

    this.releaseForm = this.fb.group({
      Id:[null],
      ReleaseNote:'',
      EmployeeId: [null],
      GroupId: [null],
      ReleaseGroup: '',
      ReleaseEmployee: '',
      JoiningDate: ''
    });

  }


  loadList() {
    this.apiService
      .getDataList(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          if (response && response.totalRecords !== undefined) {
            this.listData = response.listData;

            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;

            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
          } else {
            console.error("Unexpected API response format:", response);
          }
        }),
        takeUntil(this.unsubscribe$) // Unsubscribe when component is destroyed
      )
      .subscribe();
  }

  setReleaseModalFunc(data: any) {
  //  console.log('_params_data', data);
    this.openReleaseModal();
    this.releaseForm.patchValue({
      Id:data.id,
      ReleaseNote: data.releaseNote,
      EmployeeId: data.employeeId,
      GroupId: data.groupId,
      JoiningDate: data.joiningDate,
      ReleaseGroup: data.groupCode+" - "+data.groupName,
      ReleaseEmployee: data.employeeCode+" - "+data.employeeName
    });
  }




  onSubmitRelease(): void {
        if (this.releaseForm.invalid) {
            this.releaseForm.markAllAsTouched(); // to show validation errors
            return;
          }

      const formData = this.releaseForm.getRawValue();
      this.apiService.release(formData).subscribe({
        next: (response) => {
           this.toastr.success(response.message || 'Released successfull', 'Success');
           this.closeReleaseModal(); 
           this.loadList();
           this.releaseForm.reset();
        },
        error: (error) => {
         // const errorMessage = error.error?.message || 'Something went wrong';
          this.toastr.error(error.error?.message || 'Error saving cheque', 'Error');

          //this.toastr.error(error?.message || 'Error saving cheque', 'Error');
        }
      });

  }


  ////End Release Modal 

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }

  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      this.loadList();
    }
  }

  sortData(column: string) {
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    this.loadList();
  }

  get totalPagesArray(): (number | string)[] {
    const pages: (number | string)[] = [];

    if (this.totalPages <= 6) {
      // If there are 6 or fewer pages, show all
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    pages.push(1); // Always show first page

    if (this.page > 3) {
      pages.push('...'); // Dots before middle pages
    }

    // Show two pages before and after the current page
    for (let i = this.page - 1; i <= this.page + 1; i++) {
      if (i > 1 && i < this.totalPages) {
        pages.push(i);
      }
    }

    if (this.page < this.totalPages - 2) {
      pages.push('...'); // Dots before the last page
    }
    pages.push(this.totalPages); // Always show last page
    return pages;
  }

  changePage(p: number | string): void {
    if (typeof p === 'string') return; // Ignore '...' clicks
    if (p < 1 || p > this.totalPages) return; // Prevent invalid pages
    //console.log(`Navigating to page: ${p}`); // ✅ Debugging
    this.page = p;
    this.loadList();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadList();
  }

  // ✅ Pagination logic for 1-4, dots, last page
  getPaginationNumbers(): (number | string)[] {
    if (this.totalPages <= 6) {
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    const pages: (number | string)[] = [1, 2, 3, 4];

    if (this.page > 4) {
      pages.push('...'); // ✅ No more TypeScript error
    }

    pages.push(this.totalPages);
    return pages;
  }

  get dataRangeLabel(): string {
    if (this.totalRecords === 0) {
      return 'No records found';
    }

    const startIndex = (this.page - 1) * this.pageSize + 1;
    let endIndex = this.page * this.pageSize;

    if (endIndex > this.totalRecords) {
      endIndex = this.totalRecords; // Prevent exceeding total records
    }
    return `Showing ${startIndex} - ${endIndex} of ${this.totalRecords} records`;
  }

  navigateToCreate() {
    this.router.navigate(['mf/employee-register/employee-register-form']);
  }



}
