import { Component, ViewChild, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { DateDiffPipe } from '../../../../shared/utility/date-diff.pipe';
import { ToastrService } from 'ngx-toastr';
import { GraceScheduleService } from '../../../../services/microfinance/GraceSchedule/graceschedule.service';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { GraceSchedule } from '../../../../models/microfinance/GraceSchedule/graceschedule.model';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-graceschedule-list',
  imports: [CommonModule, ReactiveFormsModule, ConfirmModalComponent, DateDiffPipe],
  standalone: true,
  templateUrl: './graceschedule-list.component.html',
  styleUrl: './graceschedule-list.component.css'
})
export class GraceScheduleListComponent implements OnInit, OnDestroy {
  qry: string | null = null;
  graceschedules: GraceSchedule[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'Id';
  sortDirection = 'asc';
  sortOrder: string = 'Id asc';

  private unsubscribe$ = new Subject<void>();

  searchForm: FormGroup;
  pageSizeControl = new FormControl(this.pageSize);
  activeLabel: string | null = null;
  isPermitted: boolean = false;
  // Confirmation modal text properties
  confirmModalTitle = '';
  confirmModalMessage = '';

  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  @ViewChild('approveModal') approveModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;
  private approvedIdToConfirm: number | null = null;

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private router: Router,
    private gracescheduleService: GraceScheduleService,
    private toastr: ToastrService
  ) {
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
  }

  ngOnInit() {
        const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }
    this.loadGraceSchedules();

    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });

    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1;
      this.loadGraceSchedules();
    });
  }

  loadGraceSchedules() {
    this.gracescheduleService
      .getGraceSchedules(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
                if (response && response.totalRecords !== undefined) {
            this.graceschedules = response.listData;
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
             this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
          } else {
            console.error('Unexpected API response format:', response);
          }
        }),
        takeUntil(this.unsubscribe$)
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  onSearchChange(searchTerm: string) {
    this.page = 1;
    this.loadGraceSchedules();
  }

  sortData(column: string) {
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.loadGraceSchedules();
  }

  get totalPagesArray(): (number | string)[] {
    const pages: (number | string)[] = [];

    if (this.totalPages <= 6) {
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    pages.push(1);

    if (this.page > 3) {
      pages.push('...');
    }

    for (let i = this.page - 1; i <= this.page + 1; i++) {
      if (i > 1 && i < this.totalPages) {
        pages.push(i);
      }
    }

    if (this.page < this.totalPages - 2) {
      pages.push('...');
    }

    pages.push(this.totalPages);

    return pages;
  }

  changePage(p: number | string): void {
    if (typeof p === 'string') return;
    if (p < 1 || p > this.totalPages) return;
    this.page = p;
    this.loadGraceSchedules();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10;
    this.loadGraceSchedules();
  }

  get dataRangeLabel(): string {
    if (this.totalRecords === 0) {
      return 'No records found';
    }

    const startIndex = (this.page - 1) * this.pageSize + 1;
    let endIndex = this.page * this.pageSize;

    if (endIndex > this.totalRecords) {
      endIndex = this.totalRecords;
    }

    return `Showing ${startIndex} - ${endIndex} of ${this.totalRecords} records`;
  }

  editGraceSchedule(Id: number) {
    this.router.navigate(['graceschedule-form/edit'], { state: { id: Id } });
  }
  approved(id: number) {
    this.approvedIdToConfirm = id;
    this.approveModal.show(); // Open approve modal
  }
  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.gracescheduleService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadGraceSchedules();
      },
      error: () => {
        this.toastr.error('Delete failed');
      }
    });

    this.deleteIdToConfirm = null;
  }
  // Show modal for delete confirmation
  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show();
  }

  onApprovedConfirmed() {
    if (this.approvedIdToConfirm === null) return;

    this.gracescheduleService.approveData(this.approvedIdToConfirm).subscribe({
      next: (response: any) => {
        if (response.statusCode !== 200) {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadGraceSchedules();
      },
      error: () => {
        this.toastr.error('Approval failed');
      }
    });

    this.approvedIdToConfirm = null;
  }
  navigateToCreate() {
    this.router.navigate(['mf/graceschedule/graceschedule-form']);
  }
}
