import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankbranchFormComponent } from './bankbranch-form.component';

describe('BankbranchFormComponent', () => {
  let component: BankbranchFormComponent;
  let fixture: ComponentFixture<BankbranchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BankbranchFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankbranchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
