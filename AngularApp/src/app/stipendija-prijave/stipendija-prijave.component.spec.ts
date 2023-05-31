import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StipendijaPrijaveComponent } from './stipendija-prijave.component';

describe('StipendijaPrijaveComponent', () => {
  let component: StipendijaPrijaveComponent;
  let fixture: ComponentFixture<StipendijaPrijaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StipendijaPrijaveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StipendijaPrijaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
