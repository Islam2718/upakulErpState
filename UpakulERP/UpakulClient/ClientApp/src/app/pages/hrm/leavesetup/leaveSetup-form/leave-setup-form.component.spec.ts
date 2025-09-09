import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveSetupFormComponent } from './leave-setup-form.component';

describe('LeaveSetupFormComponent', () => {
  let component: LeaveSetupFormComponent;
  let fixture: ComponentFixture<LeaveSetupFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveSetupFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveSetupFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
