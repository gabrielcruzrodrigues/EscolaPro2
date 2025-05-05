import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminLastActiveUsersComponent } from './admin-last-active-users.component';

describe('AdminLastActiveUsersComponent', () => {
  let component: AdminLastActiveUsersComponent;
  let fixture: ComponentFixture<AdminLastActiveUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminLastActiveUsersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminLastActiveUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
