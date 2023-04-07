import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FakultetDetaljiComponent } from './fakultet-detalji.component';

describe('FakultetDetaljiComponent', () => {
  let component: FakultetDetaljiComponent;
  let fixture: ComponentFixture<FakultetDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FakultetDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FakultetDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
