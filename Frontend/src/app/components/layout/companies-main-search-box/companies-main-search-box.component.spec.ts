import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompaniesMainSearchBoxComponent } from './companies-main-search-box.component';

describe('CompaniesMainSearchBoxComponent', () => {
  let component: CompaniesMainSearchBoxComponent;
  let fixture: ComponentFixture<CompaniesMainSearchBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompaniesMainSearchBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompaniesMainSearchBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
