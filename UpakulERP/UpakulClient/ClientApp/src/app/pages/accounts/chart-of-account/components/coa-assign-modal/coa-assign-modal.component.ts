import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  ChartOfAccountService,
  Office,
} from '../../../../../services/accounts/chart-of-account/chart-of-account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-coa-assign-modal',
  standalone: true, // if using Angular 15+ standalone component
  imports: [CommonModule, FormsModule], // ← important
  templateUrl: './coa-assign-modal.component.html',
  styleUrls: ['./coa-assign-modal.component.css'],
})
export class CoaAssignModalComponent {
  @Input() node!: any;
  @Input() accountId!: number;

  checkboxOptions: Office[] = [];

  constructor(
    private apiService: ChartOfAccountService,
    private toastr: ToastrService
  ) {}

  open() {
    this.loadOfficeList(this.node.accountId);
    // console.log('_officeList:', this.checkboxOptions);
    const modalElement = document.getElementById('coaAssignModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  loadOfficeList(accountId: any) {
    this.apiService.getAssignableOfficeList(accountId).subscribe({
      next: (res) => {
        this.checkboxOptions = res.map((o) => ({ ...o, selected: o.isAssign }));
      },
      error: (err) => console.error('Failed to load offices', err),
    });
  }

  onSubmit() {
    // Filter selected offices
    const selectedOffices = this.checkboxOptions.filter((o) => o.selected);

    // Transform payload to the desired structure
    const payload = selectedOffices.map((o) => ({
      officeId: o.officeId,
      accountId: this.node.accountId,
    }));

    // Show in console
    console.log('Form submitted:', payload);
    this.apiService.assignOffices(payload).subscribe({
      next: (res) => {
        // console.log('Offices assigned successfully', res);
        if (res.type === 'warning') this.toastr.warning(res.message, 'Warning');
        else if (res.type === 'strongerror')
          this.toastr.error(res.message, 'Error');
        else this.toastr.success(res.message, 'Success');
        // Close modal after success
        const modalElement = document.getElementById('coaAssignModal');
        const modal = (window as any).bootstrap.Modal.getInstance(modalElement);
        modal?.hide();
      },
      error: (err) => {
        console.error('Failed to assign offices', err);
      },
    });

    // Optional: close modal after submit
    const modalElement = document.getElementById('coaAssignModal');
    if (modalElement) {
      const modal = (window as any).bootstrap.Modal.getInstance(modalElement);
      modal?.hide();
    }
  }

  // getSelectedOffices(): Office[] {
  //   return this.checkboxOptions.filter(o => o.selected);
  // }
}
