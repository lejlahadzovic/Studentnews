import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmjestajiComponent } from './smjestaji.component';

describe('SmjestajiComponent', () => {
  let component: SmjestajiComponent;
  let fixture: ComponentFixture<SmjestajiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SmjestajiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmjestajiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
