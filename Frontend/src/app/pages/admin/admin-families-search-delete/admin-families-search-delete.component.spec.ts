import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesSearchDeleteComponent } from './admin-families-search-delete.component';

describe('AdminFamiliesSearchDeleteComponent', () => {
  let component: AdminFamiliesSearchDeleteComponent;
  let fixture: ComponentFixture<AdminFamiliesSearchDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesSearchDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesSearchDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
