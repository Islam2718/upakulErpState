import { Component, Input, ViewChild } from '@angular/core';
import { NotificationDetails } from '../../../models/Global/notification/notification.model';
import { CommonModule } from '@angular/common';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';
import { GraceScheduleService } from '../../../services/microfinance/GraceSchedule/graceschedule.service'; // service path অনুযায়ী
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-gs',
  imports: [CommonModule, ConfirmModalComponent],
  templateUrl: './gs.component.html',
})
export class GsComponent {
  @Input() details!: NotificationDetails;
  @ViewChild('approveModal') approveModal!: ConfirmModalComponent;

  private deleteIdToConfirm: number | null = null;
  private approvedIdToConfirm: number | null = null;

  constructor(
    private graceScheduleService: GraceScheduleService, // inject service
    private toastr: ToastrService                        // inject toastr
  ) {}

  approved(id: number) {
    this.approvedIdToConfirm = id;
    this.approveModal.show();
  }

  onApprovedConfirmed() {
    if (!this.details?.id) return;

    this.graceScheduleService.approveData(this.details.id).subscribe({
      next: (response: any) => {
        if (response.statusCode !== 200) {
          this.toastr.error(response.message, 'Error');
        } else {
          this.toastr.success(response.message, 'Success');
        }
      },
      error: () => {
        this.toastr.error('Approval failed');
      }
    });

    this.approvedIdToConfirm = null;
  }
}
