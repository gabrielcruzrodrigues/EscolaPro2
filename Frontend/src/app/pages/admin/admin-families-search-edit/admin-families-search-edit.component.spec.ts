import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesSearchEditComponent } from './admin-families-search-edit.component';

describe('AdminFamiliesSearchEditComponent', () => {
  let component: AdminFamiliesSearchEditComponent;
  let fixture: ComponentFixture<AdminFamiliesSearchEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesSearchEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesSearchEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
