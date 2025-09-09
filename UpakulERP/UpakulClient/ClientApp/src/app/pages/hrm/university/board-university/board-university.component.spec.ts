import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardUniversityComponent } from './board-university.component';

describe('BoardUniversityComponent', () => {
  let component: BoardUniversityComponent;
  let fixture: ComponentFixture<BoardUniversityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoardUniversityComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoardUniversityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
