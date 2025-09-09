import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeComponentMappingComponent } from './office-component-mapping.component';

describe('OfficeComponentMappingComponent', () => {
  let component: OfficeComponentMappingComponent;
  let fixture: ComponentFixture<OfficeComponentMappingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfficeComponentMappingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficeComponentMappingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
