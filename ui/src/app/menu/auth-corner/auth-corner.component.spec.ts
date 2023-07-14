import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthCornerComponent } from './auth-corner.component';

describe('AuthCornerComponent', () => {
  let component: AuthCornerComponent;
  let fixture: ComponentFixture<AuthCornerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AuthCornerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthCornerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
