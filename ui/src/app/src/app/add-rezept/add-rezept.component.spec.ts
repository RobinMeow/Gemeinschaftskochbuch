import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRezeptComponent } from './add-rezept.component';

describe('AddRezeptComponent', () => {
  let component: AddRezeptComponent;
  let fixture: ComponentFixture<AddRezeptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AddRezeptComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRezeptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
