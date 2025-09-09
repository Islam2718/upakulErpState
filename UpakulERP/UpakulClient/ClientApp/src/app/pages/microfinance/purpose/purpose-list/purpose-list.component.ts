import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PurposeServiceTs } from '../../../../services/microfinance/purpose/purpose.service.js'; // Adjust path if needed
import { PurposeGrid } from '../../../../models/microfinance/purpose/purpose-grid.js';
import { GridRequestModel } from '../../../../models/girdRequestModel.js';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup, } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component.js';
import { ToastrService } from 'ngx-toastr';
import { BtnService } from '../../../../services/btn-service/btn-service.js';
@Component({
  selector: 'app-purpose-list',
  imports: [CommonModule, ReactiveFormsModule,ConfirmModalComponent],
  standalone: true,  // ✅ Add this
  templateUrl: './purpose-list.component.html',
  styleUrl: './purpose-list.component.css'
})
export class PurposeListComponent {
   qry: string | null = null;
  gridModel: GridRequestModel;
  purposes: PurposeGrid[] = [];
  page: number = 1;
  sortDirection = 'asc';
  sortColumn = '';
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(10);
  activeLabel: string | null = null;
  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router,
    private http: HttpClient,
    private toastr: ToastrService,
    private purposeService: PurposeServiceTs) {
    // Initialize searchForm with searchTerm FormControl
    this.gridModel = {
      page: 1,
      pageSize: 10,
      search: '',
      sortOrder: 'c.Code asc'
    };


    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }

  ngOnInit() {
    this.loadPurposes();
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.gridModel.pageSize = newSize as number;
      this.gridModel.page = 1; // Reset to first page
      this.loadPurposes();
    });
  }


  loadPurposes() {
    this.gridModel.search = this.searchForm.get('searchTerm')?.value;
    this.purposeService
      .getList(this.gridModel)
      .pipe(
        tap(response => {
          //console.log("API Response for purpose: ", response);
          if (response && response.totalRecords !== undefined) {
            this.purposes = response.purposes;
            // console.log(this.purposes);
            this.totalPages = Math.ceil(response.totalRecords / this.gridModel.pageSize);
            this.totalRecords = response.totalRecords;
            this.totalPages = Math.ceil(response.totalRecords / this.gridModel.pageSize);
          } else {
            console.error("Unexpected API response format:", response);
          }
        }),
        takeUntil(this.unsubscribe$) // Unsubscribe when component is destroyed
      )
      .subscribe();
  }

  edit(editId: number) {
    this.router.navigate(['/purpose/edit'], { state: { editId: editId } });
  }

  // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.purposeService.delete(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadPurposes(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }


  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }
  onSearchChange(searchTerm: string) {
    this.gridModel.page = 1; // Reset to first page when search changes
    this.loadPurposes();
  }

  sortData(column: string) {
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.gridModel.sortOrder = column + " " + (this.sortDirection === 'asc' ? 'desc' : 'asc');
    this.loadPurposes();
  }

  get totalPagesArray(): (number | string)[] {
    const pages: (number | string)[] = [];

    if (this.totalPages <= 6) {
      // If there are 6 or fewer pages, show all
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    pages.push(1); // Always show first page

    if (this.gridModel.page > 3) {
      pages.push('...'); // Dots before middle pages
    }

    // Show two pages before and after the current page
    for (let i = this.gridModel.page - 1; i <= this.gridModel.page + 1; i++) {
      if (i > 1 && i < this.totalPages) {
        pages.push(i);
      }
    }

    if (this.gridModel.page < this.totalPages - 2) {
      pages.push('...'); // Dots before the last page
    }

    pages.push(this.totalPages); // Always show last page

    return pages;
  }

  changePage(p: number | string): void {
    if (typeof p === 'string') return; // Ignore '...' clicks
    if (p < 1 || p > this.totalPages) return; // Prevent invalid pages
    //console.log(`Navigating to page: ${p}`); // ✅ Debugging
    this.gridModel.page = p;
    this.loadPurposes();
  }

  changePageSize(newSize: number | null): void {
    this.gridModel.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadPurposes();
  }
  // ✅ Pagination logic for 1-4, dots, last page
  getPaginationNumbers(): (number | string)[] {
    if (this.totalPages <= 6) {
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    const pages: (number | string)[] = [1, 2, 3, 4];

    if (this.gridModel.page > 4) {
      pages.push('...'); // ✅ No more TypeScript error
    }

    pages.push(this.totalPages);
    return pages;
  }

  get dataRangeLabel(): string {
    if (this.totalRecords === 0) {
      return 'No records found';
    }

    const startIndex = (this.gridModel.page - 1) * this.gridModel.pageSize + 1;
    let endIndex = this.gridModel.page * this.gridModel.pageSize;

    if (endIndex > this.totalRecords) {
      endIndex = this.totalRecords; // Prevent exceeding total records
    }

    return `Showing ${startIndex} - ${endIndex} of ${this.totalRecords} records`;
  }

  navigateToCreate() {
    this.router.navigate(['mf/purpose/purpose-form']);
  }
}
