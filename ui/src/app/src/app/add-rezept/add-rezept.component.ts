import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

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
  protected rezeptForm: FormGroup;

  constructor(fromBuilder: FormBuilder) {
    this.rezeptForm = fromBuilder.group({
      name: ['', [ // default value, than array of validators (you can pass in a second array for async validators. I should check it out)
        Validators.required,
        Validators.minLength(3), 
        Validators.maxLength(50)
      ]] 
    });

    // use for auto completion and stuff
    // this.rezeptForm.valueChanges.subscribe(); 

    // This shows how to make a dynamic form for the Zutaten, which can be any number, so the html should be generated based on it..
    // https://youtu.be/JeeUY6WaXiA?t=355
  }

  protected onAdd(): void {
    console.log(this.rezeptForm.value);
  }
}

// Idea: On the Zutaten. Whener ever a Zutat-name is supplied, (the first character to be excat) it should automatically raise "add Zutat" button. On Submit it needs to filter out the last one, if it is empty ..
