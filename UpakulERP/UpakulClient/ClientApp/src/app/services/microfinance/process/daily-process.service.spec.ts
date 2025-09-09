import { TestBed } from '@angular/core/testing';

import { DailyProcessService } from './daily-process.service';

describe('DailyProcessService', () => {
  let service: DailyProcessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DailyProcessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
