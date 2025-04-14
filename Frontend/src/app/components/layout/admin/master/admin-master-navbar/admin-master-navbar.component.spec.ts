import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMasterNavbarComponent } from './admin-master-navbar.component';

describe('AdminMasterNavbarComponent', () => {
  let component: AdminMasterNavbarComponent;
  let fixture: ComponentFixture<AdminMasterNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminMasterNavbarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminMasterNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
