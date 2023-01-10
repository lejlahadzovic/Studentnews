import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StipenditoriComponent } from './stipenditori.component';

describe('StipenditoriComponent', () => {
  let component: StipenditoriComponent;
  let fixture: ComponentFixture<StipenditoriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StipenditoriComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StipenditoriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
