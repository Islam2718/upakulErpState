import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankbranchListComponent } from './bankbranch-list.component';

describe('BankbranchListComponent', () => {
  let component: BankbranchListComponent;
  let fixture: ComponentFixture<BankbranchListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BankbranchListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankbranchListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
