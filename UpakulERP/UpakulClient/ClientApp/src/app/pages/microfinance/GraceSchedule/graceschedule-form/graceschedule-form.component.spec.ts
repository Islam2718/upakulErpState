import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MappingFormComponent } from './graceschedule-form.component';

describe('MappingFormComponent', () => {
  let component: MappingFormComponent;
  let fixture: ComponentFixture<MappingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MappingFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MappingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
