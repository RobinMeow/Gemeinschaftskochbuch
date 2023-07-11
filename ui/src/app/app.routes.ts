import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('src/app/home/home.component').then(c => c.HomeComponent),
        title: 'Kochbuch'
    },
    {
        path: 'home',
        loadComponent: () => import('src/app/home/home.component').then(c => c.HomeComponent),
        title: 'Kochbuch'
    },
    {
        path: 'signin',
        loadComponent: () => import('src/app/sign-in/sign-in.component').then(c => c.SignInComponent),
        title: 'Einloggen'
    },
    {
        path: 'signup',
        loadComponent: () => import('src/app/sign-up/sign-up.component').then(c => c.SignUpComponent),
        title: 'Registrieren'
    },
    {
        path: '**',
        redirectTo: '',
    }
];

// lazy load children: https://youtu.be/c5f8Y2fzZM0?t=777
// https://angular.io/guide/lazy-loading-ngmodules#config-routes
