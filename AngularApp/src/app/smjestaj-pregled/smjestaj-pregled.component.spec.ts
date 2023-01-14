import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmjestajPregledComponent } from './smjestaj-pregled.component';

describe('SmjestajPregledComponent', () => {
  let component: SmjestajPregledComponent;
  let fixture: ComponentFixture<SmjestajPregledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SmjestajPregledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmjestajPregledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
