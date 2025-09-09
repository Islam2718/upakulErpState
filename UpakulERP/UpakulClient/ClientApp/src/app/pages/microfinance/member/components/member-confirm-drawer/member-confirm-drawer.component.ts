import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr'; // optional for user feedback
import { ReactiveFormsModule } from '@angular/forms';
import { ConfigService } from '../../../../../core/config.service';
import { BtnService } from '../../../../../services/btn-service/btn-service';

@Component({
  selector: 'app-member-confirm-drawer',
  templateUrl: './member-confirm-drawer.component.html',
  styleUrls: ['./member-confirm-drawer.component.css'],
  imports: [ReactiveFormsModule]
})
export class MemberConfirmDrawerComponent implements OnInit {
  qry: string | null = null;

  @Input() memberData: any;

  approveForm!: FormGroup;

  // constructor(http: HttpClient, private configService: ConfigService) {
  //   super(http, `${configService.mfApiBaseUrl()}Member`);
  // }
  
  private apiUrl: string; 

  constructor(
    
   public Button: BtnService,
            // private route: ActivatedRoute

    private fb: FormBuilder,
    private http: HttpClient,
    private toastr: ToastrService,  // optional, for toast notifications
    private configService: ConfigService
  ) {
    // super(http, `${configService.mfApiBaseUrl()}Member`);
    this.apiUrl = `${this.configService.mfApiBaseUrl()}Member/Approved`;
  }

  ngOnInit() {
    this.apiUrl = `${this.configService.mfApiBaseUrl()}Member/Approved`;
    // initialize form
    this.approveForm = this.fb.group({
      isApproved: ['approved', Validators.required], // default select option
      note: ['']
    });
  }

  onSubmitConfirmMember() {
    if (this.approveForm.invalid) {
      this.toastr.error('Please select approval status'); // optional
      return;
    }

    if (!this.memberData?.memberId) {
      this.toastr.error('Member ID missing');
      return;
    }

    // prepare payload
    const payload = {
      MemberId: this.memberData.memberId,
      IsApproved: this.approveForm.value.isApproved === 'approved', // convert string to boolean
      Note: this.approveForm.value.note
    };

    this.http.put(this.apiUrl, payload).subscribe({
      next: (res) => {
        this.toastr.success('Member status updated successfully!');
        // optionally reset form or close modal
      },
      error: (err) => {
        this.toastr.error('Failed to update member status');
        console.error(err);
      }
    });
  }
}
