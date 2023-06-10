import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Observable, catchError } from 'rxjs';
import { HandleError, HttpErrorHandler } from 'src/app/http-error-handler.service';
import { MessageService } from 'src/app/message.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    // Authorization: 'my-auth-token'
  })
};

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
  providers: [
    MessageService, // we need the message service first
    HttpErrorHandler
  ],
  templateUrl: './add-rezept.component.html',
  styleUrls: ['./add-rezept.component.scss']
})
export class AddRezeptComponent {
  // not marked as static, because I want to access them from the template
  protected readonly NAME_MIN_LENGTH: number = 3;
  protected readonly NAME_MAX_LENGTH: number = 50;
  private _handleError: HandleError; // https://angular.io/guide/http#sending-data-to-a-server

  protected rezeptForm: FormGroup;

  get name(): AbstractControl<any, any> { return this.rezeptForm.get('name')!; }

  constructor(
    fromBuilder: FormBuilder,
    private _httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    protected messageService: MessageService
    ) {
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

    this._handleError = httpErrorHandler.createHandleError('AddRezeptComponent');
  }

  protected onAdd(): void {
    console.log('onAdd raised!');
    console.log(this.rezeptForm.value);

    if (!this.nameIsValid()) return;

    const rezept: Rezept = {
      name: this.name.value
    };

    const example = this.addRezept(rezept);
    example.subscribe((rezept: Rezept) => console.log('Added: ' + rezept.name)); // this is raised even tho it fails ... humm...
  }

  private nameIsValid(): boolean {
    const nameValue: string = this.name.value;

    if (nameValue.length < this.NAME_MIN_LENGTH) return false;
    if (nameValue.length > this.NAME_MAX_LENGTH) return false;
    if (nameValue.length == 0) return false;

    return true;
  }

  addRezept(rezept: Rezept): Observable<Rezept> {
    const body = rezept;
    console.log('addRezept raised!');
    return this._httpClient.post<Rezept>('http://localhost:5263' + '/Rezepte/Add', body, httpOptions)
    .pipe(
      catchError(this._handleError('addRezept', rezept))
    );
  }
}

interface Rezept {
  name: string;
}

// Idea: On the Zutaten. Whener ever a Zutat-name is supplied, (the first character to be excat) it should automatically raise "add Zutat" button. On Submit it needs to filter out the last one, if it is empty ..
