import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StipendijePregledComponent } from './stipendije-pregled.component';

describe('StipendijePregledComponent', () => {
  let component: StipendijePregledComponent;
  let fixture: ComponentFixture<StipendijePregledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StipendijePregledComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StipendijePregledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
