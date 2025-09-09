import { TestBed } from '@angular/core/testing';

import { BudgetComponentService } from './budget-component.service';

describe('BudgetComponentService', () => {
  let service: BudgetComponentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BudgetComponentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
