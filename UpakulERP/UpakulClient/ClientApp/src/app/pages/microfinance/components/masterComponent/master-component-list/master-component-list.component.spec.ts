import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterComponentListComponent } from './master-component-list.component';

describe('MasterComponentListComponent', () => {
  let component: MasterComponentListComponent;
  let fixture: ComponentFixture<MasterComponentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MasterComponentListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MasterComponentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
