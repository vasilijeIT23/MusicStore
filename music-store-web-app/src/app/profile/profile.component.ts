import { Component } from '@angular/core';
import { Customer, CustomerClient, DeleteCustomerCommand } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'role', 'status', 'statusExpirationDate', 'moneySpent', 'actions'];

  customer: Customer = new Customer;

  id: string = localStorage.getItem('id')!;
  role: string = localStorage.getItem('role')!;

  constructor(private client: CustomerClient,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.client.getById(this.id).subscribe(result => {
      if (result !== null) {
        this.customer = result;
        console.log(this.customer);
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  onDelete(query: DeleteCustomerCommand) {
    this.client.delete(query).subscribe();
  }

  onUpdate(customer: Customer) {
    this.router.navigate([`profile/edit/${customer.id}`]);
  }

  onCart(customerId?: string) {
    this.router.navigate([`cart/${customerId}`]);
  }

  onUpdateCredentials(customerId?: string) {
    this.router.navigate([`credentials/${customerId}`]);
  }
}
