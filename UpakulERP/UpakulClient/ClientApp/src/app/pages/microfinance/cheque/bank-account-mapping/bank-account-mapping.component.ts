import { Component, OnDestroy, OnInit, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { BankAccountMappingService } from '../../../../services/microfinance/cheque/bank-account-mapping.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BankAccountMapping } from '../../../../models/microfinance/cheque/bank-account-mapping';
import { BankAccountChequeIDetails } from '../../../../models/microfinance/cheque/bank-account-cheque-details';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { firstValueFrom } from 'rxjs';


declare var bootstrap: any;

interface DropdownValues {
  text: string;
  value: string;
  selected: boolean;
}

interface GetOfficeBankAssignDropdownData {
  bank: DropdownValues[];
  accountHead: DropdownValues[];
}



@Component({
  selector: 'app-bank-account-mapping',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl:'./bank-account-mapping.component.html',
  styleUrl: './bank-account-mapping.component.css'
})
export class BankAccountMappingComponent implements OnInit, OnDestroy, AfterViewInit {
  
  dataForm: FormGroup;
  chequeForm: FormGroup;

  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  button = Button;
  // officeTypeId: number | null = null;
  // officeType: string | null = null;
  GLOBAL_MAPPING_ID : number | undefined;
  //officeList: DropdownValues[] = [];
  listData: BankAccountMapping[] = [];
  listChequeData: BankAccountChequeIDetails[] = [];
  bankList: DropdownValues[] = [];
  branchList: DropdownValues[] = [];  
  accountHeadList: DropdownValues[] = [];

  isPermitted: boolean = false;

  //Page And list
  page = 1;
  pageSize = 10;
  totalPages = 1;
  totalRecords = 0;
  searchTerm = '';
  sortColumn = 'BankAccountMappingId';
  sortDirection = 'asc';
  sortOrder: string = 'BankAccountMappingId asc'
  private unsubscribe$ = new Subject<void>(); // For cleanup
  searchForm: FormGroup;
  // Form Control for page size selection
  pageSizeControl = new FormControl(this.pageSize);
  //BankBranchId: any;

  //Close Modal After create cheque book
  @ViewChild('exampleModal') modalElement!: ElementRef;
  modalInstance: any;

  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.modalElement.nativeElement);
  }

  closeModal() {
    this.chequeForm.reset();
    this.modalInstance.hide(); // ✅ closes the modal
  }
  
  getOfficeBankAssignDropdownData: any = {
    bank: [],
    accountHead: []
  };

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private toastr: ToastrService,
    private apiService: BankAccountMappingService) {

    // Initialize searchForm with searchTerm FormControl
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });

    this.dataForm = this.fb.group({
      BankAccountMappingId: ['', Validators.required],
      OfficeId: ['', Validators.required],
      BankId: ['', Validators.required],
      BranchName: ['', Validators.required],
      RoutingNo: ['', Validators.required],
      BranchAddress: ['', Validators.required],
      BankAccountName: ['', Validators.required],
      BankAccountNumber: ['', Validators.required],
      AccountId: ['', Validators.required]
    });

    this.chequeForm = this.fb.group({
      AccountNumber1: [{ value: '', disabled: true }, Validators.required],
      BankAccountMappingId:[null],
      ChequeNumberPrefix:['', Validators.required],
      ChequeNumberFrom: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
      ChequeNumberTo: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
    }, {
        validators: this.fromLessThanToValidator()
    });
  }

  ngOnInit() {
    const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = (parsed.office_type_id === 6 || parsed.office_type_id === 1);
    }

    const transDate = localStorage.getItem('transactionDate');
    if (transDate) {
      //this.transDateData = transDate
    }


    
    this.dataForm = this.fb.group({
      BankAccountMappingId: [null],
      OfficeId: [null],
      BankId: [null],
      BranchName: [null],
      RoutingNo: [null],
      BranchAddress: [null],
      BankAccountName: [null],
      BankAccountNumber: [null],
      AccountId: [null]
    });

    this.chequeForm = this.fb.group({
      AccountNumber1:'',
      BankAccountMappingId:[null],
      ChequeNumberPrefix:[''],
      ChequeNumberFrom: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
      ChequeNumberTo: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
    }, {
        validators: this.fromLessThanToValidator()
    });

    this.loadData();
    this.loadAllDropdown();
   
    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(searchTerm => {
      this.onSearchChange(searchTerm);
    });
    // this.totalPagesArray()
    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(newSize => {
      this.pageSize = newSize as number;
      this.page = 1; // Reset to first page
      //this.loadData();
      //this.setGridModalFunc();
    });
  }

//Load Dropdown
  async loadAllDropdown() {
    //console.log("in loadAllDropdown");
    try {
      const response = await firstValueFrom(this.apiService.officeBankAssignDropdown());
      this.getOfficeBankAssignDropdownData = response || [];
      this.bankList = this.getOfficeBankAssignDropdownData.bank;
      this.accountHeadList = this.getOfficeBankAssignDropdownData.accountHead;  
      
      console.log("this.accountHeadList", this.accountHeadList);
      
    } catch (error) {
      console.error('Failed to load dropdown data', error);
    }
  }


  editData(editId: number) {
   //  console.log('In edit');
   //  console.log(editId);
     this.onReset();
     this.isEditMode = true;
      this.apiService.getDataById(editId).subscribe(res => {
        const dataObj = res ?? res;
      //  console.log('---', dataObj);
        if (dataObj) {
          //this.loadBankBranchDropDown(dataObj.bankId);
          this.dataForm.patchValue({
            BankAccountMappingId: dataObj.bankAccountMappingId,
            OfficeId: dataObj.officeId,
            BankId: dataObj.bankId,
            BranchName: dataObj.branchName,
            RoutingNo: dataObj.routingNo,
            BranchAddress: dataObj.branchAddress,
            BankAccountName: dataObj.bankAccountName,
            BankAccountNumber: dataObj.bankAccountNumber,
            AccountId: dataObj.accountId
          });
        }
      });


    //this.router.navigate(['/bank-account-mapping/edit'], { state: { editId: editId } });
  }


  deleteData(deleteId: number) {
    if (confirm('Are you sure you want to delete this data?')) {
      this.apiService.deleteData(deleteId).subscribe({
        next: (response) => {
          if (response.type === 'warning') {
            this.toastr.warning(response.message, 'Warning');
          } else if (response.type === 'strongerror') {
            this.toastr.error(response.message, 'Error');
          } else {
            this.toastr.success(response.message, 'Success');
           // console.log("Deleted!!");
          }
          this.loadData();   
        },
        error: (error) => {
          if (error.type === 'warning') {
            this.toastr.warning(error.message, 'Warning');
          } else if (error.type === 'strongerror') {
            this.toastr.error(error.message, 'Error');
          } else {
            this.toastr.error('Delete failed');
          }
          //console.error('Delete error:', error);
        },
        complete: () => {
        }
      });
    }
  }




  setBookModalFunc(data: any) {
   // console.log('_params_data', data);
    this.chequeForm.patchValue({
      AccountNumber1: data.accountNumber,
      BankAccountMappingId:data.bankAccountMappingId,
    });
  }


  setGridModalFunc(data: any) {
    //console.log('_params_data_grid', data);
    this.chequeForm.patchValue({
      AccountNumber1: data.accountNumber,
      BankAccountMappingId:data.bankAccountMappingId,
    });
    this.GLOBAL_MAPPING_ID = data.bankAccountMappingId;
    this.loadChequeDetailsData(this.GLOBAL_MAPPING_ID );
  }

fromLessThanToValidator(): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const from = group.get('ChequeNumberFrom')?.value;
    const to = group.get('ChequeNumberTo')?.value;

    if (!from || !to) return null; // skip if empty

    return parseInt(from, 10) < parseInt(to, 10)
      ? null
      : { fromGreaterThanTo: true };
  };
}



  onSubmit() {
    if (this.dataForm.valid) {
      this.isSubmitting = true;
     // console.log(this.dataForm.valid);
      if (this.isEditMode) {
       // console.log('in isEditMode');
        this.apiService.updateData(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success'); 
            //console.log('----- updateData'); 
            this.onReset();  
            this.loadData();         
          },
          error: (error) => {
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        console.log("this.dataForm.value", this.dataForm.value);
        this.apiService.addData(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
            this.loadData(); 
            this.onReset();  
            this.dataForm.reset();
          },

          error: (error) => {
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }
    }
  }

  onSubmitCheque(): void {
        if (this.chequeForm.invalid) {
            this.chequeForm.markAllAsTouched(); // to show validation errors
            return;
          }

      const formData = this.chequeForm.getRawValue();
      this.apiService.createCheque(formData).subscribe({
        next: (response) => {
           this.toastr.success(response.message || 'Cheque saved successfully', 'Success');
           this.closeModal(); 
           this.loadData();
           this.chequeForm.reset();
        },
        error: (error) => {
          this.toastr.error(error?.message || 'Error saving cheque', 'Error');
        }
      });

      //this.chequeForm.reset();
  }

  loadData() {
    this.apiService
      .getDataList(this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
         // console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.listData = response.listData;
           // console.log(this.listData);
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

  loadChequeDetailsData(mappingId: number = 0) {
    this.apiService
      .getChequeDetailsList(mappingId, this.page, this.pageSize, this.searchForm.get('searchTerm')?.value, this.sortOrder)
      .pipe(
        tap(response => {
          //console.log("API Response: ", response);
          if (response && response.totalRecords !== undefined) {
            this.listChequeData = response.listData;
           // console.log("_listChequeData",this.listChequeData);
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


  // showChequeStatus(status: string | undefined): string {
  //     if (!status) return 'Unknown';
      
  //     switch (status) {
  //       case 'U': return 'Used';
  //       case 'N': return 'Unused';
  //       case 'R': return 'Rejected';
  //       case 'D': return 'Deleted';
  //       default: return 'Unknown';
  //     }
  // }


  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete(); // Avoid memory leaks
  }

  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length == 0) {
      this.page = 1; // Reset to first page when search changes
      //this.loadData();
      this.loadChequeDetailsData(this.GLOBAL_MAPPING_ID );
    }
  }

  sortData(column: string) {    
    this.sortColumn = column;
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrder = column + " " + this.sortDirection;
    //this.loadData();
    this.loadChequeDetailsData(this.GLOBAL_MAPPING_ID );
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
    this.page = p;
    //this.loadData();
    this.loadChequeDetailsData(this.GLOBAL_MAPPING_ID );
  }

  changePageSize(newSize: number | null): void {
    this.pageSize = newSize ?? 10; // Default to 10 if newSize is null // Reset to first page when pageSize changes
    //this.loadData();
    this.loadChequeDetailsData(this.GLOBAL_MAPPING_ID );
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

  // navigateToList() {
  //   this.router.navigate(['mf/samity/samity-list']);
  // }

  onReset(): void {
    //this.bankList = [];
    //this.listData = [];
    //this.listChequeData=[];
    this.isSubmitting = false;
    this.successMessage = '';
    this.isEditMode = false;
    this.GLOBAL_MAPPING_ID = 0;
    this.branchList = [];
    this.chequeForm.reset();
    this.dataForm.reset();
  }
}
