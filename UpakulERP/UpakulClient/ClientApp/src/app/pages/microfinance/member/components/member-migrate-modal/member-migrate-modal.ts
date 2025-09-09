import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';
import { MemberService } from '../../../../../services/microfinance/member/member.service';



@Component({
  selector: 'app-member-migrate-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './member-migrate-modal.html',
  styleUrls: ['./member-migrate-modal.css']
})
export class MemberMigrateModalComponent {
  @Input() memberId!: number;
  @Input() memberName!: string;
  @Output() closed = new EventEmitter<boolean>();

  migratedNote: string = '';
  isMigrating: boolean = false;

  constructor(
    private memberservice: MemberService,
    private toastr: ToastrService
  ) {}

  close(success: boolean = false) {
    this.closed.emit(success);
  }

  migrateMember() {
    if (!this.migratedNote) {
      this.toastr.warning('Please enter a migration note', 'Warning');
      return;
    }

    this.isMigrating = true;

    this.memberservice.updateMigrateMember(this.memberId, this.migratedNote)
      .pipe(
        catchError(err => {
          const msg = err?.error?.message || 'Migration failed';
          this.toastr.error(msg, 'Error');
          this.isMigrating = false;
          return throwError(() => err);
        })
      )
      .subscribe(res => {
        this.toastr.success(res?.message || 'Member migrated successfully', 'Success');
        this.isMigrating = false;
        this.close(true); // emit success
      });
  }
}
