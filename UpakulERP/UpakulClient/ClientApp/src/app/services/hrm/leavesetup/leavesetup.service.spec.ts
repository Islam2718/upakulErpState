import { TestBed } from '@angular/core/testing';

import { LeavesetupService } from './leavesetup.service';

describe('LeavesetupService', () => {
  let service: LeavesetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeavesetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
