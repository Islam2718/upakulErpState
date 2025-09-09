import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolydayFormComponent } from './holyday-form.component';

describe('HolydayFormComponent', () => {
  let component: HolydayFormComponent;
  let fixture: ComponentFixture<HolydayFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HolydayFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HolydayFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
