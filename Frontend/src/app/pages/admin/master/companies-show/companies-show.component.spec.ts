import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompaniesShowComponent } from './companies-show.component';

describe('CompaniesShowComponent', () => {
  let component: CompaniesShowComponent;
  let fixture: ComponentFixture<CompaniesShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompaniesShowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompaniesShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
