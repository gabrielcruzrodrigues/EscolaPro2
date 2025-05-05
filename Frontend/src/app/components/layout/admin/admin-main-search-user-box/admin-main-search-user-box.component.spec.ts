import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMainSearchUserBoxComponent } from './admin-main-search-user-box.component';

describe('AdminMainSearchUserBoxComponent', () => {
  let component: AdminMainSearchUserBoxComponent;
  let fixture: ComponentFixture<AdminMainSearchUserBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminMainSearchUserBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminMainSearchUserBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
