import { Component, ViewChild } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { EmployeeComponent } from './employee/employee.component';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModel } from '@angular/forms';
import { JwtInterceptor } from './jwt-interceptor';
import { authService } from './auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HomeComponent,AboutComponent,EmployeeComponent,LoginComponent,RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers:[HttpClientModule]
})
export class AppComponent {
  @ViewChild(LoginComponent) loginRef!:LoginComponent;

  title = 'angularProjectWithJWT';

}
