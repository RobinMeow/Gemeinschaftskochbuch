import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-add-rezept',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './add-rezept.component.html',
  styleUrls: ['./add-rezept.component.scss']
})
export class AddRezeptComponent {
  // not marked as static, because I want to access them from the template 
  protected readonly NAME_MIN_LENGTH: number = 3;
  protected readonly NAME_MAX_LENGTH: number = 50;
  
  protected rezeptForm: FormGroup;

  get name(): AbstractControl<any, any> { return this.rezeptForm.get('name')!; }

  constructor(fromBuilder: FormBuilder) {
    this.rezeptForm = fromBuilder.group({
      name: ['', [ // default value, than array of validators (you can pass in a second array for async validators. I should check it out)
        Validators.required,
        Validators.minLength(this.NAME_MIN_LENGTH),
        Validators.maxLength(this.NAME_MAX_LENGTH)
      ]] 
    });

    // use for auto completion and stuff
    // this.rezeptForm.valueChanges.subscribe(); 
    this.rezeptForm.valueChanges.subscribe(() => console.log(this.rezeptForm.value));

    // This shows how to make a dynamic form for the Zutaten, which can be any number, so the html should be generated based on it..
    // https://youtu.be/JeeUY6WaXiA?t=355
  }

  protected onAdd(): void {
    console.log(this.rezeptForm.value);
  }
}

// Idea: On the Zutaten. Whener ever a Zutat-name is supplied, (the first character to be excat) it should automatically raise "add Zutat" button. On Submit it needs to filter out the last one, if it is empty ..
