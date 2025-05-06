import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersShowComponent } from './admin-users-show.component';

describe('AdminUsersShowComponent', () => {
  let component: AdminUsersShowComponent;
  let fixture: ComponentFixture<AdminUsersShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminUsersShowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminUsersShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
