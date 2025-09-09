import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberOtpVerifyModalComponent } from './member-otp-verify-modal.component';

describe('MemberOtpVerifyModalComponent', () => {
  let component: MemberOtpVerifyModalComponent;
  let fixture: ComponentFixture<MemberOtpVerifyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberOtpVerifyModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberOtpVerifyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
