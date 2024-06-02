import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackupsManagementComponent } from './backups-management.component';

describe('BackupsManagementComponent', () => {
  let component: BackupsManagementComponent;
  let fixture: ComponentFixture<BackupsManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BackupsManagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BackupsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
