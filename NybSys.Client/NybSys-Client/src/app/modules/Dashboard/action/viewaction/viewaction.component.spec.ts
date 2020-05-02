import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewactionComponent } from './viewaction.component';

describe('ViewactionComponent', () => {
  let component: ViewactionComponent;
  let fixture: ComponentFixture<ViewactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
