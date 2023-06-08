import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
    {
        path: '**',
        redirectTo: '',
    }
];

// lazy load children: https://youtu.be/c5f8Y2fzZM0?t=777
// https://angular.io/guide/lazy-loading-ngmodules#config-routes
