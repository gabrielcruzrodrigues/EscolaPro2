import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersSearchDeleteComponent } from './admin-users-search-delete.component';

describe('AdminUsersSearchDeleteComponent', () => {
  let component: AdminUsersSearchDeleteComponent;
  let fixture: ComponentFixture<AdminUsersSearchDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminUsersSearchDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminUsersSearchDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
