import { Component } from '@angular/core';
import { Employee } from '../employee';
import { EmployeeService } from '../employee.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { error } from 'jquery';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [HttpClientModule,CommonModule,FormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.scss'
})
export class EmployeeComponent {
  employees:Employee[]=[];
  newEmployee:Employee=new Employee();
  editEmployee:Employee=new Employee();

  constructor(private empService:EmployeeService){}

  ngOnInit(){
    this.getAllEmployees();
  }
  
  // showModal():void{
  //   const modalElement = document.getElementById('showAllEmpList');

  //   // Check if the element exists to avoid errors
  //   if (modalElement) {
  //     // Create or retrieve the Bootstrap modal instance
  //     const modal = new Modal(modalElement);

  //     // Show the modal
  //     modal.show();
  //   } else {
  //     console.error('Modal element not found');
  //   }
  // }

  getAllEmployees(){
    this.empService.getAllEmp().subscribe(
      (response)=>{
        this.employees=response;
      },
      (error)=>{
        console.log(error);
      }
    );
  }

  onDeleteClick(event: Event, id: number): void {
    console.log('Event: ', event);
    console.log('Id: ', id);
    // Handle deletion logic here
    this.empService.deleteEmp(id).subscribe(
      (Response)=>{
        console.log(Response);
        this.getAllEmployees();
      },
      (error)=>{
        console.log(error,"record not deleted");
      }
    );
  }

  onSaveClick():void{
    this.empService.saveEmp(this.newEmployee).subscribe(
      (Response)=>{
        this.newEmployee=Response;
        console.log(Response);
        this.getAllEmployees();
      },
      (error)=>{
        console.log(error);
      }
    );
  }
  
  onEditClick(event:Event,id:number):void{
    this.empService.getEmpById(id).subscribe(
      (Response)=>{
        console.log(Response)
        this.editEmployee=Response
      },
      (error)=>{
        console.log(error,"something went wrong while finding the employee's record");
      }
    );
  }

  onUpdateClick(){
    this.empService.updateEmp(this.editEmployee).subscribe(
      (Response)=>{
        console.log(Response)
        this.getAllEmployees();
      },
      (error)=>{
        console.log(error,"record did not updated");
      }
    );
  }
}
