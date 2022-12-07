import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferentiFakultetaComponent } from './referenti-fakulteta.component';

describe('ReferentiFakultetaComponent', () => {
  let component: ReferentiFakultetaComponent;
  let fixture: ComponentFixture<ReferentiFakultetaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferentiFakultetaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReferentiFakultetaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
