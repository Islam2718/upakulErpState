import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeRegisterListComponent } from './employee-register-list.component';

describe('EmployeeRegisterListComponent', () => {
  let component: EmployeeRegisterListComponent;
  let fixture: ComponentFixture<EmployeeRegisterListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeRegisterListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeRegisterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
