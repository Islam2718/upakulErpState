import { Component, ViewChild } from '@angular/core';
import { OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { EmployeeService } from '../../../../services/hrm/employeeprofile/employee.service';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup, } from '@angular/forms';
import { HostListener } from '@angular/core';
import { ConfigService } from '../../../../core/config.service';
import { ToastrService } from 'ngx-toastr';
import { response } from 'express';
import { Employee } from '../../../../models/hr/employeeprofile/employee';
import { ImageurlMappingConstant } from '../../../../shared/image-url-mapping-constant';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-employee-list',
  imports: [CommonModule, ReactiveFormsModule,ConfirmModalComponent],
  standalone: true,
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit, OnDestroy {
  qry: string | null = null;
  lstObj: Employee[] = [];
  empImgURL: string =  '';
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'employeeId';
  sortDirection = 'asc';
  sortOrder: string = 'employeeId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  activeLabel: string | null = null;
  private domain_url_hrm: string;
  constructor(
    public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router, private http: HttpClient, private toastr: ToastrService, private employeeService: EmployeeService
    , private configService: ConfigService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
    this.domain_url_hrm=configService.hrmApiBaseUrl();
  }
  ngOnInit() {
this.empImgURL=this.domain_url_hrm.replace("/api/v1","")+ImageurlMappingConstant.IMG_BASE_URL;


    this.LoadEmployee();
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      this.LoadEmployee();
    });
  }


  LoadEmployee() {
    console.log("IN LoadEmployee");
    this.employeeService.getEmployeeProfileGrid(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder).pipe(
      tap(response => {
        if (response && response.totalRecords !== undefined) {
          this.lstObj = response.listData;
          //this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
          this.totalRecords = response.totalRecords;
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        }
      }),
      takeUntil(this.unsubscribe$)
    ).subscribe();
  }

  // parseDate(dateStr: string): Date {
  //   return new Date(dateStr); // Or use a date library like moment or dayjs if needed
  // }



  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }
  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      this.LoadEmployee();
    }
  }
  sortData(column: string) {
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    this.LoadEmployee();
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
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
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
    this.router.navigate(['hr/hrm/emp-profile']);
  }

  edit(employeeid: number) {
    // alert(employeeid)
    this.router.navigate(['/emp-profile/edit'], { state: { employeeId: employeeid } });
  }
  // delete(employeeid: number) {
  //  if (confirm('Are you sure you want to delete this Employee?')) {
  //     this.employeeService.deleteEmployee(employeeid).subscribe({
  //       next: (response) => {
  //         if (response.type === 'warning')
  //           this.toastr.warning(response.message, 'Warning');
  //         else if (response.type === 'strongerror')
  //           this.toastr.error(response.message, 'Error');
  //         else
  //           this.toastr.success(response.message, 'Success');

  //         this.LoadEmployee(); // Reload list
  //       },
  //       error: (error) => {
  //         if (error.type === 'warning') {
  //           this.toastr.warning(error.message, 'Warning');
  //         } else if (error.type === 'strongerror') {
  //           this.toastr.error(error.message, 'Error');
  //         } else {
  //           this.toastr.error('Delete failed');
  //         }
  //         //console.error('Delete error:', error);
  //       },
  //       complete: () => {
  //       }
  //     });
  //   }
  // }
// Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.employeeService.delete(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.LoadEmployee(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }
}
