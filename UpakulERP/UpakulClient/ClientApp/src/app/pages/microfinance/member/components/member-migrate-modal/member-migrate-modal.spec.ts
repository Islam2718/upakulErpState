import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberMigrationComponent } from './member-migrate-modal';

describe('MemberMigrationComponent', () => {
  let component: MemberMigrationComponent;
  let fixture: ComponentFixture<MemberMigrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberMigrationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberMigrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
