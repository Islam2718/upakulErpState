import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { takeUntil, tap } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../../services/administrator/user.service';
import { User } from '../../../../models/administration/user';
import { ToastrService } from 'ngx-toastr';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { Subject } from 'rxjs/internal/Subject';


@Component({
  selector: 'app-user-list',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ConfirmModalComponent
    //MatFormFieldModule,
    //MatInputModule,
    //MatButtonModule,
    //FormsModule
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  page = 1;
  pageSize = 20;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'Id';
  sortDirection = 'asc';
  sortOrder: string = 'Id asc';
  private unsubscribe$ = new Subject<void>();  // For cleanup
  searchForm: FormGroup;
  pageSizeControl = new FormControl(this.pageSize);

  constructor(private fb: FormBuilder,
    private apiService: UserService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  

  ) {
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });

  }


  ngOnInit(): void {

    this.loadList();
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1;
      this.loadList();
    });
  }

  loadList() {
    
    this.apiService.LoadList(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          if (response && response.totalRecords !== undefined) {
            this.users = response.listData;
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
  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }

  edit(editUserId: number, editEmployeeId: number): void {
    //console.log('In edit', editUserId, editEmployeeId);

    this.router.navigate(
      ['adm/user/employee-registration'],
      {
        state: {
          editUserId: editUserId,
          editEmployeeId: editEmployeeId
        }
      }
    );
  }
// Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.apiService.delete(this.deleteIdToConfirm).subscribe({
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


  // delete(deleteId: number) {
  //   if (confirm('Are you sure you want to delete this data?')) {
  //     this.apiService.delete(deleteId).subscribe({
  //       next: () => {
  //         this.loadList();
  //         this.router.navigate(['adm/user/user-list']);
  //       },
  //       error: err => console.error('Delete failed', err)
  //     });
  //   }
  // }

  resetUserPassword(userName: string) {
    this.apiService.resetUserPassword(userName).subscribe({
      next: (response) => {
        //console.log('Password reset successful', response);
        this.toastr.success(response.message, 'Success');
      },
      error: (error) => {
        //console.error('Password reset failed', error);
         this.toastr.warning("Password reset failed.", 'Warning');
      }
    });
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
    // console.log(`Navigating to page: ${p}`); // ✅ Debugging
    this.page = p;
    this.loadList();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadList();
  }
  
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
    this.router.navigate(['adm/user/employee-registration']);
  }

  activeLabel: string | null = null;

}
