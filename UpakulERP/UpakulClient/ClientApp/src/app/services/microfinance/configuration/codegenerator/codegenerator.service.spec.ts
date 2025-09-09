import { TestBed } from '@angular/core/testing';

import { CodegeneratorService } from './codegenerator.service';

describe('CodegeneratorService', () => {
  let service: CodegeneratorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CodegeneratorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
