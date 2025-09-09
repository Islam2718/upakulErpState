import { TestBed } from '@angular/core/testing';

import { LoanProposalsService } from './loan-proposals.service';

describe('LoanProposalsService', () => {
  let service: LoanProposalsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanProposalsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
