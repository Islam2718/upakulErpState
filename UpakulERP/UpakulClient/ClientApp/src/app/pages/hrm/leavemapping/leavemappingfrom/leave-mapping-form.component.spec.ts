import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveMappingFormComponent } from './leave-mapping-form.component';

describe('LeaveMappingFormComponent', () => {
  let component: LeaveMappingFormComponent;
  let fixture: ComponentFixture<LeaveMappingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveMappingFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveMappingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
