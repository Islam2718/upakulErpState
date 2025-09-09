import { TestBed } from '@angular/core/testing';

import { DailyCollectionService } from './daily-collection.service';

describe('DailyCollectionService', () => {
  let service: DailyCollectionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DailyCollectionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
