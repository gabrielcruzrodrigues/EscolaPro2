import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewDocumentButtonComponent } from './view-document-button.component';

describe('ViewDocumentButtonComponent', () => {
  let component: ViewDocumentButtonComponent;
  let fixture: ComponentFixture<ViewDocumentButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewDocumentButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewDocumentButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
