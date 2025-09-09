import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodegeneratorComponent } from './codegenerator.component';

describe('CodegeneratorComponent', () => {
  let component: CodegeneratorComponent;
  let fixture: ComponentFixture<CodegeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CodegeneratorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CodegeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
