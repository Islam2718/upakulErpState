import { Component, ElementRef, ViewChild, Input, OnInit  } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MemberService } from '../../../../../services/microfinance/member/member.service';
import { Member } from '../../../../../models/microfinance/member/member';
import { DefaultGlobalConfig, ToastrService } from 'ngx-toastr';

declare var bootstrap: any;

@Component({
  selector: 'app-member-form-review-modal',
  templateUrl: './member-form-review-modal.component.html',
})
export class MemberFormReviewModalComponent {
  @ViewChild('modal', { static: false }) modalRef!: ElementRef;
  // @Input() data!: FormGroup;
  @Input() data: any;
  router: any;

  constructor(
    private memberService: MemberService,
    private toastr: ToastrService,
  ) {}

  ngOnIt(){
    // alert('aaa giya...');
  }

  convertJsonToFormData(data: any, formData: FormData = new FormData(), parentKey: string | null = null): FormData {
    if (data && typeof data === 'object' && !(data instanceof File)) {
      Object.keys(data).forEach(key => {
        const fullKey = parentKey ? `${parentKey}[${key}]` : key;
        this.convertJsonToFormData(data[key], formData, fullKey);
      });
    } else {
      if (data !== null && data !== undefined) {
        formData.append(parentKey!, data);
      }
    }
    return formData;
  }

  jsonToFormData(data: any): FormData {
    const formData = new FormData();
    Object.keys(data).forEach((key) => {
      if (data[key] !== null && data[key] !== undefined) {
        formData.append(key, data[key]);
      }
    });
    return formData;
  }

  @Input() resetFn!: () => void;

  onSaveMemberForm() {
    const formData = this.jsonToFormData(this.data);
    this.memberService.createMember(formData).subscribe({
      next: (response: any) => {
        const reviewModal = bootstrap.Modal.getInstance(this.modalRef.nativeElement);        
        if (reviewModal) {          
          if (this.resetFn) {
            this.resetFn();
          }
          reviewModal.hide();
          this.toastr.success(response.message, 'Success');
          this.router.navigate(['mf/microfinance/member-setup']);
        }
      },
      error: (error) => {
      //error: (error: { error: { errors: any; }; }) => {
        // console.error('API error:', error);
         if (error.type === 'warning')
            this.toastr.warning(error.message, 'Warning');
          else if (error.type === 'strongerror')
            this.toastr.error(error.message, 'Error');
          else
            this.toastr.error(error.message);
        // const serverErrors = error?.error?.errors;
        // if (serverErrors) {
        //   Object.keys(serverErrors).forEach((field) => {
        //     const control = this.data.get(field);
        //     if (control) {
        //       control.setErrors({ serverError: serverErrors[field][0] });
        //     }
        //   });
        // }
        // this.toastr.error('Try Again !');
      }
    });
  }

  show() {
    const modal = new bootstrap.Modal(this.modalRef.nativeElement);
    modal.show();
  }
}
