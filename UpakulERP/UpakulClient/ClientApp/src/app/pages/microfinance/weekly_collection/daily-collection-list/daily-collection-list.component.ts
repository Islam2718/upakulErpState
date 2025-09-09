import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DailyCollectionService } from '../../../../services/microfinance/daily_collection/daily-collection.service'; // Adjust path if needed
import { DailyCollection } from '../../../../models/microfinance/daily_collection/daily-collection';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-daily-collection-list',
  imports: [
    CommonModule, ReactiveFormsModule
  ],
  templateUrl: './daily-collection-list.component.html',
  styleUrl: './daily-collection-list.component.css'
})
export class DailyCollectionListComponent implements OnInit, OnDestroy{

  isSubmitting = false;
  listData: DailyCollection[] = [];
  rows: any[] = [];
  employeeGroupData: any[] = [];
  
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'Id';
  sortDirection = 'asc';
  sortOrder: string = 'Id asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // ✅ Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  
  
  constructor(private fb: FormBuilder, // FormBuilder to easily create the form group
  public router: Router, 
  private http: HttpClient, 
  private apiService: DailyCollectionService,
  private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }

ngOnInit() {
   // this.loadData();  
   this.loadEmployeeGroupData();      
  }

  
  loadEmployeeGroupData(): void {
    this.apiService.getEmployeeXGroupSheet().subscribe({
      next: (res) => {
        console.log('API Response:', res);
        this.employeeGroupData = res;  // bind data to table/dropdown
      },
      error: (err) => {
        console.error('Error fetching employee group data:', err);
      }
    });
  }


  loadData(){

  }

  groupXMemberData(groupId: number) {
    this.router.navigate(['dailycollection-form/edit'], { state: { gId: groupId } });
  }



    //// Add Iregular Collection
    addRow(event: Event) {
      alert("AddRow");
          event.preventDefault(); // prevents page reload from <a href="#">
          this.rows.push({
            col1: '00116 - Shurobi',
            col2: '19 - Md.Faroque',
            col3: 'Wednesday',
            col4: '3',
            col5: '3 (0)',
            col6: '3 (0)',
            col7: '0(0)',
            col8: '>>',
            col9: '',
            col10: 'Refresh',
            col11: '',
            col12: 'Pending'
          });

          console.log(this.rows);
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
    this.router.navigate(['mf/dailycollection/dailycollection-form']);
  }

  // onReset(): void {
  //   this.dataForm.reset();
  // }
}
