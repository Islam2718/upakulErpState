import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberConfirmDrawerComponent } from './member-confirm-drawer.component';

describe('MemberConfirmDrawerComponent', () => {
  let component: MemberConfirmDrawerComponent;
  let fixture: ComponentFixture<MemberConfirmDrawerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberConfirmDrawerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberConfirmDrawerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
