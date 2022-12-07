import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UposleniciFirmeComponent } from './uposlenici-firme.component';

describe('UposleniciFirmeComponent', () => {
  let component: UposleniciFirmeComponent;
  let fixture: ComponentFixture<UposleniciFirmeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UposleniciFirmeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UposleniciFirmeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
