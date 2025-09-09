import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyCollectionFormComponent } from './daily-collection-form.component';

describe('DailyCollectionFormComponent', () => {
  let component: DailyCollectionFormComponent;
  let fixture: ComponentFixture<DailyCollectionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DailyCollectionFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyCollectionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
