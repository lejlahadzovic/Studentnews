import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StipendijaDetaljiComponent } from './stipendija-detalji.component';

describe('StipendijaDetaljiComponent', () => {
  let component: StipendijaDetaljiComponent;
  let fixture: ComponentFixture<StipendijaDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StipendijaDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StipendijaDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
