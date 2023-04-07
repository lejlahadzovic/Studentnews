import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PraksaDetaljiComponent } from './praksa-detalji.component';

describe('PraksaDetaljiComponent', () => {
  let component: PraksaDetaljiComponent;
  let fixture: ComponentFixture<PraksaDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PraksaDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PraksaDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
