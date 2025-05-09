import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesCreateComponent } from './admin-families-create.component';

describe('AdminFamiliesCreateComponent', () => {
  let component: AdminFamiliesCreateComponent;
  let fixture: ComponentFixture<AdminFamiliesCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
