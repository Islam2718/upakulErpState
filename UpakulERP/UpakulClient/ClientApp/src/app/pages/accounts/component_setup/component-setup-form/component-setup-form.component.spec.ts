import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentSetupFormComponent } from './component-setup-form.component';

describe('ComponentSetupFormComponent', () => {
  let component: ComponentSetupFormComponent;
  let fixture: ComponentFixture<ComponentSetupFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComponentSetupFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComponentSetupFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
