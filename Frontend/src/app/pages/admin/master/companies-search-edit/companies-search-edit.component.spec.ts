import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompaniesSearchEditComponent } from './companies-search-edit.component';

describe('CompaniesSearchEditComponent', () => {
  let component: CompaniesSearchEditComponent;
  let fixture: ComponentFixture<CompaniesSearchEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompaniesSearchEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompaniesSearchEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
