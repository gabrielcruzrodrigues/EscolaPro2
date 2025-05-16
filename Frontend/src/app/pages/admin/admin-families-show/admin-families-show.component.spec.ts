import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesShowComponent } from './admin-families-show.component';

describe('AdminFamiliesShowComponent', () => {
  let component: AdminFamiliesShowComponent;
  let fixture: ComponentFixture<AdminFamiliesShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesShowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
