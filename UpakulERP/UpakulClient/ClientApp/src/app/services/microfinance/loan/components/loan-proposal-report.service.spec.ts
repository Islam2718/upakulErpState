import { TestBed } from '@angular/core/testing';

import { LoanProposalReportService } from './loan-proposal-report.service';

describe('LoanProposalReportService', () => {
  let service: LoanProposalReportService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanProposalReportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
