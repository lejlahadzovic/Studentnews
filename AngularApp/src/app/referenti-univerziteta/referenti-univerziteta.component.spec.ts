import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferentiUniverzitetaComponent } from './referenti-univerziteta.component';

describe('ReferentiUniverzitetaComponent', () => {
  let component: ReferentiUniverzitetaComponent;
  let fixture: ComponentFixture<ReferentiUniverzitetaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferentiUniverzitetaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReferentiUniverzitetaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
