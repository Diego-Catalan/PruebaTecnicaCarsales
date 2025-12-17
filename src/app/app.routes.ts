import { Routes } from '@angular/router';
import { Api } from './components/api/api';
export const routes: Routes = [/*rutas para que solo se conecte a la página "http://localhost:4200/api"*/
    { path: 'api', component: Api },
    { path: '', redirectTo: 'api', pathMatch: 'full' },
    { path: '**', redirectTo: 'api', pathMatch: 'full' },
];
