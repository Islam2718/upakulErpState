import { Component, OnDestroy, OnInit, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormsModule, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { GroupService } from '../../../../services/microfinance/group/group.service'; // Adjust path if needed
import { CommonModule, formatDate } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GroupModel } from '../../../../models/microfinance/group/groupModel';
// At the top of country-list.component.ts
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
declare var bootstrap: any;
import { firstValueFrom } from 'rxjs';
import { Modal } from 'bootstrap';
import { BtnService } from '../../../../services/btn-service/btn-service';


interface DropdownValues {
  text: string;
  value: string;
  selected: boolean;
}

interface GroupCommitteeDatasValue {
  committeePositionId: number;
  memberId: number;
  groupId: number | null;
  startDate: string;
}

interface GroupCommitteeAllDropdown {
  committee: DropdownValues[];
  members: DropdownValues[];
  groupCommitteeDatas: GroupCommitteeDatasValue[];
}



@Component({
  selector: 'app-group-list',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule, ConfirmModalComponent],
  standalone: true,  // ✅ Add this
  templateUrl: './group-list.component.html',
  styleUrl: './group-list.component.css'
})
export class GroupListComponent implements OnInit, OnDestroy, AfterViewInit {
  qry: string | null = null;
  samities: GroupModel[] = [];
  groupCommitteeForm !: FormGroup;
  groupCommitteeDatas: any[] = [];

  transDateData: string = '';

  isSubmitting = false;
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'GroupId';
  sortDirection = 'asc';
  sortOrder: string = 'GroupId asc';
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  isPermitted: boolean = false;

  dropdownMemberOptions: DropdownValues[] = [];
  dropdownCommitteeOptions: DropdownValues[] = [];


  @ViewChild('groupCommitteeModal') groupCommitteeModal!: ElementRef;
  @ViewChild('closeBtn') closeBtn!: ElementRef;

  modalInstance: any;

  ngAfterViewInit() {
    this.modalInstance = new Modal(this.groupCommitteeModal.nativeElement);
  }

  openModal() {
    this.modalInstance.show();
  }

  closeModal() {
    //console.log("closeModal");
    this.groupCommitteeForm.reset();
    this.modalInstance.hide(); // ✅ closes the modal
  }

  groupCommitteeAllDropdown: any = {
    members: [],
    committee: [],
    groupCommitteeDatas: []
  };

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder, // FormBuilder to easily create the form group
    public router: Router,
    private http: HttpClient,
    private apiService: GroupService,
    private toastr: ToastrService) {
    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });

    this.groupCommitteeForm = this.fb.group({
      committees: this.fb.array([])
    });

  }

  ngOnInit() {
    const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }

    const transDate = localStorage.getItem('transactionDate');
    if (transDate) {
      this.transDateData = transDate
    }
    this.loadGrid();
    // Listen for value changes on the searchTerm control
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // this.totalPagesArray()
    // Listen for pageSize changes
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      this.loadGrid();
    });

  }

  loadGrid() {
    this.apiService
      .getGroups(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          // console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.samities = response.listData;
            //console.log("LIST HERE",this.samities);
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

  editData(editId: number) {
    this.router.navigate(['/group/edit'], { state: { editId: editId } });
  }

  // Confirmation modal for delete
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;

  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;

    this.apiService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response: { type: string; message: string | undefined; }) => {
        if (response.type === 'warning') {
          this.toastr.warning(response.message, 'Warning');
        } else if (response.type === 'strongerror') {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
        this.loadGrid(); // Reload list
      },
      error: (err: any) => { this.toastr.error('Delete failed'); }
    });
    this.deleteIdToConfirm = null; // reset
  }

  delete(id: number) {
    this.deleteIdToConfirm = id;
    this.confirmModal.show(); // show the modal
  }

  setModalFunc(groupId: number) {
    this.initForm();
    this.loadAllDropdown(groupId);
  }

  initForm() {
    this.groupCommitteeForm = this.fb.group({
      committees: this.fb.array([])
    });

  }

  get committees(): FormArray {
    return this.groupCommitteeForm.get('committees') as FormArray;
  }
  
  // Custom Validator for duplicate members
duplicateMemberValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  if (!(control instanceof FormArray)) return null;

  const selectedMembers = control.controls
    .map(c => c.get('memberId')?.value)
    .filter(v => v);

  const duplicates = selectedMembers.filter(
    (id, index) => selectedMembers.indexOf(id) !== index
  );

  return duplicates.length > 0 ? { duplicateMember: true } : null;
};

isMemberDisabled(memberValue: any, index: number): boolean {
  const committees = this.groupCommitteeForm.get('committees') as FormArray;
  // collect all selected memberIds except the current index
  const selected = committees.controls
    .map((ctrl, i) => i !== index ? ctrl.get('memberId')?.value : null)
    .filter(v => v);
  return selected.includes(memberValue);
}

  //Load Dropdown
  async loadAllDropdown(groupId: number) {
    //console.log("in loadAllDropdown");
    try {
      const response = await firstValueFrom(this.apiService.groupCommitteeAllDropdown(groupId));
      this.groupCommitteeAllDropdown = response || [];
      this.dropdownMemberOptions = this.groupCommitteeAllDropdown.members;
      this.dropdownCommitteeOptions = this.groupCommitteeAllDropdown.committee;
      this.groupCommitteeDatas = this.groupCommitteeAllDropdown.groupCommitteeDatas;

     // console.log("this.groupCommitteeDatas", this.groupCommitteeDatas);
      this.dropdownCommitteeOptions.forEach(c => {
        const existing = this.groupCommitteeDatas.find(gc => gc.committeePositionId == Number(c.value));
        if (existing) { /*Data found*/
          const st_dt = existing ? formatDate(existing.startDate, 'dd-MMM-yyyy', 'en-US') : this.transDateData
          //Set Modal Formarray.. 
          this.committees.push(
            this.fb.group({
              groupId: groupId,
              committeePositionId: [c.value],
              memberId: existing.memberId,
              startDate: st_dt
            })
          );

        }
        else { /*Data not found*/
          this.committees.push(
            this.fb.group({
              groupId: groupId,
              committeePositionId: [c.value],
              memberId: null,
              startDate: this.transDateData
            })
          );
        }
      })
    } catch (error) {
      console.error('Failed to load dropdown data', error);
    }
  }

  private normalizeNull(value: any) {
    return (value === "null" || value === undefined) ? null : value;
  }

  onSubmitGroupCommittee() {
    //debugger
      if (this.groupCommitteeForm.invalid) {
        return;
      }

      const groupCommitteeFormData = this.groupCommitteeForm.value.committees;
      //console.log('groupCommitteeFormData:', groupCommitteeFormData);
      
      const cleanedData = groupCommitteeFormData.map((d: any) => ({
        ...d,
        memberId: d.memberId === "null" ? null : d.memberId
      }));

      //console.log("cleanedData", cleanedData);
      
      const memberIds = cleanedData
        .map((d: any) => d.memberId)
        .filter((id: any) => id !== null)
        .map((id: any) => Number(id)); // normalize to number

      const seen = new Set<number>();
      const duplicates = memberIds.filter((id: number) => {
        if (seen.has(id)) return true;
        seen.add(id);
        return false;
      });

      if (duplicates.length > 0) {
        this.toastr.error(
          `Duplicate members found... A member cant have multiple position.`,  //Duplicate members found: ${[...new Set(duplicates)].join(", ")}
          'Validation Error'
        );
        return; // stop submit   
      }

      this.apiService.saveCommittee(cleanedData).subscribe({
        next: (res) => {
          this.groupCommitteeForm.reset();
          this.closeBtn.nativeElement.click();
          this.toastr.success(res.message, 'Success');
        },
        error: (err) => {
          this.groupCommitteeForm.reset();
          console.error('Error saving:', err);
        }
      });
    
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }

  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      this.loadGrid();
    }
  }

  sortData(column: string) {
    // console.log("Sorting by:", column); // Debug log
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    this.loadGrid();
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
    this.loadGrid();
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    this.loadGrid();
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
    this.router.navigate(['mf/group/group']);
  }

  activeLabel: string | null = null;
}