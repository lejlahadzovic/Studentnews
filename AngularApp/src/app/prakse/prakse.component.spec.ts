import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrakseComponent } from './prakse.component';

describe('PrakseComponent', () => {
  let component: PrakseComponent;
  let fixture: ComponentFixture<PrakseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrakseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrakseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
