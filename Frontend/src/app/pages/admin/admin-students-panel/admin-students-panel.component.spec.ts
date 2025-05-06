import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminStudentsPanelComponent } from './admin-students-panel.component';

describe('AdminStudentsPanelComponent', () => {
  let component: AdminStudentsPanelComponent;
  let fixture: ComponentFixture<AdminStudentsPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminStudentsPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminStudentsPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
