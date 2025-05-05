import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoTopComponent } from './info-top.component';

describe('InfoTopComponent', () => {
  let component: InfoTopComponent;
  let fixture: ComponentFixture<InfoTopComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InfoTopComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoTopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
