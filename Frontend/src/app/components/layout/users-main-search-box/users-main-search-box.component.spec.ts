import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersMainSearchBoxComponent } from './users-main-search-box.component';

describe('UsersMainSearchBoxComponent', () => {
  let component: UsersMainSearchBoxComponent;
  let fixture: ComponentFixture<UsersMainSearchBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersMainSearchBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersMainSearchBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
