import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersSearchEditComponent } from './admin-users-search-edit.component';

describe('AdminUsersSearchEditComponent', () => {
  let component: AdminUsersSearchEditComponent;
  let fixture: ComponentFixture<AdminUsersSearchEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminUsersSearchEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminUsersSearchEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
