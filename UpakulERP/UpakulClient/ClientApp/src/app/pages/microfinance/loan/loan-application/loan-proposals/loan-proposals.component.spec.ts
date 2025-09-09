import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanProposalsComponent } from './loan-proposals.component';

describe('LoanProposalsComponent', () => {
  let component: LoanProposalsComponent;
  let fixture: ComponentFixture<LoanProposalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanProposalsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanProposalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
