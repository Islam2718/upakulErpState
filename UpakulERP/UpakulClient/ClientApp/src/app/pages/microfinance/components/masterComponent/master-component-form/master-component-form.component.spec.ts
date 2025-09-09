import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterComponentFormComponent } from './master-component-form.component';

describe('MasterComponentFormComponent', () => {
  let component: MasterComponentFormComponent;
  let fixture: ComponentFixture<MasterComponentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MasterComponentFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MasterComponentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
