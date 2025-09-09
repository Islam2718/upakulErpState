import { Component, ViewChild } from '@angular/core';
import { OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LeavesetupService } from '../../../../services/hrm/leavesetup/leavesetup.service';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup, } from '@angular/forms';
import { HostListener } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Leavesetup } from '../../../../models/hr/leavesetup/leavesetup';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { BtnService } from '../../../../services/btn-service/btn-service';
@Component({
  selector: 'app-leave-setup-list',
  imports: [CommonModule, ReactiveFormsModule,ConfirmModalComponent],
  templateUrl: './leave-setup-list.component.html',
  styleUrl: './leave-setup-list.component.css'
})
export class LeaveSetupListComponent implements OnInit, OnDestroy{
  qry: string | null = null;
  leavesetups: Leavesetup[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'LeaveTypeId';
  sortDirection = 'asc';
  sortOrder: string = 'LeaveTypeId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router, private http: HttpClient, private LeavesetupService: LeavesetupService, private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });

  }

  ngOnInit() {
    this.loadLeaveSetup();
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      this.loadLeaveSetup();
    });
  }

  loadLeaveSetup() {

    this.LeavesetupService
      .getLeaveSetups(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          //console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.leavesetups = response.listData;
            //console.log(response.listData);

            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
            // console.log(`Loading data for page: ${this.page}`); // ✅ Debugging
            // console.log("Total Records:", response.totalRecords); // ✅ Debugging
            // console.log("Total Pages:", this.totalPages); // ✅ Debugging
            // console.log("Total Pages Array:", this.totalPagesArray); // ✅ Debugging
            //this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
          } else {
            //console.error("Unexpected API response format:", response);
          }
        }),

        takeUntil(this.unsubscribe$) // Unsubscribe when component is destroyed
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }
  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      this.loadLeaveSetup();
    }
  }

  sortData(column: string) {
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    this.loadLeaveSetup();
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
    this.loadLeaveSetup();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadLeaveSetup();
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
    this.router.navigate(['hr/hrm/leave-setup-form']);
  }

  activeLabel: string | null = null;

  editLeaveSetup(LeaveTypeId: number) {
    //console.log("LeaveTypeId", LeaveTypeId);
    this.router.navigate(['/leave-setup-form/edit'], { state: { LeaveTypeId: LeaveTypeId } });
  }
 // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.LeavesetupService.deleteLeaveSetup(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadLeaveSetup(); // Reload list
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

