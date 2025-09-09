import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanProposalReportComponent } from './loan-proposal-report.component';

describe('LoanProposalReportComponent', () => {
  let component: LoanProposalReportComponent;
  let fixture: ComponentFixture<LoanProposalReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanProposalReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanProposalReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
