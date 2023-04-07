import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniverzitetDetaljiComponent } from './univerzitet-detalji.component';

describe('UniverzitetDetaljiComponent', () => {
  let component: UniverzitetDetaljiComponent;
  let fixture: ComponentFixture<UniverzitetDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniverzitetDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UniverzitetDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
