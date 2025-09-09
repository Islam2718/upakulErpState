import { TestBed } from '@angular/core/testing';

import { EmployeeRegisterService } from './employee-register.service';

describe('EmployeeRegisterService', () => {
  let service: EmployeeRegisterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeRegisterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
