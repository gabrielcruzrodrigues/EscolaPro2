import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersDetailsComponent } from './admin-users-details.component';

describe('AdminUsersDetailsComponent', () => {
  let component: AdminUsersDetailsComponent;
  let fixture: ComponentFixture<AdminUsersDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminUsersDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminUsersDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
