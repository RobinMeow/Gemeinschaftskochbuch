import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddRezeptComponent } from './add-rezept.component';
import { provideAnimations } from '@angular/platform-browser/animations';

describe('AddRezeptComponent', () => {
  let component: AddRezeptComponent;
  let fixture: ComponentFixture<AddRezeptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AddRezeptComponent,
      ],
      providers: [
        provideAnimations()
      ]
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
