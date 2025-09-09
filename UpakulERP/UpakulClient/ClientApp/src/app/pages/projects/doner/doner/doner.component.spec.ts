import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonerComponent } from './doner.component';

describe('DonerComponent', () => {
  let component: DonerComponent;
  let fixture: ComponentFixture<DonerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
