import {
  Component,
  Input,
  OnInit,
  OnChanges,
  SimpleChanges,
  AfterViewInit,
} from '@angular/core';
import { NotificationDetails } from '../../nav/nav.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoanProposalPut } from '../../../models/microfinance/loan-proposal/loan-proposal-put';
import { LoanProposalsService } from '../../../services/microfinance/loan/loan-proposal/loan-proposals.service';
import {
  PaymentTypeService,
  PaymentTypeDropdown,
  Bank,
  Branch,
  Cheque,
} from '../../../services/payment/payment-type.service';
import { NotificationService } from '../../../services/notification.service';
import { ToastrService } from 'ngx-toastr';
import html2pdf from 'html2pdf.js';
// state manage 
import { Store } from '@ngrx/store';
import { updateStatePersonal } from '../../../state/auth.actions';


declare var bootstrap: any;

@Component({
  selector: 'app-lp',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './lp.component.html',
  styleUrls: ['./lp.component.css'],
})
export class LPComponent implements OnInit, OnChanges {
  @Input() details!: NotificationDetails;

  dataForm!: FormGroup;
  submitting = false;

  paymentTypeList: PaymentTypeDropdown[] = [];
  checkInformation: any;
  newDta: any;

  

  constructor(
    private fb: FormBuilder,
    private loanProposalsService: LoanProposalsService,
    private paymentTypeService: PaymentTypeService,
    private notificationService: NotificationService,
    private toastr: ToastrService,
    private store:Store
  ) {
    this.newDta = 'Islam-' + Math.floor(Math.random() * 6);
  }
  
  updatePersonal(newPersonalData: any = 'Islam27') {
    this.store.dispatch(updateStatePersonal({ statePersonal: newPersonalData }));
  }

  ngOnInit() {
    this.initForm();

    if (this.details) {
      this.patchForm(this.details);
    }

    if (this.details?.loanType === 'R') {
      this.loadPaymentTypes();
    }

    this.checkInformation = {
      payTo: '2503081454 - Mahfuz',
      acc_Pay: 'Account Pay',
      inWord: 'Five thousand',
      amount: 5000,
      day_1: 1,
      day_2: 5,
      month_1: 0,
      month_2: 7,
      year_1: 2,
      year_2: 0,
      year_3: 2,
      year_4: 5,
    };
  }

  // ngAfterViewInit() {
  //   const modalEl = document.getElementById('staticBackdrop');
  //   const myModal = new bootstrap.Modal(modalEl);
  //   myModal.show();
  // }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['details'] && this.dataForm) {
      this.patchForm(this.details);
    }
  }

  /** Initialize reactive form */
  private initForm() {
    this.dataForm = this.fb.group({
      loanApplicationId: [null],
      proposedAmount: [null, Validators.required],
      actionType: ['APPROVED', Validators.required],
      note: [''],
      paymentType: [''], // added paymentType
      bank: [''],
      // branch: [null],
      cheque: [''],
      receiptReference: [''],
    });
  }

  /** Patch form values from notification details */
  private patchForm(details: NotificationDetails) {
    this.dataForm.patchValue({
      loanApplicationId: details?.id || null,
      proposedAmount: details?.proposedAmount || null,
      actionType: 'APPROVED',
      note: '',
    });
  }

  /** Load payment types from service */
  loadPaymentTypes() {
    this.paymentTypeService.getPaymentTypeDropdown().subscribe({
      next: (data) => {
        this.paymentTypeList = data;
        console.log('Payment Type List:', this.paymentTypeList);
      },
      error: (error) => {
        console.error('Error fetching payment types:', error);
        this.toastr.error('Failed to load payment types');
      },
    });
  }

  /** Handle action type change */
  actionTypeSelectionFunc(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.dataForm.get('actionType')?.setValue(selectedValue);

    if (selectedValue === 'APPROVED') {
      this.dataForm.get('note')?.setValue('');
    }
  }

  /** Submit approve form */
  onSubmitApproveForm() {
    if (this.dataForm.invalid) {
      this.toastr.error('Please fill required fields');
      return;
    }

    this.submitting = true;
    const payload: LoanProposalPut = {
      loanApplicationId: this.dataForm.get('loanApplicationId')?.value,
      proposedAmount: this.details.proposedAmount,
      actionType:
        this.details.loanType === 'R'
          ? 'DISBURSED'
          : this.dataForm.get('actionType')?.value,
      note: this.dataForm.get('note')?.value || null,
      bankId: this.dataForm.get('bank')?.value || null,
      chequeNo: this.dataForm.get('cheque')?.value || null,
      referenceNo: this.dataForm.get('referenceNo')?.value || null,
      paymentType: this.dataForm.get('paymentType')?.value,
    };

    this.loanProposalsService.updateLoanWorkFlow(payload).subscribe({
      next: (response: any) => {
        this.toastr.success(response.message);
        this.notificationService.removeNotification(this.details.id);

        // Show cheque modal if response.data exists
        if (response.data) {
          this.checkInformation = response.data;
          const modalEl = document.getElementById('staticBackdrop');
          const myModal = new bootstrap.Modal(modalEl);
          myModal.show();
        }

        // Hide offcanvas
        const offcanvasEl = document.querySelector('.offcanvas') as HTMLElement;
        if (offcanvasEl) {
          const bsOffcanvas =
            bootstrap.Offcanvas.getInstance(offcanvasEl) ||
            new bootstrap.Offcanvas(offcanvasEl);
          bsOffcanvas.hide();
        }

        this.submitting = false;
      },
      error: (error: any) => {
        console.error('Error:', error.message);
        this.toastr.error(error.message || 'Failed to submit');
        this.submitting = false;
      },
    });
  }

  // onChequeConfirm() {
  //   console.log('Cheque confirmed!');
  //   // You can do printing or other action here
  // }

  // paymentTypeFilterFunc \\this.dataForm.get('paymentType')?.value || ''
  paymentType = '';
  paymentTypeFilterFunc(event: any) {
    this.paymentType = this.dataForm.get('paymentType')?.value || '';
    if (this.paymentType === 'CHQ' || this.paymentType === 'APC') {
      this.bankListDropdown();
    }
  }

  bankList: Bank[] = [];
  bankListDropdown() {
    this.paymentTypeService.getBankDropdown().subscribe({
      next: (data) => (this.bankList = data),
      error: (err) => console.error('Error fetching banks:', err),
    });
  }
  bankId = ''; //this.paymentType = this.dataForm.get('paymentType')?.value || ''
  bankListFilterFunc(event: any) {
    this.bankId = this.dataForm.get('bank')?.value || '';
    // console.log('__BankId',event.value); this.loadBranches(this.bankId);
    this.chequeListDropdownFunc(this.bankId);
  }

  chequeList: Cheque[] = [];
  chequeListDropdownFunc(bankId: string) {
    this.paymentTypeService.getChequeDropdown(bankId).subscribe({
      next: (data) => {
        this.chequeList = data;
        console.log('Cheque List:', this.chequeList);
      },
      error: (err) => console.error('Error fetching cheques:', err),
    });
  }

  // print func
  printCheque() {
    const printContents = document.getElementById(
      'printableChequeArea'
    )?.innerHTML;
    if (!printContents) return;

    // Open preview window at similar aspect ratio
    const printWindow = window.open('', '', 'width=710,height=326');
    if (printWindow) {
      printWindow.document.write(`
      <html>
        <head>
          <title>Print Cheque</title>
          <style>
            body { margin: 0; padding: 0; font-family: Arial, sans-serif; }

            @page {
              size: 188mm 86mm; /* real cheque size */
              margin: 0;
            }

            .cheque { width: 100%; height: 100%; box-sizing: border-box; padding: 20px; }
            .dotted-line { border-bottom: 1px dotted #000; min-height: 24px; }
            .date-box { display: inline-block; border: 1px solid #000; padding: 4px; margin: 2px; width: 20px; text-align: center; }
            .amount-box { border: 1px solid #000; padding: 6px; font-weight: bold; }
            .footer { margin-top: 20px; font-weight: bold; }
          </style>
        </head>
        <body>
          ${printContents}
        </body>
      </html>
    `);
      printWindow.document.close();
      printWindow.print();
    }
  }

  downloadCheque() {
    const chequeElement = document.getElementById('printableChequeArea');
    if (!chequeElement) return;

    const options = {
      margin: 0,
      filename: Date()+'- cheque.pdf',
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 2 }, // improves quality
      jsPDF: { unit: 'mm', format: [86, 188], orientation: 'landscape' } // cheque size
    };

    html2pdf().set(options).from(chequeElement).save();
  }
}
