import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyCollectionListComponent } from './daily-collection-list.component';

describe('WeeklyCollectionListComponent', () => {
  let component: DailyCollectionListComponent;
  let fixture: ComponentFixture<DailyCollectionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DailyCollectionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyCollectionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
