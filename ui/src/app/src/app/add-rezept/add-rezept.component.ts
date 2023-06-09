import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-rezept',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './add-rezept.component.html',
  styleUrls: ['./add-rezept.component.scss']
})
export class AddRezeptComponent {
  protected rezeptForm: FormGroup = new FormGroup({
    name: new FormControl()
  });

  protected onAdd(): void {
    console.log(this.rezeptForm.value);
  }
}
