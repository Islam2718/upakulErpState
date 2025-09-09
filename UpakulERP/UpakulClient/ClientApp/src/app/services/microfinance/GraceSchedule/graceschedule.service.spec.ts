import { TestBed } from '@angular/core/testing';

import {GraceScheduleService } from './graceschedule.service';

describe('MappingService', () => {
  let service: GraceScheduleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GraceScheduleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
