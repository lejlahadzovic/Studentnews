import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniverzitetiPregledComponent } from './univerziteti-pregled.component';

describe('UniverzitetiPregledComponent', () => {
  let component: UniverzitetiPregledComponent;
  let fixture: ComponentFixture<UniverzitetiPregledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniverzitetiPregledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UniverzitetiPregledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
