import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FakultetiPregledComponent } from './fakulteti-pregled.component';

describe('FakultetiPregledComponent', () => {
  let component: FakultetiPregledComponent;
  let fixture: ComponentFixture<FakultetiPregledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FakultetiPregledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FakultetiPregledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
