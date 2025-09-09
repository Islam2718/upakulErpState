import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoaAssignModalComponent } from './coa-assign-modal.component';

describe('CoaAssignModalComponent', () => {
  let component: CoaAssignModalComponent;
  let fixture: ComponentFixture<CoaAssignModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoaAssignModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoaAssignModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
