import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpinningComponent } from './spinning.component';

describe('SpinningComponent', () => {
  let component: SpinningComponent;
  let fixture: ComponentFixture<SpinningComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpinningComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpinningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
