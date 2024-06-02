import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTechinspectionRequestsComponent } from './manage-techinspection-requests.component';

describe('ManageTechinspectionRequestsComponent', () => {
  let component: ManageTechinspectionRequestsComponent;
  let fixture: ComponentFixture<ManageTechinspectionRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageTechinspectionRequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManageTechinspectionRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
