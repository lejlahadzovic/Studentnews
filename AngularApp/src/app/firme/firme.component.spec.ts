import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirmeComponent } from './firme.component';

describe('FirmeComponent', () => {
  let component: FirmeComponent;
  let fixture: ComponentFixture<FirmeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirmeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FirmeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
