import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCounterUriComponent } from './add-counter-uri.component';

describe('AddCounterUriComponent', () => {
  let component: AddCounterUriComponent;
  let fixture: ComponentFixture<AddCounterUriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCounterUriComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCounterUriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
