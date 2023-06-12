import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddRecipeComponent } from './add-recipe.component';
import { provideAnimations } from '@angular/platform-browser/animations';

describe('AddRecipeComponent', () => {
  let component: AddRecipeComponent;
  let fixture: ComponentFixture<AddRecipeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AddRecipeComponent,
      ],
      providers: [
        provideAnimations()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRecipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
