import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BoardUniversityService } from '../../../../services/hrm/boarduniversity/board-university.service'; // Adjust path if needed
import { BoardUniversity } from '../../../../models/hr/university/board-university';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { BtnService } from '../../../../services/btn-service/btn-service';
@Component({
  selector: 'app-board-universitylist',
  imports: [CommonModule, ReactiveFormsModule,ConfirmModalComponent],
  templateUrl: './board-universitylist.component.html',
  styleUrl: './board-universitylist.component.css'
})
export class BoardUniversitylistComponent implements OnInit, OnDestroy {
  qry: string | null = null;
  dataList: BoardUniversity[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'buId';
  sortDirection = 'asc';
  sortOrder: string = 'buId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);


  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    private router: Router,
    private http: HttpClient,
    private dataService: BoardUniversityService,
    private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }


  ngOnInit() {
    this.loadList();
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // this.totalPagesArray()
    // ✅ Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      this.loadList();
    });
  }


  editData(editId: number) {
    console.log('In editData');
    console.log(editId);
    this.router.navigate(['/board-university/edit'], { state: { editId: editId } });
  }

//   deleteData(deleteId: number) {
// if (confirm('Are you sure you want to delete this data?')) {
//       this.dataService.deleteData(deleteId).subscribe({
//         next: (response) => {
//            if (response.type === 'warning') {
//             this.toastr.warning(response.message, 'Warning');
//           } else if (response.type === 'strongerror') {
//             this.toastr.error(response.message, 'Error');
//           } else {
//             this.toastr.success(response.message, 'Success');
//           }
//           this.loadList(); // Reload list
//         },
//         error: (error) => {
//           if (error.type === 'warning') {
//             this.toastr.warning(error.message, 'Warning');
//           } else if (error.type === 'strongerror') {
//             this.toastr.error(error.message, 'Error');
//           } else {
//             this.toastr.error('Delete failed');
//           }
//           //console.error('Delete error:', error);
//         },
//         complete: () => {
//         }
//       });
//     }
//   }
 // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.dataService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadList(); // Reload list
      },
      error: (err) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // ✅ show the modal
  }


  loadList() {
    // console.log('sort col - ' + this.sortColumn);
    // console.log('sort dir - ' + this.sortDirection);

    this.dataService.getList(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.dataList = response.listData;
            //console.log(this.dataList);
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
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
    console.log(`Navigating to page: ${p}`); // ✅ Debugging
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
    this.router.navigate(['hr/hrm/bu']);
  }
  activeLabel: string | null = null;


}
