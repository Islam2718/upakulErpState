import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFormReviewModalComponent } from './member-form-review-modal.component';

describe('MemberFormReviewModalComponent', () => {
  let component: MemberFormReviewModalComponent;
  let fixture: ComponentFixture<MemberFormReviewModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberFormReviewModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberFormReviewModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
