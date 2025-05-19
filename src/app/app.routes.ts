import { Routes } from '@angular/router';
import { PersonaFormComponent } from './personas/persona-form/persona-form.component';
import { PersonaListComponent } from './personas/persona-list/persona-list.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './guards/auth.guard';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'personas', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'personas',
    canActivate: [AuthGuard],
    children: [
      { path: '', component: PersonaListComponent },
      { path: 'nueva', component: PersonaFormComponent },
      { path: ':id', component: PersonaFormComponent }
    ]
  },
  { path: '**', redirectTo: '' }
]
