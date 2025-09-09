import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentSetupListComponent } from './component-setup-list.component';

describe('ComponentSetupListComponent', () => {
  let component: ComponentSetupListComponent;
  let fixture: ComponentFixture<ComponentSetupListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComponentSetupListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComponentSetupListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
