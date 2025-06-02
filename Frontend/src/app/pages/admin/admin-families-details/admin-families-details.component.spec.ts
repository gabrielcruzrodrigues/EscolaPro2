import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesDetailsComponent } from './admin-families-details.component';

describe('AdminFamiliesDetailsComponent', () => {
  let component: AdminFamiliesDetailsComponent;
  let fixture: ComponentFixture<AdminFamiliesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
