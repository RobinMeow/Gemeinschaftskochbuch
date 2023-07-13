import { AppComponent } from './app/app.component';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { APP_ROUTES } from './app/app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { importProvidersFrom } from '@angular/core';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { environment } from './environments/environment';
import { API_BASE_URI, FRONTEND_ORIGINS } from './app/app.tokens';
import { provideFirebaseApp, initializeApp } from '@angular/fire/app';
import { connectAuthEmulator, getAuth, provideAuth } from '@angular/fire/auth';
import { LOG_FAILURE, LOG_SUCCESS } from './macros';
import { TokenInterceptor } from './app/token.interceptor';

bootstrapApplication(AppComponent, {
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
        provideHttpClient(withInterceptorsFromDi()),

        provideRouter(APP_ROUTES),
        importProvidersFrom(BrowserAnimationsModule),
        provideHttpClient(),
        { provide: API_BASE_URI, useValue: environment.apiBaseUri },
        { provide: FRONTEND_ORIGINS, useValue: environment.frontendOrigins },

        importProvidersFrom(
            provideFirebaseApp(() => initializeApp(environment.firebase)),
            provideAuth(() => {
                const auth = getAuth();
                if (environment.isDevelopment) {
                    connectAuthEmulator(auth, 'http://localhost:9099', { disableWarnings: true });
                    fetch('http://localhost:9099', {
                        mode: 'no-cors'
                    })
                    .then(() => LOG_SUCCESS('Firebase Auth Emulator connected at http://localhost:9099 UI: http://127.0.0.1:4000/auth.'))
                    .catch((reason) => {
                        LOG_FAILURE(`Firebase Auth Emulator has not been connected. \nPlease run 'npm run fireauth-emu' to start up the Firebase Auth Emulator.`);
                        throw reason;
                    });
                }
                return auth;
            })
        )
    ]
}).catch(err => console.error(err));
