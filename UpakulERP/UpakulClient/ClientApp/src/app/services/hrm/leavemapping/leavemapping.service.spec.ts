import { TestBed } from '@angular/core/testing';

import { LeavemappingService } from './leavemapping.service';

describe('LeavemappingService', () => {
  let service: LeavemappingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeavemappingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
