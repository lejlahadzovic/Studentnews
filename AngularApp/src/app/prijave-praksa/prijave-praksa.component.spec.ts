import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrijavePraksaComponent } from './prijave-praksa.component';

describe('PrijavePraksaComponent', () => {
  let component: PrijavePraksaComponent;
  let fixture: ComponentFixture<PrijavePraksaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrijavePraksaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrijavePraksaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
