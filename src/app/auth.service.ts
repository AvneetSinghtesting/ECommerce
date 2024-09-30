import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginVM } from './login-vm';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class authService {

  constructor(private httpClient: HttpClient,private router:Router) { }

  login(userCredentials:LoginVM):Observable<any>{
    return this.httpClient.post<any>("https://localhost:7255/api/Auth",userCredentials);
    // .pipe(
    //   map(user=>{
    //     // login successful if there's a jwt token in the response
    //     if(user && user.token){
    //       // store user details and jwt token in local storage to keep user logged in between page refreshes
    //       localStorage.setItem('currentUser',JSON.stringify(user))
    //     }
    //     return user;
    //   })
    // );
  }

  logout():void{
            // remove user from local storage to log user out
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isLoggedIn(){
    return localStorage.getItem("token");
  }

  saveToken(Token:string):void{
    //alert("reached saveToke");
    localStorage.setItem('token',Token);
    //alert(localStorage.getItem('token'));
  }


}
