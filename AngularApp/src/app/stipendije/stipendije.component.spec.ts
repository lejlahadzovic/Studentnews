import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StipendijeComponent } from './stipendije.component';

describe('StipendijeComponent', () => {
  let component: StipendijeComponent;
  let fixture: ComponentFixture<StipendijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StipendijeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StipendijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
