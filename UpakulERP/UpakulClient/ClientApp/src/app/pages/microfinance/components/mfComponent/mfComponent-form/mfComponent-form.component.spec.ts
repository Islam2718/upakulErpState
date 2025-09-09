import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentMFFormComponent } from './mfComponent-form.component';

describe('ComponentMFFormComponent', () => {
  let component: ComponentMFFormComponent;
  let fixture: ComponentFixture<ComponentMFFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComponentMFFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComponentMFFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
