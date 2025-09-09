import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoanProposalReportService } from '../../../../../services/microfinance/loan/components/loan-proposal-report.service';
import { LoanProposalReport } from '../../../../../models/microfinance/loan-proposal/loan-proposal-report';



@Component({
  selector: 'app-loan-proposal-report',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loan-proposal-report.component.html',
  styleUrls: ['./loan-proposal-report.component.css'],
})
export class LoanProposalReportComponent implements OnInit {
  @Input() loanproposalId!: number;
  @Output() closeModal = new EventEmitter<void>();

  loanProposalReportData!: LoanProposalReport;

  constructor(
    private loanproposalreportservice: LoanProposalReportService) {}

  ngOnInit(): void {
    if (this.loanproposalId) {
      this.loanproposalreportservice.getLoanForm(this.loanproposalId).subscribe({
        next: (res: any) => {
          console.log('Loan Proposal API response:', res);
          this.loanProposalReportData = res.data;
        },
        error: (err) => console.error(err),
      });
    }
  }

  close() {
    this.closeModal.emit();
  }

  printDiv() {
    const printContents = document.querySelector('.printablearea')?.outerHTML;
    if (!printContents) return;

    const popupWin = window.open('', '_blank', 'top=0,left=0,height=auto,width=auto');
    if (!popupWin) return;

    const styles = Array.from(document.querySelectorAll('link[rel="stylesheet"], style'))
      .map((el) => el.outerHTML)
      .join('');

    const componentStyles = `
      body { margin:0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 8px }
      .coast-a4 { width:210mm; min-height:297mm; padding:15mm 18mm; background:#fff; color:#222; font-size:11px; line-height:1.4; border:1px solid #ccc; border-radius:6px; box-shadow:0 3px 12px rgba(0,0,0,0.1);}
      table { border-collapse: collapse; width:100%; }
      th, td { border:1px solid #ccc; padding:3mm 4px; }
      .uline { border-bottom:1px solid #444; display:inline-block; padding-bottom:2px; }
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
          <title>Loan Proposal Report</title>
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
