import {
  Component, OnDestroy, OnInit, AfterViewInit, ElementRef, ViewChild
} from '@angular/core';
import {
  FormBuilder, FormGroup, FormArray, Validators, AbstractControl, ValidationErrors
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { DailyCollectionService } from '../../../../services/microfinance/daily_collection/daily-collection.service';
import { DailyCollection } from '../../../../models/microfinance/daily_collection/daily-collection';

declare var bootstrap: any;

interface GroupXMemberRow {
  employeeId: number;
  employeeName: string;
  groupName: string;
  memberCode?: string;
  memberName?: string;
  compulsoryAmount?: number;
  // add any other fields you render in the table
  CollectionAmount?: number;
}

@Component({
  selector: 'app-daily-collection-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './daily-collection-form.component.html',
  styleUrl: './daily-collection-form.component.css'
})
export class DailyCollectionFormComponent implements OnInit, OnDestroy, AfterViewInit {
  // Main grid form
  dataForm: FormGroup;
  // Offcanvas form
  collectionForm: FormGroup;

  // raw API list used to seed the grid
  getGroupXMemberData: GroupXMemberRow[] = [];

  isSubmitting = false;
  isEditMode = false;

  // holds the row we’re editing in the offcanvas
  private currentRowIndex: number | null = null;

  // Modal/offcanvas refs
  @ViewChild('exampleModal') modalElement!: ElementRef; // (kept if you still need the modal elsewhere)
  modalInstance: any;

  @ViewChild('offcanvasRight') offcanvasRef!: ElementRef;
  offcanvasInstance: any;

  constructor(
    private fb: FormBuilder,
    private apiService: DailyCollectionService,
    private toastr: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    // Build forms in constructor to have them ready for template binding
    this.dataForm = this.fb.group({
      employees: this.fb.array([])
    });

    this.collectionForm = this.fb.group({
      CollectionId: [null],
      CollectionAmount: [null, [Validators.min(0)]],
      SavingCom: [null, [Validators.min(0)]],
      SavingVol: [null, [Validators.min(0)]],
      SavingOth: [null, [Validators.min(0)]],
      RefundCom: [null, [Validators.min(0)]],
      RefundVol: [null, [Validators.min(0)]],
      RefundOth: [null, [Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    const id = history.state?.gId;
    if (id) {
      this.isEditMode = true;
      this.loadGroupMembers(id);
    }
  }

  ngAfterViewInit(): void {
    if (this.modalElement) {
      this.modalInstance = new bootstrap.Modal(this.modalElement.nativeElement);
    }
    if (this.offcanvasRef) {
      this.offcanvasInstance = new bootstrap.Offcanvas(this.offcanvasRef.nativeElement);
    }
  }

  ngOnDestroy(): void {
    // cleanup if needed
  }

  // -------------------
  // Form getters
  // -------------------
  get employees(): FormArray {
    return this.dataForm.get('employees') as FormArray;
  }

  // -------------------
  // Data loading
  // -------------------
  private loadGroupMembers(groupId: number): void {
    this.apiService.getGroupXMemberSheet(groupId).subscribe({
      next: (res: GroupXMemberRow[]) => {
        // Bind table data
        this.getGroupXMemberData = res || [];
        // Populate form array
        this.employees.clear();
        this.getGroupXMemberData.forEach((row) => {
          this.employees.push(this.createEmployeeRow(row));
        });
      },
      error: (err) => {
        console.error('Error fetching group members:', err);
        this.toastr.error('Failed to load members', 'Error');
      }
    });
  }

  private createEmployeeRow(row: GroupXMemberRow): FormGroup {
    return this.fb.group({
      employeeId: [row.employeeId],
      employeeName: [row.employeeName],
      groupName: [row.groupName],
      memberCode: [row.memberCode ?? ''],
      memberName: [row.memberName ?? ''],
      compulsoryAmount: [row.compulsoryAmount ?? 0],
      CollectionAmount: [row.CollectionAmount ?? 0, [Validators.min(0)]]
    });
  }

  // -------------------
  // Offcanvas handlers
  // -------------------
  openCollectionOffcanvas(rowData: any, rowIndex: number): void {
    this.currentRowIndex = rowIndex;

    // Patch any defaults you want to show in offcanvas:
    this.collectionForm.reset({
      CollectionId: rowData?.CollectionId ?? null,
      CollectionAmount: rowData?.CollectionAmount ?? 0,
      SavingCom: null,
      SavingVol: null,
      SavingOth: null,
      RefundCom: null,
      RefundVol: null,
      RefundOth: null
    });

    this.offcanvasInstance?.show();
  }

  closeOffcanvas(): void {
    this.offcanvasInstance?.hide();
  }

  // -------------------
  // Submissions
  // -------------------
  onSubmit(): void {
    if (this.dataForm.invalid) {
      this.toastr.warning('Please fix validation errors in the grid.');
      return;
    }

    const payload = this.dataForm.value.employees;
    // TODO: map to your backend contract if needed
    console.log('Grid submit payload:', payload);

    // Example save:
    // this.apiService.saveDailyCollection(payload).subscribe(...)
  }

  onSubmitCollection(): void {
    if (this.collectionForm.invalid || this.currentRowIndex === null) {
      this.toastr.warning('Please provide valid collection values.');
      return;
    }

    const values = this.collectionForm.value;

    const rowGroup = this.employees.at(this.currentRowIndex);
    rowGroup.patchValue({
      CollectionAmount: Number(values.CollectionAmount ?? 0)
    });

  
    this.toastr.success('Collection updated.');
    this.closeOffcanvas();
  }

  onlyNumberKey(event: KeyboardEvent): void {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
    }
  }

  navigateToList(): void {
    this.router.navigate(['mf/microfinance/dailycollection-list']);
  }

  onReset(): void {
    this.dataForm.reset();
    this.employees.clear();
  }
}