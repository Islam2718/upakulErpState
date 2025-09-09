import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityPlanComponent } from './activity-plan.component';

describe('ActivityPlanComponent', () => {
  let component: ActivityPlanComponent;
  let fixture: ComponentFixture<ActivityPlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActivityPlanComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivityPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
