import { TestBed } from '@angular/core/testing';

import { LoanconfigureService } from './loanconfigure.service';

describe('LoanconfigureService', () => {
  let service: LoanconfigureService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanconfigureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
