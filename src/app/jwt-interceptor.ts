import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { authService } from "./auth.service";
import { Router } from "@angular/router";


// export class JwtInterceptor implements HttpInterceptor {

//     constructor(private authService: authService, private router: Router) {}
    
//     // intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     //     // add authorization header with jwt token if available
//     //     // const currentUser=JSON.parse(localStorage.getItem('currentUser'));

//     //     // const localData=localStorage.getItem('currentUser');
//     //     // let currentUser:any=JSON.parse(localData);
//     //     // if(currentUser! && currentUser.token)
//     //     // {
//     //     //     request=request.clone({
//     //     //         setHeaders:{
//     //     //             Authorization:'Bearer ${currentUser.token}'
//     //     //         }
//     //     //     });
//     //     // }
//     //     // return next.handle(request);


//     //     const token=localStorage.getItem('token');

//     //     if(token)
//     //     {
//     //         const cloned=request.clone({
//     //             headers:request.headers.set('Authorization', `Bearer ${token}`)
//     //         });
//     //         return next.handle(cloned);
//     //     }
//     //     return next.handle(request);
//     // }

//     intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//         // Get the token from localStorage
//         const token = localStorage.getItem('token');
    
//         // Clone the request and add the Authorization header with the token if available
//         let clonedRequest = req;
//         if (token) {
//           alert("reached jwtinterceptor");
//           clonedRequest = req.clone({
//             headers: req.headers.set('Authorization', `Bearer ${token}`)
//           });
//         }
    
//         // Continue with the request and catch any errors
//         return next.handle(clonedRequest).pipe(
//           catchError((error: HttpErrorResponse) => {
//             if (error.status === 401) {
//               // Token has expired or the user is unauthorized
//               this.authService.logout(); // Log out the user
//               this.router.navigate(['/login']); // Redirect to the login page
//             }
    
//             // Handle other errors (optional)
//             return throwError(() => error);
//           })
//         );
//       }
// }
// export class JwtInterceptor implements HttpInterceptor {
//   constructor(private authService: authService) {}

//   intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//       const token = localStorage.getItem('token');

//       if (token) {
//         alert("into interceptor");
//           request = request.clone({
//               setHeaders: {
//                   Authorization: `Bearer ${token}`
//               }
//           });
//       }

//       return next.handle(request);
//   }
// }
export const JwtInterceptor: HttpInterceptorFn = (req, next) => {
  const aauthService = inject(authService);
  const token = aauthService.isLoggedIn();

  if (token) {
    // alert("control reached jwt interceptor");
    // alert(token);
    const clonedReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
    return next(clonedReq);
  }

  return next(req);
};
