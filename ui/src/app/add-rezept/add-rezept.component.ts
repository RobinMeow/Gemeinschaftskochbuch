import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpErrorHandler } from 'src/app/http-error-handler.service';
import { MessageService } from 'src/app/message.service';
import { Rezept } from './Rezept';
import { RezeptService } from '../rezept.service';



@Component({
  selector: 'app-add-rezept',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    HttpClientModule
  ],
  templateUrl: './add-rezept.component.html',
  styleUrls: ['./add-rezept.component.scss']
})
export class AddRezeptComponent {
  // not marked as static, because I want to access them from the template
  protected readonly NAME_MIN_LENGTH: number = 3;
  protected readonly NAME_MAX_LENGTH: number = 100;

  protected rezeptForm: FormGroup;
  protected rezepte: Rezept[] = [];

  get name(): AbstractControl<any, any> { return this.rezeptForm.get('name')!; }

  constructor(
    fromBuilder: FormBuilder,
    private _rezeptService: RezeptService,
    protected messageService: MessageService
    ) {
      this.rezeptForm = fromBuilder.group({
        name: ['', [ // default value, than array of validators (you can pass in a second array for async validators. I should check it out)
          Validators.required,
          Validators.minLength(this.NAME_MIN_LENGTH),
          Validators.maxLength(this.NAME_MAX_LENGTH)
        ]]
    });

    // ToDo: auto completion and stuff

    // This shows how to make a dynamic form for the Zutaten, which can be any number, so the html should be generated based on it..
    // https://youtu.be/JeeUY6WaXiA?t=355

    _rezeptService.getAll().subscribe((rezepte: Rezept[]) => {
      this.rezepte = rezepte;
    });
  }

  protected onAdd(): void {
    if (!this.rezeptForm.valid) return;

    const rezept: Rezept = {
      name: this.name.value,
    } as Rezept;

    const addRezept$ = this._rezeptService.add(rezept);
    addRezept$.subscribe((rezept: Rezept) => {
      this.rezepte.push(rezept);
    });
  }

}
