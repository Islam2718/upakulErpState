import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MicrofinanceDashboardComponent } from './dashboard.component';

describe('MicrofinanceDashboardComponent', () => {
  let component: MicrofinanceDashboardComponent;
  let fixture: ComponentFixture<MicrofinanceDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MicrofinanceDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MicrofinanceDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
