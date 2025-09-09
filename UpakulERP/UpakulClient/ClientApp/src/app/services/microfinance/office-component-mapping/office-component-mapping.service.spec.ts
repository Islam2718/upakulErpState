import { TestBed } from '@angular/core/testing';

import { OfficeComponentMappingService } from './office-component-mapping.service';

describe('OfficeComponentMappingService', () => {
  let service: OfficeComponentMappingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficeComponentMappingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
