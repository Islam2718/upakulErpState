import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFormSubmitSuccessModalComponent } from './member-form-submit-success-modal.component';

describe('MemberFormSubmitSuccessModalComponent', () => {
  let component: MemberFormSubmitSuccessModalComponent;
  let fixture: ComponentFixture<MemberFormSubmitSuccessModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberFormSubmitSuccessModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberFormSubmitSuccessModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
