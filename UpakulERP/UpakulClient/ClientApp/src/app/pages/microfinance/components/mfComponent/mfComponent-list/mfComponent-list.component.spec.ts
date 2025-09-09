import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentMFListComponent } from './mfComponent-list.component';

describe('ComponentMFListComponent', () => {
  let component: ComponentMFListComponent;
  let fixture: ComponentFixture<ComponentMFListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComponentMFListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComponentMFListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
