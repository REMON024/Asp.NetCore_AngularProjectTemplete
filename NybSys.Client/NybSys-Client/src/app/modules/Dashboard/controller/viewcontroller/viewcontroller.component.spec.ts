import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewcontrollerComponent } from './viewcontroller.component';

describe('ViewcontrollerComponent', () => {
  let component: ViewcontrollerComponent;
  let fixture: ComponentFixture<ViewcontrollerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewcontrollerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewcontrollerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
