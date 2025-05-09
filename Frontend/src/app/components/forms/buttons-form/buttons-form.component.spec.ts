import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonsFormComponent } from './buttons-form.component';

describe('ButtonsFormComponent', () => {
  let component: ButtonsFormComponent;
  let fixture: ComponentFixture<ButtonsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ButtonsFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ButtonsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
