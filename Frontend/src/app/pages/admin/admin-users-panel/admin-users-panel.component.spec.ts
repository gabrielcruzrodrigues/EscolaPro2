import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersPanelComponent } from './admin-users-panel.component';

describe('AdminUsersPanelComponent', () => {
  let component: AdminUsersPanelComponent;
  let fixture: ComponentFixture<AdminUsersPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminUsersPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminUsersPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
