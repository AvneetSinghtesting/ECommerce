import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from './employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpclient:HttpClient) {}

  getAllEmp():Observable<Employee[]>{
    return this.httpclient.get<Employee[]>("https://localhost:7255/api/Employee");
  }
  saveEmp(newEmp:Employee):Observable<Employee>{
    return this.httpclient.post<Employee>("https://localhost:7255/api/Employee",newEmp);
  }
  updateEmp(Emp:Employee):Observable<Employee>{
    return this.httpclient.patch<Employee>("https://localhost:7255/api/Employee",Emp);
  }
  deleteEmp(id:number):Observable<any>{
    return this.httpclient.delete<any>("https://localhost:7255/api/Employee?id="+id);
  }
  getEmpById(id:number):Observable<Employee>{
    return this.httpclient.get<Employee>("https://localhost:7255/api/Employee/GetById?id="+id);
  }
}
