import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MemberService } from '../../../../services/microfinance/member/member.service';
import { Member } from '../../../../models/microfinance/member/member';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormControl, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { ConfirmModalComponent } from '../../../../shared/confirm-modal/confirm-modal.component';
import { ConfigService } from '../../../../core/config.service';
import { ImageurlMappingConstant } from '../../../../shared/image-url-mapping-constant';
import { MemberConfirmDrawerComponent } from "../components/member-confirm-drawer/member-confirm-drawer.component";
import { MemberViewModalComponent } from "../components/member-view-modal/member-view-modal.component";
import { MemberOtpVerifyModalComponent } from "../components/member-otp-verify-modal/member-otp-verify-modal.component";
import { MemberMigrateModalComponent } from "../components/member-migrate-modal/member-migrate-modal";
import { MemberViewModal } from '../../../../models/microfinance/member/member-view-modal/member-view-modal';
import { BtnService } from '../../../../services/btn-service/btn-service';
import { EncryptionService } from '../../../../services/generic/encryption.service';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ConfirmModalComponent,
    MemberConfirmDrawerComponent,
    MemberViewModalComponent,
    MemberOtpVerifyModalComponent,
    MemberMigrateModalComponent
  ],
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit, OnDestroy {
  qry: string | null = null;
  isSubmitting = false;
  memImgURL: string = '';
  dataList: Member[] = [];
  page = 1;
  pageSize = 15;
  totalPages = 1;
  totalRecords = 0;
  sortColumn = 'MemberId';
  sortDirection = 'asc';
  sortOrder: string = 'MemberId asc';
  private unsubscribe$ = new Subject<void>();
  searchForm: FormGroup;
  pageSizeControl = new FormControl(this.pageSize);
  isPermitted: boolean = false;
  approveMode = false;
  isApproveFromList = false;
  private domain_url_mf: string;

  // ---------- Modals ----------
  showViewModal = false;
  selectedMember!: number;
  showOtpModal = false;
  selectedContact: string = '';
  currentOtpMember: Member | null = null;


  viewMode: 'view' | 'approve' = 'view'; // <-- 'view' or 'approve'


  showMigrateModal = false;
  selectedMemberId!: number;
  selectedMemberName!: string;

  selectedData!: MemberViewModal | null; // ✅


  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  @ViewChild('approveConfirmModal') approveConfirmModal!: ConfirmModalComponent;
  private deleteIdToConfirm: number | null = null;
  private approveIdToConfirm: number | null = null;

  constructor(
    public Button: BtnService,
    // private route: ActivatedRoute
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private http: HttpClient,
    private apiService: MemberService,
    private configService: ConfigService,
    private toastr: ToastrService

  ) {
    this.searchForm = this.fb.group({
      searchTerm: ['']
    });
    this.domain_url_mf = configService.mfApiBaseUrl();

    this.route.queryParams.subscribe(params => {
      const encrypted = params['qry'];
      if (encrypted) {
        try {
          const decoded = decodeURIComponent(encrypted);
          const decryptedJson = this.encryptionService.decryptUrlParm(decoded);
          const permissions = JSON.parse(decryptedJson); // now a real object
          //permissions.isAdd
          //console.log(permissions.isView); // true / false
        } catch {
        }
      }
    });
  }

  // ---------- VIEW MODAL ----------
  // openViewModal(member: Member, approveClick: boolean = false) {
  //   this.selectedMember = member.memberId;
  //   this.showViewModal = true;
  //   this.viewMode = approveClick ? 'approve' : 'view';
  //   this.approveMode = approveClick; 

  // }

  // openApproveModal(member: Member) {
  //   this.selectedMember = member.memberId;
  //   this.showViewModal = true;
  //   this.viewMode = 'approve';
  //   this.approveMode = true;
  // }
  openViewModal(member: Member, approveClick: boolean = false) {
    this.selectedMember = member.memberId;
    this.showViewModal = true;
    this.viewMode = approveClick ? 'approve' : 'view';
    this.approveMode = approveClick;
  }

  approve(memberId: number) {
    this.approveIdToConfirm = memberId;
    this.showViewModal = false;
    this.approveConfirmModal.show();
  }


  onApproveConfirmed() {
    if (!this.approveIdToConfirm) return;

    this.apiService.approvedMember(this.approveIdToConfirm, true).subscribe({
      next: () => {
        this.toastr.success('Member approved successfully');
        // update list
        this.dataList = this.dataList.map(m =>
          m.memberId === this.approveIdToConfirm ? { ...m, isApproved: true } : m
        );
        this.approveIdToConfirm = null;
        this.closeViewModal(); // close modal if you want
      },
      error: () => this.toastr.error('Approval failed!')
    });
  }


  closeViewModal() {
    this.showViewModal = false;
    this.selectedMember = 0;
  }
  // ---------- OTP MODAL ----------
  openOtpModal(member: Member) {
    this.selectedMember = member.memberId;
    this.selectedContact = member.mobileNumber ?? '';
    this.showOtpModal = true;
    this.currentOtpMember = member;
  }
  closeOtpModal(isVerified: boolean = false) {
    if (isVerified && this.currentOtpMember) {
      this.dataList = this.dataList.map(m =>
        m.memberId === this.currentOtpMember!.memberId
          ? { ...m, isOtpVerified: true }
          : m
      );
    }
    this.showOtpModal = false;
    this.selectedMember = 0;
    this.currentOtpMember = null;
  }

  // ---------- MIGRATE MODAL ----------
  openMigrateModal(member: Member) {
    this.selectedMemberId = member.memberId;
    this.selectedMemberName = member.memberName;
    this.showMigrateModal = true;
  }
  handleMigrateClosed(success: boolean) {
    if (success) {
      this.dataList = this.dataList.map(m =>
        m.memberId === this.selectedMemberId
          ? { ...m, isMigrated: true }
          : m
      );
    }
    this.showMigrateModal = false;
    this.selectedMemberId = 0;
    this.selectedMemberName = '';
  }

  ngOnInit() {
    const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }

    this.memImgURL = this.domain_url_mf.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL;

    this.loadList();

    this.searchForm.get('searchTerm')?.valueChanges.pipe(takeUntil(this.unsubscribe$))
      .subscribe(searchTerm => this.onSearchChange(searchTerm));

    this.pageSizeControl.valueChanges.pipe(takeUntil(this.unsubscribe$))
      .subscribe(newSize => {
        this.pageSize = newSize as number;
        this.page = 1;
        this.loadList();
      });
  }

  get selectedMemberData(): Member | undefined {
    return this.dataList.find(m => m.memberId === this.selectedMember);
  }

  // ---------- CRUD ----------
  editData(memberId: number | null) {
    this.router.navigate(['mf/microfinance/member-setup'], { state: { memberId: memberId } });
  }

  delete(memberId: number) {
    this.deleteIdToConfirm = memberId;
    this.confirmModal.show();
  }
  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;
    this.apiService.deleteData(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning') this.toastr.warning(response.message, 'Warning');
        else if (response.type === 'strongerror') this.toastr.error(response.message, 'Error');
        else this.toastr.success(response.message, 'Success');
        this.loadList();
      },
      error: () => this.toastr.error('Delete failed')
    });
    this.deleteIdToConfirm = null;
  }

  loadList() {
    this.apiService.getList(
      this.page,
      this.pageSize,
      this.searchForm.get('searchTerm')?.value,
      this.sortOrder
    )
      .pipe(
        tap(response => {
          if (response && response.totalRecords !== undefined) {
            this.dataList = (response.listData ?? []).map(m => ({
              ...m,
              isOtpVerified: m.isCheckedInContactNo ?? false
            }));
            this.totalPages = Math.ceil(response.totalRecords / this.pageSize);
            this.totalRecords = response.totalRecords;
          }
        }),
        takeUntil(this.unsubscribe$)
      )
      .subscribe();
  }

  onSearchChange(searchTerm: string) {
    if (searchTerm.length > 1 || searchTerm.length === 0) {
      this.page = 1;
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
    if (this.totalPages <= 6) return Array.from({ length: this.totalPages }, (_, i) => i + 1);

    pages.push(1);
    if (this.page > 3) pages.push('...');
    for (let i = this.page - 1; i <= this.page + 1; i++) if (i > 1 && i < this.totalPages) pages.push(i);
    if (this.page < this.totalPages - 2) pages.push('...');
    pages.push(this.totalPages);
    return pages;
  }

  changePage(p: number | string): void {
    if (typeof p === 'string') return;
    if (p < 1 || p > this.totalPages) return;
    this.page = p;
    this.loadList();
  }

  changePageSize(newSize: number | null): void {

    this.pageSize = newSize ?? 10;

    this.pageSize = newSize ?? 15; // Default to 10 if newSize is null // Reset to first page when pageSize changes

    this.loadList();
  }

  get dataRangeLabel(): string {
    if (this.totalRecords === 0) return 'No records found';
    const startIndex = (this.page - 1) * this.pageSize + 1;
    let endIndex = this.page * this.pageSize;
    if (endIndex > this.totalRecords) endIndex = this.totalRecords;
    return `Showing ${startIndex} - ${endIndex} of ${this.totalRecords} records`;
  }

  openDrawer(memberData: any) {
    this.selectedData = memberData;
  }

  navigateToCreate() {
    this.router.navigate(['mf/microfinance/member-setup']);
  }
  handleApprovedEvent(memberId: number) {
    this.dataList = this.dataList.map(m =>
      m.memberId === memberId ? { ...m, isApproved: true } : m
    );
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
