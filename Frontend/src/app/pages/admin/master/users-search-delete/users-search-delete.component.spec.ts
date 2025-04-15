import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersSearchDeleteComponent } from './users-search-delete.component';

describe('UsersSearchDeleteComponent', () => {
  let component: UsersSearchDeleteComponent;
  let fixture: ComponentFixture<UsersSearchDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersSearchDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersSearchDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
