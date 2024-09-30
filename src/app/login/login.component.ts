import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
import { authService } from '../auth.service';
import { Observable, Subscriber } from 'rxjs';
import { LoginVM } from '../login-vm';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HttpClientModule,CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  providers:[HttpClientModule]
    
})

export class LoginComponent implements OnInit {
  user:LoginVM=new LoginVM();
  errorMessage:string="";

  // @ViewChild('loginDiv', { static: true }) loginDiv!: ElementRef;

  constructor(private authService:authService,private router:Router){}

  // onLoginClick(){
  //   this.authService.login(this.user).subscribe(
  //     (Response)=>{
  //       this.user=Response;
  //       console.log("login successful, response:",Response);
  //     },
  //     (error)=>{
  //       console.error("error while login :",error);
  //     }
  //   );

  // }
  ngOnInit(): void {
    // Trigger modal after the view has been initialized
    this.showModal();
  }

  showModal(): void {
    // Get the modal element by its ID
    const modalElement = document.getElementById('loginDiv');

    // Check if the element exists to avoid errors
    if (modalElement) {
      // Create or retrieve the Bootstrap modal instance
      const modal = new Modal(modalElement);

      // Show the modal
      modal.show();
    } else {
      console.error('Modal element not found');
    }
  }

  onLoginClick(){
    this.authService.login(this.user).subscribe({
      next:(Response)=>{
        if(Response.token){
          //alert("onLoginClick from login component");
          this.authService.saveToken(Response.token);
          // alert(Response.token)
          //alert(localStorage.getItem('token'));
          this.router.navigate(['employee']);
        }
        else{
          this.errorMessage="Login Failed, pls check your credentials";
        }
      },
      error:()=>{
        this.errorMessage="login Failed pls try again";
      }
    });
  }


}
