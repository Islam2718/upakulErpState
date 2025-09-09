import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../../../../core/config.service';
import { BaseService } from '../../../generic/base.service';
import { LoanProposalReport } from '../../../../models/microfinance/loan-proposal/loan-proposal-report';
interface DropdownValue {
  text: string;
  value: string;
  selected: boolean;
}
@Injectable({
  providedIn: 'root'
})
export class LoanProposalReportService extends BaseService<LoanProposalReport> {

  private domain_url: string;
  constructor(http: HttpClient, private configService: ConfigService) {
    super(http, `${configService.mfApiBaseUrl()}LoanProposal`);
    this.domain_url = configService.mfApiBaseUrl();
  } 
getLoanForm(loanApplicationId: number, summaryId: number = 0): Observable<LoanProposalReport> {
  return this.http.get<LoanProposalReport>(
    `${this.domain_url}LoanProposal/GetLoanForm?loanApplicationId=${loanApplicationId}&summaryId=${summaryId}`
  );
}

}

