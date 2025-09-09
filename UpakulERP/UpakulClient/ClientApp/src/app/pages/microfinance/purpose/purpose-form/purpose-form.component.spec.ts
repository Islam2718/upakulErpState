import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurposeFormComponent } from './purpose-form.component';

describe('PurposeFormComponent', () => {
  let component: PurposeFormComponent;
  let fixture: ComponentFixture<PurposeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurposeFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurposeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
