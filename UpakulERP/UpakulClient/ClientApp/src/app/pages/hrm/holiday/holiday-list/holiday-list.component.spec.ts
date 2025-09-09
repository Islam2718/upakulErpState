import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolydayListComponent } from './holyday-list.component';

describe('HolydayListComponent', () => {
  let component: HolydayListComponent;
  let fixture: ComponentFixture<HolydayListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HolydayListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HolydayListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
