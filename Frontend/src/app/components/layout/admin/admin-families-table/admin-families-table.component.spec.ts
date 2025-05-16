import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesTableComponent } from './admin-families-table.component';

describe('AdminFamiliesTableComponent', () => {
  let component: AdminFamiliesTableComponent;
  let fixture: ComponentFixture<AdminFamiliesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
