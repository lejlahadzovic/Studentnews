import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzdavaciSmjestajaComponent } from './izdavaci-smjestaja.component';

describe('IzdavaciSmjestajaComponent', () => {
  let component: IzdavaciSmjestajaComponent;
  let fixture: ComponentFixture<IzdavaciSmjestajaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzdavaciSmjestajaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzdavaciSmjestajaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
