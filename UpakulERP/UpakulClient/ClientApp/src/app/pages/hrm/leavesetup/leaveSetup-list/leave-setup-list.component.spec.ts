import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveSetupListComponent } from './leave-setup-list.component';

describe('LeaveSetupListComponent', () => {
  let component: LeaveSetupListComponent;
  let fixture: ComponentFixture<LeaveSetupListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveSetupListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveSetupListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
