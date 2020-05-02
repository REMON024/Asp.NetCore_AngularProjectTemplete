import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditcontrollerComponent } from './editcontroller.component';

describe('EditcontrollerComponent', () => {
  let component: EditcontrollerComponent;
  let fixture: ComponentFixture<EditcontrollerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditcontrollerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditcontrollerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
