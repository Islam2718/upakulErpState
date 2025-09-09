import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeolistComponent } from './geolist.component';

describe('GeolistComponent', () => {
  let component: GeolistComponent;
  let fixture: ComponentFixture<GeolistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GeolistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GeolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
