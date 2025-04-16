import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompaniesSearchDeleteComponent } from './companies-search-delete.component';

describe('CompaniesSearchDeleteComponent', () => {
  let component: CompaniesSearchDeleteComponent;
  let fixture: ComponentFixture<CompaniesSearchDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompaniesSearchDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompaniesSearchDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
