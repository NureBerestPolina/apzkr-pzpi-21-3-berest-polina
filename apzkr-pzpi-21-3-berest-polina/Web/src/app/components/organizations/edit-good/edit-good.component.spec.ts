import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGoodComponent } from './edit-good.component';

describe('EditGoodComponent', () => {
  let component: EditGoodComponent;
  let fixture: ComponentFixture<EditGoodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditGoodComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditGoodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
