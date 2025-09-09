import { Component, Input, Output, EventEmitter, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MemberService } from '../../../../../services/microfinance/member/member.service';

@Component({
  selector: 'app-member-otp-verify-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './member-otp-verify-modal.component.html',
  styleUrls: ['./member-otp-verify-modal.component.css']
})
export class MemberOtpVerifyModalComponent {
  @Input() memberId!: number;  
  @Input() contactNumber!: string;
  @Output() closed = new EventEmitter<boolean>(); // emit boolean for success/failure

  otp: string[] = ['', '', '', '']; // 4-digit OTP
  @ViewChildren('otpInput') otpInputRefs!: QueryList<ElementRef>;

  isVerified: boolean = false;
  isSubmitting: boolean = false; // prevent multiple clicks

  constructor(
   private memberservice: MemberService,
    private toastr: ToastrService
  ) {}

  close(success: boolean = false) {
    this.closed.emit(success); // emit true if OTP verified
  }

  onKey(event: KeyboardEvent, index: number) {
    if (event.key >= '0' && event.key <= '9') {
      this.otp[index] = event.key;
      if (index < this.otp.length - 1) {
        setTimeout(() => this.otpInputRefs.toArray()[index + 1].nativeElement.focus(), 0);
      }
      event.preventDefault();
    } else if (event.key === 'Backspace') {
      this.otp[index] = '';
      if (index > 0) {
        setTimeout(() => this.otpInputRefs.toArray()[index - 1].nativeElement.focus(), 0);
      }
      event.preventDefault();
    } else {
      event.preventDefault();
    }
  }

  verifyOtp() {
    if (this.isSubmitting) return; // prevent multiple clicks
    const otpCode = this.otp.join('');
    if (otpCode.length !== 4) {
      this.toastr.warning('Please enter complete 4-digit OTP', 'Warning');
      return;
    }

    this.isSubmitting = true;
    this.memberservice.updateVerifyMemberOtp(this.memberId, otpCode).subscribe({
      next: (res: any) => {
        const msg = res?.message || 'OTP verified';
        this.toastr.success(msg, 'Success');
        if (msg?.toLowerCase().includes('success')) {
          this.isVerified = true;
          this.closed.emit(true); // ✅ List component gets true
          this.isSubmitting = false;
        }
      },
      error: (err: any) => {
        const msg = err?.error?.message || err?.error || 'OTP verification failed';
        this.toastr.error(msg, 'Error');
        this.isSubmitting = false;
      }
    });
  }
}
