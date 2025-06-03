import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminStudentsCreateComponent } from './admin-students-create.component';

describe('AdminStudentsCreateComponent', () => {
  let component: AdminStudentsCreateComponent;
  let fixture: ComponentFixture<AdminStudentsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminStudentsCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminStudentsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
