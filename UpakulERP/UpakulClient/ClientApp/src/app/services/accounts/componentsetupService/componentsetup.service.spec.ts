import { TestBed } from '@angular/core/testing';

import { ComponentsetupServiceService } from './componentsetup.service';

describe('ComponentsetupServiceService', () => {
  let service: ComponentsetupServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComponentsetupServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
