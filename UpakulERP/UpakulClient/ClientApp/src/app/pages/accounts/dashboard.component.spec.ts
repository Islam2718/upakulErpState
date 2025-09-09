import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountDashboardsComponent } from './dashboard.component';

describe('accountsComponent', () => {
  let component: AccountDashboardsComponent;
  let fixture: ComponentFixture<AccountDashboardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountDashboardsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountDashboardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
