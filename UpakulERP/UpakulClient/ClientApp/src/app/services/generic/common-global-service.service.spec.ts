import { TestBed } from '@angular/core/testing';

import { CommonGlobalServiceService } from './common-global-service.service';

describe('CommonGlobalServiceService', () => {
  let service: CommonGlobalServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CommonGlobalServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
