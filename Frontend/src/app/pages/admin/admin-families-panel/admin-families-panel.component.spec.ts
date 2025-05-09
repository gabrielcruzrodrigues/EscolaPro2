import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFamiliesPanelComponent } from './admin-families-panel.component';

describe('AdminFamiliesPanelComponent', () => {
  let component: AdminFamiliesPanelComponent;
  let fixture: ComponentFixture<AdminFamiliesPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminFamiliesPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFamiliesPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
