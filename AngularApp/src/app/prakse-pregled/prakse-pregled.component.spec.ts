import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PraksePregledComponent } from './prakse-pregled.component';

describe('PraksePregledComponent', () => {
  let component: PraksePregledComponent;
  let fixture: ComponentFixture<PraksePregledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PraksePregledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PraksePregledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
