import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersSearchEditComponent } from './users-search-edit.component';

describe('UsersSearchEditComponent', () => {
  let component: UsersSearchEditComponent;
  let fixture: ComponentFixture<UsersSearchEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersSearchEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersSearchEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
