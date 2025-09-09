import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MemberService } from '../../../../../services/microfinance/member/member.service';
import { MemberViewModal } from '../../../../../models/microfinance/member/member-view-modal/member-view-modal';
import { ConfigService } from '../../../../../core/config.service';
import { ImageurlMappingConstant } from '../../../../../shared/image-url-mapping-constant';
import { BtnService } from '../../../../../services/btn-service/btn-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-view-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './member-view-modal.component.html',
  styleUrls: ['./member-view-modal.component.css'],
})
export class MemberViewModalComponent implements OnInit, OnChanges {
  @Input() memberId!: number;  
  @Input() isApproveFromList = false; 
  @Input() viewMode: 'view' | 'approve' = 'view';
  @Output() requestApproveConfirm = new EventEmitter<number>();
    @Input() approveMode: boolean = false;
  @Output() closeModal = new EventEmitter<void>();
  @Output() approvedEvent = new EventEmitter<number>();

  memImgURL: string = '';
  memberData!: MemberViewModal;       
  private domain_url_mf: string;


  constructor(
    public Button: BtnService,
    private memberService: MemberService,
    private configService: ConfigService,
    private toastr: ToastrService
  ) {
    this.domain_url_mf = configService.mfApiBaseUrl();
  }

  ngOnInit(): void {
    this.memImgURL = this.domain_url_mf.replace("/api/v1", "") + ImageurlMappingConstant.IMG_BASE_URL;
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['memberId'] && this.memberId) {
      this.fetchMemberData();
    }
  }

  fetchMemberData() {
    this.memberService.getMemberDetailById(this.memberId).subscribe({
      next: (res: any) => {
        if (res) {
          this.memberData = { ...res, isApproved: res.isApproved ?? false }; // Add isApproved flag
        } else {
          console.error('Member data not found', res);
        }
      },
      error: (err) => console.error('Error fetching member details:', err),
    });
  }
  
confirmApprove(memberId: number) {
  console.log('Approve clicked in modal', memberId); // <-- debug
  this.requestApproveConfirm.emit(memberId);
}
// confirmApprove(memberId: number) {
//   // 1️⃣ show confirm dialog
//   const confirmed = confirm('Are you sure you want to approve this member?');
//   if (!confirmed) return;

//   // 2️⃣ API call
//   this.memberService.approvedMember(memberId, true).subscribe({
//     next: () => {
//       this.toastr.success('Member approved successfully');
//       this.memberData.isApproved = true; // modal update
//       this.approvedEvent.emit(memberId); // list update
//       this.close(); // modal close
//     },
//     error: () => {
//       this.toastr.error('Approval failed!');
//     }
//   });
// }


  close() {
    this.closeModal.emit();
  }


  printDiv() {
    const printContents = document.querySelector('.printablearea')?.outerHTML;
    if (!printContents) return;

    const popupWin = window.open('', '_blank', 'top=0,left=0,height=auto,width=auto');
    if (!popupWin) return;

    const styles = Array.from(document.querySelectorAll('link[rel="stylesheet"], style'))
      .map(el => el.outerHTML)
      .join('');

    const componentStyles = `
      body { margin:0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
      .coast-a4 { width:210mm; min-height:297mm; padding:15mm 18mm; background:#fff; color:#222; font-size:11px; line-height:1.4; border:1px solid #ccc; border-radius:6px; box-shadow:0 3px 12px rgba(0,0,0,0.1);}
      table { border-collapse: collapse; width:100%; }
      th, td { border:1px solid #ccc; padding:3mm 4px; }
      .photo-box img { width:100%; height:100%; object-fit:cover; }
      .uline { border-bottom:1px solid #444; display:inline-block; padding-bottom:2px; }
      .box-field { border:1px solid #bbb; padding:0 4px; min-height:12px; line-height:1.3; border-radius:3px; box-shadow: inset 0 1px 2px rgba(0,0,0,0.05); }
      .section-title { font-weight:700; text-decoration:underline; color:#34495e; margin-top:4mm; display:block; }
      .sign-row { display:flex; gap:20mm; justify-content:space-between; margin-top:10mm; }
      .sig-line { border-bottom:1px solid #444; height:12mm; border-radius:2px; }
      .sig-cap { text-align:center; font-size:10.5px; margin-top:1mm; color:#555; }
      .t-center { text-align:center; }
      .t-right { text-align:right; }
      @media print { @page { size:A4; margin:10mm; } }
    `;

    popupWin.document.write(`
      <html>
        <head>
          <title>Print Preview</title>
          ${styles}
          <style>${componentStyles}</style>
        </head>
        <body onload="window.print();" onafterprint="window.close();">
          ${printContents}
        </body>
      </html>
    `);

    popupWin.document.close();
  }
}
