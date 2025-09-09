import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanProposalListComponent } from './loan-proposal-list.component';

describe('LoanProposalListComponent', () => {
  let component: LoanProposalListComponent;
  let fixture: ComponentFixture<LoanProposalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanProposalListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanProposalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
