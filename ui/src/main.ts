import { AppComponent } from './app/app.component';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { APP_ROUTES } from './app/app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { importProvidersFrom } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { environment } from './environments/environment';
import { API_BASE_URI, FRONTEND_ORIGINS } from './app/app.tokens';
import { provideFirebaseApp, initializeApp } from '@angular/fire/app';
import { getAuth, provideAuth } from '@angular/fire/auth';

bootstrapApplication(AppComponent, {
    providers: [
        provideRouter(APP_ROUTES),
        importProvidersFrom(BrowserAnimationsModule),
        provideHttpClient(),
        { provide: API_BASE_URI, useValue: environment.apiBaseUri },
        { provide: FRONTEND_ORIGINS, useValue: environment.frontendOrigins },

        importProvidersFrom(
            provideFirebaseApp(() => initializeApp(environment.firebase)),
            provideAuth(() => getAuth()),

        )
    ]
}).catch(err => console.error(err));
