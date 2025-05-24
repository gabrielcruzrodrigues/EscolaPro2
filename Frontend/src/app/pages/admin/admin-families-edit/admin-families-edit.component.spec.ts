import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesEditComponent } from './admin-families-edit.component';

describe('AdminFamiliesEditComponent', () => {
  let component: AdminFamiliesEditComponent;
  let fixture: ComponentFixture<AdminFamiliesEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
