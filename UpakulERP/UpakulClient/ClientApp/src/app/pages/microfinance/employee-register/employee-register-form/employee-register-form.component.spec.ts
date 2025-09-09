import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeRegisterFormComponent } from './employee-register-form.component';

describe('EmployeeRegisterFormComponent', () => {
  let component: EmployeeRegisterFormComponent;
  let fixture: ComponentFixture<EmployeeRegisterFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeRegisterFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeRegisterFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
