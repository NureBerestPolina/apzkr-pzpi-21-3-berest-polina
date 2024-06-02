import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppClientListComponent } from './app-client-list.component';

describe('AppClientListComponent', () => {
  let component: AppClientListComponent;
  let fixture: ComponentFixture<AppClientListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppClientListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppClientListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
