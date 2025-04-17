import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthLoadingComponent } from './auth-loading.component';

describe('AuthLoadingComponent', () => {
  let component: AuthLoadingComponent;
  let fixture: ComponentFixture<AuthLoadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthLoadingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
