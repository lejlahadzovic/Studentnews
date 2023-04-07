import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmjestajDetaljiComponent } from './smjestaj-detalji.component';

describe('SmjestajDetaljiComponent', () => {
  let component: SmjestajDetaljiComponent;
  let fixture: ComponentFixture<SmjestajDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SmjestajDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmjestajDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
