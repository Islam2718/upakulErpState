import { TestBed } from '@angular/core/testing';

import { LoanWorkflowService } from './loan-workflow.service';

describe('LoanWorkflowService', () => {
  let service: LoanWorkflowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanWorkflowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
