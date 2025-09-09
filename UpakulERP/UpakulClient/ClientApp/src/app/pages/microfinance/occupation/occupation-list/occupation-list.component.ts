import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OccupationService } from '../../../../services/microfinance/occupation/occupation.service'; // Adjust path if needed
import { Occupation } from '../../../../models/microfinance/occupation/occupation';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { BtnService } from '../../../../services/btn-service/btn-service';


@Component({
  selector: 'app-occupation-list',
  imports: [CommonModule, ReactiveFormsModule,ConfirmModalComponent],
  templateUrl: './occupation-list.component.html',
  styleUrl: './occupation-list.component.css'
})
export class OccupationListComponent implements OnInit, OnDestroy{
  qry: string | null = null;
  isSubmitting = false;
  listData: Occupation[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'OccupationId';
  sortDirection = 'asc';
  sortOrder: string = 'OccupationId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  
  
  constructor(
  public Button: BtnService,
            // private route: ActivatedRoute
  private fb: FormBuilder, // FormBuilder to easily create the form group
  public router: Router, 
  private http: HttpClient, 
  private apiService: OccupationService,
  private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }

  ngOnInit() {
    this.loadData();    
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
   // this.totalPagesArray()
   // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
    this.pageSize = newSize as number;
    this.page = 1; // Reset to first page
    this.loadData();
  });
  }



   editData(editId: number) {
     this.router.navigate(['/occupation/edit'], { state: { editId: editId } });
   }
   // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.apiService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadData(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }

     loadData() {
    this.apiService
      .getDataList(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          // console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.listData = response.listData;
            //console.log(this.samities);
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
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
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      this.loadData();
    }
  }

  sortData(column: string) {
   // console.log("Sorting by:", column); // ✅ Debug log
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    this.loadData();
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
  this.loadData();
}

changePageSize(newSize: number | null): void {
  this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadData();
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
      this.router.navigate(['mf/occupation/occupation']);
    }
  
    activeLabel: string | null = null;

}
