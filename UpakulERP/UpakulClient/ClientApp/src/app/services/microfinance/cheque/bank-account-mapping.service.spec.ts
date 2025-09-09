import { TestBed } from '@angular/core/testing';

import { BankAccountMappingService } from './bank-account-mapping.service';

describe('BankAccountMappingService', () => {
  let service: BankAccountMappingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BankAccountMappingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
