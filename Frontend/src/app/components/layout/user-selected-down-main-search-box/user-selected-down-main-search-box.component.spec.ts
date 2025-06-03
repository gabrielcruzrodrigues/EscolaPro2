import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSelectedDownMainSearchBoxComponent } from './user-selected-down-main-search-box.component';

describe('UserSelectedDownMainSearchBoxComponent', () => {
  let component: UserSelectedDownMainSearchBoxComponent;
  let fixture: ComponentFixture<UserSelectedDownMainSearchBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserSelectedDownMainSearchBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserSelectedDownMainSearchBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
