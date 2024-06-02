import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeInstallCounterRequestComponent } from './make-install-counter-request.component';

describe('MakeInstallCounterRequestComponent', () => {
  let component: MakeInstallCounterRequestComponent;
  let fixture: ComponentFixture<MakeInstallCounterRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MakeInstallCounterRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MakeInstallCounterRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
