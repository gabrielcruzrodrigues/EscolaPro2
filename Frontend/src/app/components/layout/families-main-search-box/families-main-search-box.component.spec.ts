import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FamiliesMainSearchBoxComponent } from './families-main-search-box.component';

describe('FamiliesMainSearchBoxComponent', () => {
  let component: FamiliesMainSearchBoxComponent;
  let fixture: ComponentFixture<FamiliesMainSearchBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FamiliesMainSearchBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FamiliesMainSearchBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
