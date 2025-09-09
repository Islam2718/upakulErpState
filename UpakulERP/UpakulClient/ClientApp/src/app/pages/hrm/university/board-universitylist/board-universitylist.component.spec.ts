import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardUniversitylistComponent } from './board-universitylist.component';

describe('BoardUniversitylistComponent', () => {
  let component: BoardUniversitylistComponent;
  let fixture: ComponentFixture<BoardUniversitylistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoardUniversitylistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoardUniversitylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
