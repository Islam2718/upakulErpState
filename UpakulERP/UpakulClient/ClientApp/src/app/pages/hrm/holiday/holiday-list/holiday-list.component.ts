import { Component, ViewChild } from '@angular/core';
import { OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup, } from '@angular/forms';
import { HostListener } from '@angular/core';
import { DateDiffPipe } from '../../../../shared/utility/date-diff.pipe';
import { ToastrService } from 'ngx-toastr';
import { Holiday } from '../../../../models/hr/holiday/holiday.model';
import { HoliDayService } from '../../../../services/hrm/holiday/holiday.service';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-holiday-list',
  imports: [CommonModule, ReactiveFormsModule, ConfirmModalComponent,DateDiffPipe],
  standalone: true,
  templateUrl: './holiday-list.component.html',
  styleUrl: './holiday-list.component.css'
})
export class HolidayListComponent implements OnInit, OnDestroy {
  qry: string | null = null;
  holidays: Holiday[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'holiDayId';
  sortDirection = 'asc';
  sortOrder: string = 'holiDayId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  activeLabel: string | null = null;
  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router, 
    private http: HttpClient,
     private holidayService: HoliDayService,private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }
  ngOnInit() {
    this.loadHoliDays();

    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // this.totalPagesArray()
    // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      this.loadHoliDays();
    });
  }

  parseDate(dateStr: string): Date {
    return new Date(dateStr); // Or use a date library like moment or dayjs if needed
  }


  loadHoliDays() {
    this.holidayService
      .getHoliDays(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          //  console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.holidays = response.listData;
            // console.log(this.holydays);
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
            // console.log(`Loading data for page: ${this.page}`); // ✅ Debugging

            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
          } else {
            console.error("Unexpected API response format:", response);
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
    this.page = 1; // Reset to first page when search changes
    this.loadHoliDays();
  }
  sortData(column: string) {
    console.log("Sorting by:", column); // ✅ Debug log
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.loadHoliDays();
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
    console.log(`Navigating to page: ${p}`); // ✅ Debugging
    this.page = p;
    this.loadHoliDays();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadHoliDays();
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
  editHoliDay(holidayId: number) {
    // alert(holyDayId)
    this.router.navigate(['/holiday-form/edit'], { state: { holidayId: holidayId } });
  }
  // deleteHoliDay(holiDayId: number) {
  //   if (confirm('Are you sure you want to delete this holiday?')) {
  //     this.holidayService.deleteData(holiDayId).subscribe({
  //       next: () => {
  //         this.loadHoliDays(); // Reload list
  //       },
  //       error: err => console.error('Delete failed', err)
  //     });
  //   }
  // }
   // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.holidayService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadHoliDays(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }
  navigateToCreate() {
    this.router.navigate(['hr/hrm/holiday-form']);
  }
}
