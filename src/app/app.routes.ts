import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { EmployeeComponent } from './employee/employee.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
    {path:"home",component:HomeComponent},
    {path:"about",component:AboutComponent},
    {path:"employee",component:EmployeeComponent},
    {path:"login",component:LoginComponent},
    {path:"",redirectTo:'/home',pathMatch:'full'},
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];
