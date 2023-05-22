import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer, CustomerClient, DeleteCustomerCommand } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { MatMenuTrigger } from '@angular/material/menu';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'role', 'status', 'statusExpirationDate', 'moneySpent', 'actions'];

  customers: Customer[] = [];
  loggedCustomer: Customer[] = [];

  id: string = localStorage.getItem('id')!;
  role: string = localStorage.getItem('role')!;

  constructor(private client: CustomerClient, private router: Router, public dialog: MatDialog) {}

  ngOnInit(){
    this.client.getAll().subscribe(result => {
      this.customers = result;
    });
    this.client.getById(this.id).subscribe(result => {
      this.loggedCustomer.push(result);
      console.log(this.loggedCustomer);
      console.log(this.customers);
    });
  }

  onDelete(query: DeleteCustomerCommand){
    this.client.delete(query).subscribe(_ => {
      this.customers = this.customers.filter(x => x.id !== query.id)
    })
  }

  onUpdate(customer: Customer) {
    this.router.navigate([`customers/edit/${customer.id}`]);
  }

  onCart(customerId: string){
    this.router.navigate([`cart/${customerId}`]);

  }
}
