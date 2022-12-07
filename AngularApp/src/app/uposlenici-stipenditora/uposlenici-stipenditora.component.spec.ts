import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UposleniciStipenditoraComponent } from './uposlenici-stipenditora.component';

describe('UposleniciStipenditoraComponent', () => {
  let component: UposleniciStipenditoraComponent;
  let fixture: ComponentFixture<UposleniciStipenditoraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UposleniciStipenditoraComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UposleniciStipenditoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
