import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageCounterInstallRequestsComponent } from './manage-counter-install-requests.component';

describe('ManageCounterInstallRequestsComponent', () => {
  let component: ManageCounterInstallRequestsComponent;
  let fixture: ComponentFixture<ManageCounterInstallRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageCounterInstallRequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManageCounterInstallRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
